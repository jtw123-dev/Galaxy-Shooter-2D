using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float _canFire=-0.4f;
    private float _fireRate=0.5f;
    private float _mineRate = 1;
    private float _canMine = -1;
    private int _speed = 4;
    private int _enemyHealth= 14;
    [SerializeField] private GameObject _bossExplosion;
    [SerializeField] private GameObject _miniBossExplosion;
    [SerializeField] private GameObject _bossMissilePrefab;
    [SerializeField] private GameObject _bossShield;
    private bool _bossShieldActive =true;
    private bool _bossIsAlive = true;
    [SerializeField] private GameObject _mineExplosion;
    [SerializeField] private GameObject _minePrefab;
    private AudioSource _explosionSound;
    private UIManager _manager;

    private void Start()
    {
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _explosionSound = GameObject.Find("Explosion_Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        if (Time.time>_canFire &&_bossIsAlive==true)
        {
            _fireRate = Random.Range(3, 5);
            Instantiate(_bossMissilePrefab, transform.position, Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
        if (Time.time>_canMine&&_bossIsAlive==true &&_enemyHealth<=7)
        {
            _mineRate = Random.Range(1, 2);
            int randX = Random.Range(0, 10);

            Vector3 pos = new Vector3(randX, 12, 0);
            Instantiate(_minePrefab, pos, Quaternion.identity);
            _canMine = Time.time + _mineRate;
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= 0)
        {
            transform.position =new Vector3(0,0,0); 
        }
        if (_enemyHealth == 0)
        {
            Destroy(this.gameObject,(2.3f));
            Instantiate(_bossExplosion, transform.position, Quaternion.identity);
            _explosionSound.Play();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player!=null)
            {
                player.Damage();
            }         
        }
         if (other.tag == "Laser" && _bossShieldActive == true)
        {
            _bossShield.gameObject.SetActive(false);
            Destroy(other.gameObject);
            StartCoroutine("ShieldCoolDown");
            return;
        }
         else if (other.tag == "Laser" && _bossShieldActive ==false )
        {
            Destroy(other.gameObject);
            Instantiate(_miniBossExplosion, transform.position, Quaternion.identity);
            _enemyHealth--;
            _explosionSound.Play();
        }     
    }
    private void BossShieldActive()
    {
        _bossShield.gameObject.SetActive(true);
        _bossShieldActive = true;
    }
    IEnumerator ShieldCoolDown()
    {
        _bossShieldActive = false;
        yield return new WaitForSeconds(10);
        BossShieldActive();
    }
    private void IsDead()
    {
        if (_enemyHealth <=0)
        {
            _bossIsAlive = false;
            _manager.GameOver();
        }
    }
}
