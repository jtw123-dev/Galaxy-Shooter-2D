using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y>=9)
        {
            Destroy(this.gameObject);
           
            if (gameObject.transform.parent  !=null)//can also be transform.parent !=null
            {
                Destroy(transform.parent.gameObject);
            }
            
        }            
    }

}
