using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tank : MonoBehaviour
{
    public float speed;
    public float maxMovement;
    public float minMovement;
    public GameObject bulletPrefab;
    private float _target = 0f;
    
    private void Start()
    {
        InvokeRepeating(nameof(Fire), 2f, 2f);
    }

    private void Update()
    {
        var position = transform.position;
        if (Math.Abs(position.x - _target) < 1)
        {
            _target = Random.Range(minMovement, maxMovement);
        }
        transform.position = Vector2.MoveTowards(position, new Vector2(_target, position.y), 
            speed * Time.deltaTime);
    }

    private void Fire()
    {
        var position = transform.position;
        Instantiate(bulletPrefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }
}
