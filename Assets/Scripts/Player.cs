using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    private Vector3 _laserOffset = new Vector3();
    [SerializeField]
    private int _lives = 3;
    
    
    
    private float _canFire = -1f;
    private float _fireSpeed = 0.5f;
 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _laserOffset = new Vector3(transform.position.x, 1, 0);
       
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    private void CalculateMovement()
    {
        float horizonatlInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizonatlInput, verticalInput, 0);
        transform.Translate(direction * Time.deltaTime * _speed);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void FireLaser()
    {         
       _canFire = Time.time + _fireSpeed;
       
        Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);       
    }
    public void Damage()
    {
        _lives--;
        if (_lives<1)
        {
            Destroy(this.gameObject);
        }
    }
}
