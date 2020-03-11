using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text soldiersOnboardText;
    public Text soldiersSavedText;
    
    public GameObject winText;
    public GameObject lossText;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private GameObject _window;

    private int _soldiersOnboard;
    private int _soldiersSaved;
    private bool _gameOverState;

    private void Start()
    {
        _window = transform.GetChild(0).gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        UpdateText();
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
        
        if(Input.GetKey(KeyCode.R) && _gameOverState)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
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
                    UpdateText();
                }
                break;
            case "Building":
                _soldiersSaved += _soldiersOnboard;
                _soldiersOnboard = 0;
                _window.SetActive(false);
                UpdateText();
                break;
            case "Tree":
                Time.timeScale = 0;
                lossText.SetActive(true);
                _gameOverState = true;
                break;
        }
    }

    private void UpdateText()
    {
        soldiersOnboardText.text = _soldiersOnboard.ToString();
        soldiersSavedText.text = _soldiersSaved.ToString();
    }
}
