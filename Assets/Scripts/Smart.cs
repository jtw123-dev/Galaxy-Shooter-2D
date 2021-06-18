using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart : MonoBehaviour
{
    private Enemy _enemy;
    private Player _player;
    [SerializeField] private GameObject _enemyLaser;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.Find("Smart_Enemy").GetComponent<Enemy>();
        if (_enemy==null)
        {
            Debug.LogError("_enemy is null");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("_player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            _enemy.SmartAttack();           
        }
    }
}
