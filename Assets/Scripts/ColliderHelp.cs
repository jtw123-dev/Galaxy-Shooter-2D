using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHelp : MonoBehaviour
{
   private int  _speed =  10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag=="Player")
        {
            Debug.Log("hi");
        }
        if (other.tag=="Player"&&Input.GetKey(KeyCode.C))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, other.transform.position, _speed);
        }
    }
}
