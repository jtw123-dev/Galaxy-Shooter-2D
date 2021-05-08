using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    private Vector3 _laserOffset = new Vector3();
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
   
    private float _canFire = -1f;
    private float _fireSpeed = 0.5f;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private Vector3 _tripleShotOffset = new Vector3(-3.65f, 2.28f, 0f);
    private bool _isSpeedActive;
    private float _speedMultiplier = 2f;
    private bool _isShieldActive;
    [SerializeField]
    private GameObject _shieldToggle;
    private int _score;
    private UIManager _manager;

 
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 0, 0);
        _laserOffset = new Vector3(transform.position.x, 1, 0);
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();
   
        if (_spawnManager ==null)
        {
            Debug.LogError("spawnManager is null");
        }       
        if (_manager==null)
        {
            Debug.LogError("manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    private void CalculateMovement()
    {
        float horizonatlInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizonatlInput, verticalInput, 0);
        
        if (_isSpeedActive==false)
        {
            transform.Translate(direction * Time.deltaTime * _speed);
        }
        else
        {
            transform.Translate(direction * Time.deltaTime * (_speed* _speedMultiplier));
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
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
    void FireLaser()
    {         
       _canFire = Time.time + _fireSpeed;
       
        if (_isTripleShotActive == false)
        {
            Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }               
        else 
        {
            Instantiate(_tripleShotPrefab, transform.position + _tripleShotOffset, Quaternion.identity);
        }      
    }
    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldToggle.SetActive(false);
            return;
        }
        _lives--;

        _manager.UpdateLives(_lives);
        
       
        if (_lives<1)
        {
            _spawnManager.OnPlayerDeath();
            _manager.GameOver();
            Destroy(this.gameObject);            
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine("TripleShotPowerDownRoutine");
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;            
    }
    public void SpeedActive ()
    {
        _isSpeedActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine("SpeedPowerDownRoutine");
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
        _isSpeedActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shield")
        {
            _isShieldActive = true;
            Destroy(other.gameObject);
        }
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldToggle.SetActive(true);
    }

    public void AddScore(int _points)
    {
        _score += _points;
        _manager.ScoreUpdate(_score);
    }
    //method for adding 10 to  score
    //update score to ui
}
