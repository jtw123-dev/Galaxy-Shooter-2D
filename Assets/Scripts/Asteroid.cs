using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int _speed = 10;
    private float _zRotation;
    [SerializeField]
    private GameObject _explode;
    private SpawnManager _spawnManager;
    private AudioSource _asteriodExplode;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("spawn manager is null");
        }
        _asteriodExplode = GameObject.Find("Explosion_Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _zRotation = Time.deltaTime * _speed;
       transform.Rotate (new Vector3(0, 0, _zRotation));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Laser")
        {
            Instantiate(_explode, transform.position, Quaternion.identity);
            _asteriodExplode.Play();
            Destroy(other.gameObject);
            _spawnManager.StartGameSpawning();          
            Destroy(this.gameObject,0.5f);
        }
    }
}
