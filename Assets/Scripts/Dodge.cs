using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
       /* _enemy = GameObject.Find("Dodge_Enemy").GetComponent<Enemy>();
        if (_enemy ==null)
        {
            Debug.LogError("_enemy is null");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            this.transform.parent.GetComponent<Enemy>().DodgeSpeed();         
        }    
    }   
}
