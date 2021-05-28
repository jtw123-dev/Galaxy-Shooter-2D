﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine("SpawnPowerupRoutine");
        StartCoroutine("SpawnMegaShotRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_stopSpawning==false)
        {
            float randomX = (Random.Range(9, -9));
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX,9,0),Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator  SpawnPowerupRoutine ()
    {
        while (_stopSpawning == false)
        {
            int randomPowerup = Random.Range(0, 6);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }      
    }
    IEnumerator SpawnMegaShotRoutine()
    {
        while (_stopSpawning == false)
        {
            Instantiate(_megaShot, new Vector3(Random.Range(-9, 9), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20,30));
        }
            
    }
    public void OnPlayerDeath ()
    {
        _stopSpawning = true;
    }
}
