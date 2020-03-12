using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tank : MonoBehaviour
{
    public float speed;
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
            _target = Random.Range(-5, 8);
        }
        position = Vector2.MoveTowards(position, 
            new Vector2(_target, position.y), speed * Time.deltaTime);
        transform.position = position;
    }

    private void Fire()
    {
        print("fire!");
    }
}
