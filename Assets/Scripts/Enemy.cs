﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y<=-5)
        {
            float randomX = Random.Range(8.7f, -8.7f);
            transform.position = new Vector3(randomX,6, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player !=null)
            {
                player.Damage();             
            }

            Destroy(this.gameObject);


        }
            
    }
}
