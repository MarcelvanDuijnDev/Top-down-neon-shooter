using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float _MaxHealht;
    [SerializeField] private GameObject _DieEffect;
    private float _Health;
    private NavMeshAgent _Nav;
    private GameObject _PlayerObj;

    void OnEnable() {
        _Health = _MaxHealht;
    }

	void Start () {
        _PlayerObj = GameObject.Find("Player");
        _Nav = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        _Nav.destination = _PlayerObj.transform.position;

        if(_Health <= 0)
        {
            GameObject.Find("GameHandler").GetComponent<ScoreHandler>().AddScore(10000);
            this.gameObject.SetActive(false);
        }
    }

    public void DoDamage(float damageAmount)
    {
        _Health -= damageAmount;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Instantiate(_DieEffect, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<PlayerScript>().DoDamage(10);
            gameObject.SetActive(false);
        }
    }
}
