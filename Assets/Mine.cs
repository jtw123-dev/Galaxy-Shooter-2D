using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private Player _player;
    [SerializeField] private GameObject _mineExplosion;
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player")
        {
            Destroy(this.gameObject,1.5f);
            Instantiate(_mineExplosion, transform.position, Quaternion.identity);
            _player.Damage();
        }
        if (other.tag =="Laser")
        {
            Destroy(this.gameObject, 1.5f);
            Instantiate(_mineExplosion, transform.position, Quaternion.identity);
        }
    }
   
}
