using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private float _speed = 4;
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
    private bool _enemyShieldActive;  
    [SerializeField]
    private int _shieldCheck;
    private SpawnManager _manager;
    [SerializeField]
    private int _dodgeCheck ;
    private bool _dodgeActive = true;
    [SerializeField] private bool _smartCheck;
    [SerializeField]
    private int _specialEnemyCheck ;
    [SerializeField]
    private GameObject _specialExplode;
    [SerializeField]
    private GameObject _lasershot;
    private Vector3 _velocity;
    private Vector3 _gravity;
    private Rigidbody2D rb;
    float _speedX;
    private Laser _laser;
    [SerializeField]
    private int _enemyID;
    [SerializeField] private GameObject _smartLaser;
    [SerializeField]private int _randShield;



    void Start()
    {
        

        _velocity = new Vector3(2, 2, 0);
        _gravity = new Vector3(0, -1.5f, 0);

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
        ShieldCheck();
        rb = GetComponent<Rigidbody2D>();
        if (rb==null)
        {
            Debug.LogError("rb is null");
        }
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
        if (Time.time > _canFire &&_specialEnemyCheck== 0)
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
        else if (Time.time > _canFire && _specialEnemyCheck == 1)
        {
            _fireRate = Random.Range(3, 7);
            _canFire = Time.time + _fireRate;
            GameObject superLaser = Instantiate(_lasershot, transform.position, Quaternion.identity);
            Laser laser = superLaser.GetComponentInChildren<Laser>();
            laser.AssignEnemyLaser();
        }
    }

    private void CalculateMovement()
    {   
        if (_specialEnemyCheck==0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed, Space.World);//space world helps with rotation
        }
            
        else if (_specialEnemyCheck ==1)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime,Space.World);
            if (transform.position.y<4.5f)
            {
                transform.position += _velocity * Time.deltaTime * 2;
                _velocity += _gravity * Time.deltaTime;

                if (_velocity.y <= -3)
                {
                    _velocity.y = 3;
                }
                if (transform.position.x > 11)
                {
                    transform.position = new Vector3(-11, transform.position.y, 0);
                }
                else if (transform.position.x < -11)
                {
                    transform.position = new Vector3(11, transform.position.y, 0);
                }
            }
            
        

        
    }
 
        if (transform.position.y <= -5)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3  (randomX, 6, 0);
            _sidewaysActive = true;                    
        }        
    }
    private void SideWaysMovement()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
        if (transform.position.y <= -5 && _specialEnemyCheck == 0)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3(randomX, 6, 0);
            _sidewaysActive = false;
            CalculateMovement();
        }
        if (transform.position.x > 10.5f)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3(randomX, 6, 0);
        }
        if (transform.position.x < -10.5f)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    
    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag=="Laser" || other.tag=="Missile")
        {           
            Destroy(other.gameObject);

            if (_player!=null &&_enemyShieldActive==false)
            {            
                _player.AddScore(10);
            }
            if (_enemyShieldActive==true)
            {
                _enemyShieldActive = false;
                _enemyShield.gameObject.SetActive(false);
                return;
            }
            if (_specialEnemyCheck ==1)
            {
                _enemyExplode.Play();
                Instantiate(_specialExplode, transform.position, Quaternion.identity);
                Destroy(GetComponent<Collider2D>());
                _manager.EnemyDeath();
                _speed = 0;
                Destroy(this.gameObject,0.5f);
            }
            else
            {
                _speed = 0;              
                _animator.SetTrigger("OnEnemyDeath");
                _enemyExplode.Play();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 0.5f);
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
             if (other.tag=="Player"&&_enemyShieldActive==true)
            {
                _enemyShieldActive = false;
                _enemyShield.gameObject.SetActive(false);
                return;
            }
             if (_specialEnemyCheck ==1)
            {
                _enemyExplode.Play();
                Instantiate(_specialExplode, transform.position, Quaternion.identity);
                Destroy(GetComponent<Collider2D>());
                _manager.EnemyDeath();
                _speed = 0;
                Destroy(this.gameObject,0.5f);
            }
            else
            {
                Destroy(GetComponent<Collider2D>());
                _manager.EnemyDeath();
                _speed = 0;
                _animator.SetTrigger("OnEnemyDeath");
                Destroy(this.gameObject, (2.8f));
            }
           
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
        if (other.tag=="Ram" &&_smartCheck==false)
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
    
    private void ShieldCheck()
    {
        _randShield = Random.Range(0, 50);
        if (_shieldCheck==1 && _randShield<=25)
        {
                _enemyShieldActive = true;
                _enemyShield.gameObject.SetActive(true);                  
        }
    }
    public void DodgeSpeed()
    {
        if (_dodgeActive==true &&_dodgeCheck==1)
        {
            _speed = 15;
            StartCoroutine("Delay");
        }      
    }

    public void SmartAttack()
    {
        if (_smartCheck == true)
        {
           GameObject smart = Instantiate(_smartLaser, transform.position, Quaternion.identity);
            Laser smartLasers = smart.GetComponentInChildren<Laser>();
            smartLasers.SmartLaser();
        }
    }



private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        _dodgeActive = false;
        _speed =4;
       
    }

}
