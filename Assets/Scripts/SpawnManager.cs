using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior
{
    public Random random;
    public string names;
    public int ID;

    public SpawnBehavior(Random random,string names,int ID )
    {
        this.random = random;
        this.names = names;
        this.ID = ID;
    }
}



public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject _tripleShotPowerup;
    [SerializeField]
    private GameObject [] _powerups;
    [SerializeField]
    private GameObject _megaShot;
    private int _enemyCount;
    [SerializeField]
    private int _waveID;
    private bool _asteriodExploded;
    private UIManager _manager;
    public int totalWave;
    private bool _itemStop = false;
    public GameObject[] spawnArray;
    public List<GameObject> spawnList = new List<GameObject>();
    // private GameObject newEnemy;
    private int _powerupRarity;
   

    private GameObject _health;

   // _health = new SpawnBehavior()
    public void StartSpawning()
    {
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();

        StartCoroutine("SpawnCommonPowerupRoutine");
        StartCoroutine("SpawnMegaShotRoutine");
        StartCoroutine("SpawnRarePowerupRoutine");
    }
    public void Update()
    {      
        if (_asteriodExploded ==true)
        {           
            StartCoroutine("SpawnEnemyRoutine");
        }       
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        
        while (_stopSpawning==false)
        {
            switch (_waveID)
            {
                case 0:
                    for (int i =0;i<1; i ++)
                    {
                        float randomX = (Random.Range(9, -9));
                        GameObject newEnemy = Instantiate(spawnArray[0], new Vector3(randomX, 9, 0), Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        spawnList.Add(newEnemy);
                        _enemyCount++;                   
                        
                        if (_enemyCount==1)
                        {              
                            _stopSpawning = true;                          
                        }                          
                    }
                    break;
                case 1:
                    for (int i =0;i<2;i++)
                    {
                        float randomX = (Random.Range(9, -9));
                        GameObject newEnemy = Instantiate(spawnArray[0], new Vector3(randomX, 9, 0), Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        spawnList.Add(newEnemy);
                        _enemyCount++;
                        
                        if (_enemyCount==2)
                        {
                                        
                            _stopSpawning = true;
                            
                        }
                    }
                    break;
                case 2:
                    for (int i =0;i<3;i++)
                    {
                        float randomX = (Random.Range(9, -9));
                        GameObject newEnemy = Instantiate(spawnArray[0], new Vector3(randomX, 9, 0), Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        spawnList.Add(newEnemy);
                        _enemyCount++;                                           

                        if (_enemyCount==3)
                        {       
                            _stopSpawning = true;                          
                            _waveID = -1;
                        }
                    }
                    break;              
            }           
        }
    }
    IEnumerator  SpawnCommonPowerupRoutine ()
    {
         while ( _itemStop==false)
        {
                    int randomPowerup = Random.Range(0, 2);
                    Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(3, 5));
                }
    }
    IEnumerator SpawnMegaShotRoutine()
    {
        while (_itemStop==false)
        {
            Instantiate(_megaShot, new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(25,35));
        }
            
    }
    public void OnPlayerDeath ()
    {
        _stopSpawning = true;
        _itemStop = true;
    }
    public IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(3);
        NewWave(totalWave);
    }

    public void NewWave(int totalWave)
    {
        _waveID++;
        totalWave = _waveID ;       
        _manager.WaveUpdate(totalWave);
        _stopSpawning = false;
    }

    public void EnemyDeath( )
    {      
        _enemyCount--;       
        if (_enemyCount==0)
        {         
            StartCoroutine("WaitToRespawn");                          
        }                     
    }   
    public void AsteriodStart()
    {
        _asteriodExploded = true;
    }
    public IEnumerator SpawnRarePowerupRoutine()
    {
        while (_itemStop == false)
        {
            int randomPowerup = Random.Range(3, 5);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }
}
