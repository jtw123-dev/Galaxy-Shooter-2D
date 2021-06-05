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
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private float _fireRate = 3;
    private float _canFire = -1;
    private bool _sidewaysActive;
    [SerializeField]
    private GameObject _enemyShield;
    private bool _enemyShieldActive ;
    private SpawnManager _manager;
    private bool _canScore = true;

    void Start()
    {
        _manager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_manager==null)
        {
            Debug.LogError("_manager is null");
        }

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
        if (_sidewaysActive==false)
        {
            CalculateMovement();
        }
        else
        {
            StartCoroutine("SideWaysActive");
        }

        FireEnemyLaser();
        
        
    }
    private void FireEnemyLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3, 7);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3(randomX, 6, 0);
            _sidewaysActive = true;
        }
    }
    private void SideWaysMovement()
    {        
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
            if (transform.position.y <= -5)
            {
                float randomX = Random.Range(8.7f, -8.7f);
                transform.position = new Vector3(randomX, 6, 0);
                _sidewaysActive = false;
                CalculateMovement();
            }       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Laser")
        {
            
            Destroy(other.gameObject);

            if (_player!=null &&_enemyShieldActive==false)
            {
                
                _player.AddScore(10);
            }
            if (_enemyShieldActive==true)
            {
                _enemyShieldActive = false;
                return;
            }
            else
            {
                _speed = 0;              
                _animator.SetTrigger("OnEnemyDeath");
                _enemyExplode.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, (2.8f));
                _manager.EnemyDeath();
            }           
        }       
        if (other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player !=null)
            {
                player.Damage();                                                         
            }
            Destroy(GetComponent<Collider2D>());
            _manager.EnemyDeath();
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject,(2.8f));
        }       
        if (other.tag=="Powerup")
        {
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
        if (other.tag=="Ram")
        {
            _speed = 12;
        }
    }
    private  IEnumerator SideWaysActive ()
    {
        yield return new WaitForSeconds(1);
        while (_sidewaysActive==true)
        {
            SideWaysMovement();
            yield return new WaitForSeconds(Random.Range(1, 3));
            _sidewaysActive = false;
        }          
    }
    
}
