using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int _speed = 4;
    private int _enemyHealth= 3;
    [SerializeField] private GameObject _bossExplosion;
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= 0)
        {
            transform.position =new Vector3(0,0,0); 
        }
        if (_enemyHealth == 0)
        {
            Destroy(this.gameObject,(2.3f));
            Instantiate(_bossExplosion, transform.position, Quaternion.identity);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player!=null)
            {
                player.Damage();
            }         
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _enemyHealth--;           
        }
    }
}
