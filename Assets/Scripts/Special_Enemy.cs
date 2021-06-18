using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_Enemy : MonoBehaviour
{
    //private int _speed = 4;
    private SpawnManager _manager;
    private Player _player;
    private Animator _animator;
    private AudioSource _enemyExplode;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_manager == null)
        {
            Debug.LogError("_manager is null");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("player is null");
        }
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("animator is null");
        }
        _enemyExplode = GameObject.Find("Explosion_Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser" || other.tag == "Missile")
        {
            Destroy(other.gameObject);

            if (_player != null )
            {
                _player.AddScore(10);
            }                  
            else
            {
               // _speed = 0;
                _animator.SetTrigger("OnEnemyDeath");
                _enemyExplode.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, (2.8f));
                _manager.EnemyDeath();
            }
        }
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }                      
            else
            {
                Destroy(GetComponent<Collider>());
                _manager.EnemyDeath();
               // _speed = 0;
                _animator.SetTrigger("OnEnemyDeath");
                Destroy(this.gameObject, (2.8f));
            }
        }
        if (other.tag == "Powerup")
        {
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
        if (other.tag == "Ram")
        {
            //_speed = 12;
        }
    }
}
