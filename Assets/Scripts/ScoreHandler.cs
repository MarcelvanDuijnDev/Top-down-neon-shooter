using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _ScoreText;

    [Header("Settings")]
    [SerializeField] private float _SizeIncrease;
    [SerializeField] private float _HighestScoreAmount;
    [SerializeField] private float _SizeDecreaseSpeed;

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI _AliveTimeText;

    private float _Score;
    private float _AliveTime;
    private float _ScoreEncreaseTimer;
    private float _LocalScale;

    void Start()
    {
        _LocalScale = _ScoreText.fontSize;
        _ScoreText.text = "0";
        _AliveTimeText.text = "0:00:00";
    }

    void Update()
    {
        _ScoreEncreaseTimer += 1 * Time.deltaTime;
        if (_ScoreEncreaseTimer >= 60)
        {
            AddScore(1000);
            _ScoreEncreaseTimer = 0;
        }
        _AliveTime += 1 * Time.deltaTime;

        //To Text
        _ScoreText.text = _Score.ToString("0");
        _AliveTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", Mathf.Floor(_AliveTime / 3600), Mathf.Floor((_AliveTime / 60) % 60), _AliveTime % 60);

        //Score Text Increase
        if (_ScoreText.fontSize > _LocalScale)
            _ScoreText.fontSize -= _SizeDecreaseSpeed * Time.deltaTime;
        else
            _ScoreText.fontSize = _LocalScale;
    }

    public void AddScore(float scoreAdd)
    {
        _Score += scoreAdd;
        float sizeincrease = 100 + (scoreAdd / _HighestScoreAmount) * _SizeIncrease;
        if (sizeincrease > 50)
            sizeincrease = 50;
        if (_ScoreText.fontSize <= sizeincrease)
            _ScoreText.fontSize = sizeincrease;
        _ScoreText.text = _Score.ToString("0");
    }
}