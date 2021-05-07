using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    [SerializeField]
    private int _powerupID;//0 is tripleshot 1 is speed and 2 is shields


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
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
                
            switch (_powerupID)
            {
                case 0:
                    player.TripleShotActive();
                    Destroy(this.gameObject);
                    break;
                case 1:
                    player.SpeedActive();
                    Destroy(this.gameObject);
                    break;
                case 2:
                    player.ShieldActive();
                    break;

            }
        }
    }
}
