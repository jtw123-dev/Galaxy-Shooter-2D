using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    [SerializeField] private int _missileDirection;
    [SerializeField] private GameObject _bossMissileExplosion;
    private int _speed = 4;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        InBounds();
        switch (_missileDirection)
        {
            case 0:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case 1:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case 2:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case 3:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
        }          
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            _player.Damage();
            Destroy(this.gameObject);
            Instantiate(_bossMissileExplosion, transform.position, Quaternion.identity);
        }
    }
    private void InBounds()
    {
        if (transform.position.x<-20)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x>15)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.y<-15)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.y>15)
        {
            Destroy(this.gameObject);
        }
    }
}
