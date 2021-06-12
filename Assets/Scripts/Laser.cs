using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private bool _isEnemyLaser;

    // Update is called once per frame
    void Update()
    {
       if (_isEnemyLaser==false)
        {
            MoveUp();
        }
       else
        {
            MoveDown();
        }
            
    }
    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 9)
        {
            Destroy(this.gameObject);

            if (gameObject.transform.parent != null)//can also be transform.parent !=null
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime,Space.World);

        if (transform.position.y <= -9)
        {
            Destroy(this.gameObject);

            if (gameObject.transform.parent != null)//can also be transform.parent !=null
            {
                Destroy(transform.parent.gameObject);
            }
        } 
    }
    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player"&& _isEnemyLaser==true)
        {
            Player player = other.GetComponent<Player>();
            if (player!=null)
            {
                player.Damage();
            }

        }
    }
}
