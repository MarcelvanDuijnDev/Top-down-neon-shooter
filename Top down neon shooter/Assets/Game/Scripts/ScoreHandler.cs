using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _ScoreText;
    private float _Score;

    void Start()
    {
        _ScoreText.text = "0";
    }

    void Update()
    {
        _Score += 100 * Time.deltaTime;

        _ScoreText.text = _Score.ToString("0");
    }

    void AddScore(float scoreAdd)
    {
        _Score += scoreAdd;
    }
}
