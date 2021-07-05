﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
       _enemy = this.transform.parent.GetComponent<Enemy>();
        if (_enemy ==null)
        {
            Debug.LogError("_enemy is null");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            // this.transform.parent.GetComponent<Enemy>().DodgeSpeed();
            _enemy.DodgeSpeed();
        }    
    }   
}
