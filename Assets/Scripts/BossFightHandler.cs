using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightHandler : MonoBehaviour
{

    [Header("Boss/Fake Boss")]
    [SerializeField] private GameObject _BossObj;
    [SerializeField] private GameObject _FakeBossObj;
    [SerializeField] private Transform _FakeBoss_Pos;
    [SerializeField] private float _FakeBossMoveSpeed = 2;

    [Header("HealthBar")]
    [SerializeField] private GameObject _HealthBar;

    [Header("Camera")]
    [SerializeField] private Movement_Camera _CameraScript;

    private bool _BossBattleEnabled;

    void Update()
    {
        if(_BossBattleEnabled)
        {
            Intro();
        }
    }

    private void Intro()
    { 
        if(_FakeBossObj.activeSelf)
        {
            _CameraScript.Effect_ScreenShake(0.1f,0.3f);
            _FakeBossObj.transform.position = Vector3.MoveTowards(_FakeBossObj.transform.position, _FakeBoss_Pos.position, _FakeBossMoveSpeed * Time.deltaTime);
            if(_FakeBossObj.transform.position == _FakeBoss_Pos.position)
            {
                _FakeBossObj.SetActive(false);
                _BossObj.SetActive(true);
                _HealthBar.SetActive(true);
            }
        }
    }

    public void StartBossBattle()
    {
        _BossBattleEnabled = true;
    }
}
