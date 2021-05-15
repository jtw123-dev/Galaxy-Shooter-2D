using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4;
    private Player _player;
    private int _points;
    private Animator _animator;
    private AudioSource _enemyExplode; // AudioSource  _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player ==null)
        {
            Debug.LogError("player is null");
        }
        _animator = gameObject.GetComponent<Animator>();
        if (_animator ==null)
        {
            Debug.LogError("animator is null");
        }
        _enemyExplode = GameObject.Find("Explosion_Audio").GetComponent<AudioSource>(); // _audioSource = GetComponent.<AudioSource>();
        //if the audioSource is on the enemy
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y<=-5)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3(randomX,6, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Laser")
        {
            Destroy(other.gameObject);

            if (_player!=null)
            {
                _player.AddScore(10);
            }
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            _enemyExplode.Play();
            Destroy(this.gameObject,(2.8f));
        }

        if (other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player !=null)
            {
                player.Damage();             
            }
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject,(2.8f));
        }           
    }
}
