using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <=-6)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            Destroy(this.gameObject);
        }
    }
}
