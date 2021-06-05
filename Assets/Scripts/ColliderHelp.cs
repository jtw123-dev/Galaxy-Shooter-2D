using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHelp : MonoBehaviour
{
   private int  _speed =  12;
    public void OnTriggerStay2D(Collider2D other)
    {       
        if (other.tag=="Player"&&Input.GetKey(KeyCode.C))
        {
            this.gameObject.transform.parent.position = Vector3.MoveTowards(transform.position, other.transform.position,
                _speed *Time.deltaTime);
            Destroy(this.gameObject, 3f);
        }
    }
}
