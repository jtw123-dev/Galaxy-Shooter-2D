using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    //private bool _stopSpawning = false;
    [SerializeField] private GameObject _tripleShotPowerup;
    [SerializeField] private GameObject[] _powerups;
    [SerializeField] private GameObject _megaShot;
    private int _enemyCount;
    [SerializeField] private int _waveID;
    private bool _asteriodExploded;
    private UIManager _manager;
    public int totalWave;
    private bool _itemStop = false;
    public GameObject[] spawnArray;
    public List<GameObject> spawnList = new List<GameObject>();
    private int _powerupRarity;
    private GameObject newEnemy;
    private GameObject _health;

    private float _weightedTotal;
    private int _powerupToSpawn;
   [SerializeField] private int _level;

    
    public int[] table = { 50, 25, 20, 5 };
    [SerializeField] private int[] _enemyID;
    public int total;
    public int randomNumber;
    public int enemySelection;
    
    public  int _enemyToSpawn;

    [SerializeField] private bool _isGameActive = true;
    [SerializeField] private bool _spawnEnemyWave = true;
    [SerializeField] private int _currentEnemies = 0;
    [SerializeField] private int _enemiesInCurrentWave = 2;
    [SerializeField] private int _waveNumber = 0;
    private float _spawnRate;

    private void Start()
    {
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void Update()
    {    
        if (_currentEnemies==0 &&_spawnEnemyWave==false)
        {
            _manager.SpawnNextWave();
            StartEnemySpawning();
        }
    
        if (_isGameActive==false)
        {
            //StopCoroutine(SpawnCommonPowerupRoutine());
           // StopCoroutine(SpawnRarePowerupRoutine());
            StopCoroutine(SpawnEnemyRoutine());
        }

        if (_asteriodExploded ==true)
        {           
            StartCoroutine("SpawnEnemyRoutine");
        }       
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        
        while (_isGameActive  && _spawnEnemyWave )
        {
            ChooseEnemy();
            ChooseAPowerup();
            for (int i =0;i<_enemiesInCurrentWave; i ++)
                    {
                
                        float randomX = (Random.Range(9, -9));
                        GameObject newEnemy = Instantiate(spawnArray[_level], new Vector3(randomX, 9, 0), Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        _currentEnemies++;
                        yield return new WaitForSeconds(_spawnRate);
                        
                        if (_isGameActive==false)
                        {
                            break;                         
                        }                          
                    }
                    _level++;
                    _enemiesInCurrentWave += 1;
                    _waveNumber++;
                    _spawnEnemyWave = false;
        }
    }
   /* IEnumerator  SpawnCommonPowerupRoutine ()
    {
         while ( _itemStop==false)
        {
                    int randomPowerup = Random.Range(0, 2);
                    Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(3, 5));
                }
    }*/
    IEnumerator SpawnMegaShotRoutine()
    {
        while (_itemStop==false)
        {
            yield return new WaitForSeconds(Random.Range(25, 35));
            Instantiate(_megaShot, new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);           
        }           
    }
    public void OnPlayerDeath ()
    {
        _itemStop = true;
    }
    public void EnemyDeath( )
    {      
        _currentEnemies--;             
    }   
   /* public IEnumerator SpawnRarePowerupRoutine()
    {
        while (_itemStop == false)
        {
            int randomPowerup = Random.Range(3, 6);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }*/

    private void ChooseAPowerup()
    {
        _weightedTotal = 0;

        int[] powerupTable =
        {
            40,//ammo
            25,//missile
            16,//health
            8,//shield
            6,//speed
            3,//tripleSpeed
            2 // negative
        };
        int[] powerupToAward =
        {
            3,
            5,
            4,
            2,
            1,
            0,
            6
        };
        foreach (var item in powerupTable)
        {
            _weightedTotal += item;
        }
        var randomNumber = Random.Range(0, _weightedTotal);
        var i = 0;

        foreach (var weight in powerupTable)
        {
            if (randomNumber <= weight)
            {
                _powerupToSpawn = powerupToAward[i];
                return;
            }
            else
            {
                i++;
                randomNumber -= weight;
            }
        }
    }


    private void ChooseEnemy()
    {
        _weightedTotal = 0;

        var _enemiesInCurrentWave = _level;
        if(_enemiesInCurrentWave> spawnArray.Length)
        {
            _enemiesInCurrentWave = spawnArray.Length;
        }
        int[] enemyTable =
        {
            50,//main 
            25,//dodge
            15,//shield
            10,//drunk
        };
        int[] enemyID =
        {
            0,
            1,
            2,
            3,
            4
        };
        for (int i =0; i<_enemiesInCurrentWave; i++)
        {
            _weightedTotal += enemyTable[i];
        }
        var randomNumber = Random.Range(0, _weightedTotal);
        var x = 0;
        foreach (var weight in table)
        {
            if (randomNumber <= weight)
            {
                _enemyToSpawn = enemyID[x];
                return;
            }
            else
            {
                x++;
                    randomNumber -= weight;
            }                
        }
    }
    public void StartEnemySpawning()
    {
        if (_waveNumber % 2 == 0)
        {
            _spawnRate -= 0.2f;
            if (_spawnRate<=0.4f)
            {
                _spawnRate = 0.4f;
            }
        }
        StartCoroutine(SpawnEnemyRoutine());
    }
    public int GetWaveNumber()
    {
        return _waveNumber;
    }
    public void EnableNextWaveSpawning()
    {
        _spawnEnemyWave = true;
    }
    public void StartGameSpawning()
    {
        _manager.SpawnNextWave();
        StartCoroutine(SpawnEnemyRoutine());
       // StartCoroutine(SpawnCommonPowerupRoutine());
       // StartCoroutine(SpawnRarePowerupRoutine());
        StartCoroutine("SpawnMegaShotRoutine");
    }   
}
