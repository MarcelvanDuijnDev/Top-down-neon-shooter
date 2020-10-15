using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float _MaxHealth;
    [SerializeField] private GameObject _DieEffect;
    private float _Health;
    private NavMeshAgent _Nav;
    private GameObject _PlayerObj;

    void OnEnable() {
        _Health = _MaxHealth;
    }

	void Start () {
        _PlayerObj = GameObject.Find("Player");
        _Nav = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        _Nav.destination = _PlayerObj.transform.position;
    }

    public void DoDamage(float damageAmount)
    {
        _Health -= damageAmount;
        if(_Health <= 0)
        {
            GameObject.Find("GameHandler").GetComponent<ScoreHandler>().AddScore(100);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Hit Player");
            Instantiate(_DieEffect, transform.position, Quaternion.identity);
            GameObject.Find("GameHandler").GetComponent<GameHandler>().DoDamage();
            gameObject.SetActive(false);
        }
    }
}
