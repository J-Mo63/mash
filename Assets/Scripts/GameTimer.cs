using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float _time;
    private Text _text;

    private void Start()
    {
        _time = Time.timeSinceLevelLoad;
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        _text.text = Mathf.RoundToInt(_time) + "s";
    }
}
