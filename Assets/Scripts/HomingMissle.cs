using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    [SerializeField]
    private GameObject _target = null;
    [SerializeField] Transform _playerTarget;
    [SerializeField]
    private GameObject[] _activeEnemies;
    private float _speed = 5;
    private float _minDistance;
    private Vector3 _currentPosition;

    // Start is called before the first frame update
    void Start()
    {
       _target = CalculateClosestEnemy();
    }
    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        if (transform.position.y<=-9)
        {
            Destroy(this.gameObject);
            Destroy(this.gameObject, 5);
        }
        Destroy(this.gameObject, 5);
    }
    private GameObject CalculateClosestEnemy()
    {
        _activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        _minDistance = Mathf.Infinity;
        _currentPosition = transform.position;

        foreach(var enemy in _activeEnemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, _currentPosition);
            if (distance <_minDistance)
            {
                _target = enemy;
                _minDistance = distance;                 
            }
        }
        return _target;
    }
    private void MoveToTarget()
    {
        if (_target!=null)
        {
            if (Vector3.Distance(transform.position, _target.transform.position)!= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);

                Vector2 direction = (transform.position - _target.transform.position).normalized;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var offset = 90f;
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
            }         
        }
        if (_target ==null)
        {
            _target = CalculateClosestEnemy();
        }
    }    
}
