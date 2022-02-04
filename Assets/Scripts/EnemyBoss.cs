using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
    [SerializeField] private GameObject _DieEffect;

    [SerializeField] private Transform _HealthBar;


    private float _Health;
    private GameObject _PlayerObj;

    void OnEnable()
    {
        _Health = _MaxHealth;
    }

    void Start()
    {
        _PlayerObj = GameObject.Find("Player");
    }

    void Update()
    {
        //HealthBar
        _HealthBar.localScale = new Vector3(_Health / _MaxHealth,1,1);
    }

    public void DoDamage(float damageAmount)
    {
        _Health -= damageAmount;
        if (_Health <= 0)
        {
            GameObject.Find("GameHandler").GetComponent<ScoreHandler>().AddScore(100);
            this.gameObject.SetActive(false);
        }
    }
}
