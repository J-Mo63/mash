using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private GameObject _window;
    
    private int _soldiersOnboard = 0;
    private int _soldiersSaved = 0;

    private void Start()
    {
        _window = transform.GetChild(0).gameObject;
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

            var windowOffset = isRight ? 1 : -1;
            _window.transform.localPosition = new Vector3(
                Math.Abs(_window.transform.localPosition.x) * windowOffset, 
                0, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Soldier":
                if (_soldiersOnboard < 3)
                {
                    Destroy(col.gameObject);
                    _soldiersOnboard++;
                    if (_soldiersOnboard >= 3)
                        _window.SetActive(true);
                }
                break;
            case "Building":
                _soldiersSaved += _soldiersOnboard;
                _soldiersOnboard = 0;
                _window.SetActive(false);
                break;
        }
    }
}
