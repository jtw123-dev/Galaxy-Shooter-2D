using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 _resetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _resetPos = new Vector3(0, 1, -10);
    }

   public IEnumerator ShakeCamera()
    {
        transform.position = new Vector3 (-0.5f,0.8f,-10);        
       yield return new WaitForSeconds(0.05f);
        transform.position = new Vector3(0.5f, 0.8f, -10);
        yield return new WaitForSeconds(0.05f);
        transform.position = _resetPos;
    }
    public void Shake()
    {
        StartCoroutine("ShakeCamera");
    }
}
