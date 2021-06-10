using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    [SerializeField]
    private int _powerupID;//0 is tripleshot 1 is speed and 2 is shields
    private AudioSource _audioSource;
    private int _health;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")

        {
            Player player = other.transform.GetComponent<Player>();
            //AudioSource.PlayClipAtPoint(_clip,transform.position);
            if (player != null)

                switch (_powerupID)
                {
                    case 0:
                        player.AmmoIncrease();
                        Destroy(this.gameObject, 0.3f);
                        _audioSource.Play();
                        break;                      
                    case 1:
                        player.SpeedActive();
                        _audioSource.Play();
                        Destroy(this.gameObject, 0.3f);
                        break;
                    case 2:
                        player.TripleShotActive();
                        _audioSource.Play();
                        Destroy(this.gameObject, 0.3f);
                        break;         
                    case 3:
                        player.ShieldActive();
                        _audioSource.Play();
                        Destroy(this.gameObject, 0.3f);
                        break;
                    case 4:
                        player.IncreaseLife();
                        Destroy(this.gameObject, 0.3f);
                        _audioSource.Play();
                        break;
                    case 5:
                        player.MegaShot();
                        Destroy(this.gameObject, 0.3f);
                        _audioSource.Play();
                        break;
                    case 6:
                        player.ActivatePoison();
                        Destroy(this.gameObject, 0.3f);
                        _audioSource.Play();
                        break;
                    case 7:
                        _player.HomingMissle();
                        Destroy(this.gameObject, 0.3f);
                        _audioSource.Play();
                        break;
                }
        }
        if (other.tag == "EnemyLaser")
        {
            Destroy(gameObject);
        }
    }
}

