using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var direction = new Vector3();
        if(Input.GetKey(KeyCode.RightArrow))
        {
            direction = transform.right;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -transform.right;
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            direction = transform.up;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            direction = -transform.up;
        }
        _rigidbody2D.AddForce(direction);
        
        if (direction == Vector3.right || direction == Vector3.left)
        {
            var isRight = direction == Vector3.right;
            _spriteRenderer.flipX = isRight;
        }
    }
}
