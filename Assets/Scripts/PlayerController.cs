using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public AudioClip soldierPickupSound;
    public AudioClip soldierDropoffSound;
    
    public Text soldiersOnboardText;
    public Text soldiersSavedText;
    
    public GameObject winText;
    public GameObject lossText;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private GameObject _window;
    private AudioSource _audioSource;

    private int _soldiersOnboard;
    private int _soldiersSaved;
    private bool _gameOverState;

    private void Start()
    {
        _audioSource = GetComponents<AudioSource>()[1];
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
        
        if ((direction == Vector3.right || direction == Vector3.left) && !_gameOverState)
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
                    PlaySound(soldierPickupSound);
                }
                break;
            case "Building":
                if (_soldiersOnboard > 0)
                {
                    _soldiersSaved += _soldiersOnboard;
                    _soldiersOnboard = 0;
                    _window.SetActive(false);
                    UpdateText();
                    CheckSoldiers();
                    PlaySound(soldierDropoffSound);
                }
                break;
            case "Tree":
                RunGameOver();
                lossText.SetActive(true);
                break;
            case "Bullet":
                RunGameOver();
                lossText.SetActive(true);
                break;
        }
    }

    private void RunGameOver()
    {
        Time.timeScale = 0;
        StopSounds();
        _gameOverState = true;
    }

    private void UpdateText()
    {
        soldiersOnboardText.text = _soldiersOnboard.ToString();
        soldiersSavedText.text = _soldiersSaved.ToString();
    }
    
    private void CheckSoldiers()
    {
        if (GameObject.FindGameObjectsWithTag("Soldier").Length <= 0)
        {
            RunGameOver();
            winText.SetActive(true);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private void StopSounds()
    {
        GetComponents<AudioSource>()[0].Stop();
        GetComponents<AudioSource>()[0].Stop();
    }
}
