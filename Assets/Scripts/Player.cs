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
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    private AudioSource _laserAudioSource, _explosion, _powerup;
    //could also be a serialized field with laser 
    // private AudioClip _exlosion;
    [SerializeField]
    private GameObject _playerExplosion;
    [SerializeField]
    private int _totalAmmo = 15;
    private int _shieldStrength = 3;
    private SpriteRenderer _renderer;
    private bool _isMegaShotActive;
    [SerializeField]
    private GameObject _megaShotPrefab;
    public float currentThrusterFuel =25;
    private float _thrustSpeedMultiplier = 2.8f;
    public float maxThrusterFill = 50;
    private float _thrusterUsage = 20;
    private bool _isThrusterActive = true;
    private CameraShake _cameraShake;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        _shieldToggle.GetComponent<SpriteRenderer>().color = Color.green;
        _laserAudioSource = GameObject.Find("Laser_Audio").GetComponent<AudioSource>();

        if (_laserAudioSource == null)
        {
            Debug.LogError("laseraudio = null");
        }

        _explosion = GameObject.Find("Explosion_Audio").GetComponent<AudioSource>();
        if (_explosion == null)
        {
            Debug.LogError("explosion is null");
        }

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 0, 0);

        _laserOffset = new Vector3(transform.position.x, 1, 0);
        
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("spawnManager is null");
        }
        if (_manager == null)
        {
            Debug.LogError("manager is null");
        }

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        if (_cameraShake==null)
        {
            Debug.LogError("camera is null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();     
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire &&_totalAmmo>0)
        {
            FireLaser();
            AmmoCount();         
        }
        if (currentThrusterFuel<maxThrusterFill)
        {
            currentThrusterFuel +=(_thrusterUsage/ 3) * Time.deltaTime; 
            _manager.UpdateThrusterImage(currentThrusterFuel);
        }
    }
    private void CalculateMovement()
    {
        float horizonatlInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizonatlInput, verticalInput, 0);

        ThrusterBoost(direction);
        if (_isSpeedActive == false)
        {
            transform.Translate(direction * Time.deltaTime * _speed);
        }
        else
        {
            transform.Translate(direction * Time.deltaTime * (_speed * _speedMultiplier));
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

        if (_isTripleShotActive == false && _isMegaShotActive==false)
        {
            Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        else if (_isTripleShotActive ==true &&_isMegaShotActive==false)
        {
            Instantiate(_tripleShotPrefab, transform.position + _tripleShotOffset, Quaternion.identity);
        }
        
        else  if(_isMegaShotActive ==true )
        {
            Instantiate(_megaShotPrefab, transform.position, Quaternion.identity);
        }
        _laserAudioSource.Play();
    }
    public void Damage()
    {
        _cameraShake.Shake();

        if (_isShieldActive && _shieldStrength==3)
        {
            _shieldToggle.GetComponent<SpriteRenderer>().color = Color.yellow;
            _shieldStrength = 2;
            return;
        }
        if (_isShieldActive && _shieldStrength==2)
        {
            _shieldStrength = 1;
            _shieldToggle.GetComponent<SpriteRenderer>().color = Color.red;
            return;
        }

        if (_isShieldActive && _shieldStrength == 1)
        {
            _shieldStrength = 0;
            _shieldToggle.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives--;

        _manager.UpdateLives(_lives);
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
        if (_lives < 1)
        {
            Instantiate(_playerExplosion, transform.position, Quaternion.identity);
            _explosion.Play();
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
    public void SpeedActive()
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
        _shieldStrength = 3;
        _shieldToggle.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void AddScore(int _points)
    {
        _score += _points;
        _manager.ScoreUpdate(_score);
    }            
    public void IncreaseLife()
    {
        if (_lives == 3)
        {
            return;
        }
        else
        {
            _lives++;
            _manager.UpdateLives(_lives);
        }
        if (_lives==3)
        {
            _rightEngine.SetActive(false);
        }
        else if (_lives ==2)
        {
            _leftEngine.SetActive(false);
        }
    }      
    public void AmmoCount()
    {
        _totalAmmo--;
        _manager.UpdateAmmoCount(_totalAmmo);
    }
    public void ThrusterBoost(Vector3 direction)
    {      
        if (Input.GetKey(KeyCode.LeftShift)&&currentThrusterFuel>0)
        {
            if (_isThrusterActive == true)
            {
                transform.Translate(direction * (_speed * _thrustSpeedMultiplier) * Time.deltaTime);
                currentThrusterFuel -= _thrusterUsage * Time.deltaTime;
                _manager.UpdateThrusterImage(currentThrusterFuel);
            }
            else
            {
                transform.Translate(direction * _speed * Time.deltaTime);
                return;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift)&&currentThrusterFuel<=0)
        {
            StartCoroutine("ThrusterCoolDown");
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }
    public void AmmoIncrease()
    {
        if (_totalAmmo<30)
        {
            _totalAmmo += 5;
            _manager.UpdateAmmoCount(_totalAmmo);       
            if (_totalAmmo >30)
            {
                _totalAmmo = 30;
                _manager.UpdateAmmoCount(_totalAmmo);
            }
        }
        else if (_totalAmmo>=30)
        {
            _totalAmmo = 30;
            _manager.UpdateAmmoCount(_totalAmmo);
        }
        
    }
    public void MegaShot()
    {
        _isMegaShotActive = true;
        StartCoroutine("MegaShotPowerDownRoutine");
    }
    IEnumerator MegaShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isMegaShotActive = false;
    }        
    private IEnumerator ThrusterCoolDown()
    {
        _isThrusterActive = false;
        yield return new WaitForSeconds(5);
        _isThrusterActive = true;     
    }
}