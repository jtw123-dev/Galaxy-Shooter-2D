using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart : MonoBehaviour
{
    private Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindWithTag("Smart").GetComponentInParent<Enemy>();
        if (_enemy==null)
        {
            Debug.LogError("_enemy is null");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            this.transform.parent.GetComponent<Enemy>().SmartAttack();                     
        }
    }
}
