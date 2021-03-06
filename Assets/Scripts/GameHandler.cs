﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    [SerializeField] private int _MaxHealth;
    [SerializeField] private int _CurrentHealth;

    [Header("Set")]
    [SerializeField] private GameObject _HealthParent;
    [SerializeField] private GameObject _GameOverMenu;

    private List<GameObject> _HealthObj = new List<GameObject>();

	void Start ()
    {
        _GameOverMenu.SetActive(false);
        for (int i = 0; i < _HealthParent.transform.childCount; i++)
        {
            _HealthObj.Add(_HealthParent.transform.GetChild(i).gameObject);
            if(i < _CurrentHealth) { }
                else
            _HealthObj[i].SetActive(false);
        }
	}
	
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(_CurrentHealth != _MaxHealth)
            AddHealth();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_CurrentHealth != 0)
            DoDamage();
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddHealth()
    {
        _HealthObj[_CurrentHealth].SetActive(true);
        _CurrentHealth++;
    }
    public void DoDamage()
    {
        if (_CurrentHealth > 0)
        {
            _CurrentHealth--;
            _HealthObj[_CurrentHealth].SetActive(false);
        }
        
        if(_CurrentHealth <= 0)
        {
            Time.timeScale = 0;
            _GameOverMenu.SetActive(true);
        }
    }
}
