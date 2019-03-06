using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float _Health;
    private NavMeshAgent _Nav;
    private GameObject _PlayerObj;

	// Use this for initialization
	void Start () {
        _PlayerObj = GameObject.Find("Player");
        _Nav = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        _Nav.destination = _PlayerObj.transform.position;

        if(_Health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void DoDamage(float damageAmount)
    {
        _Health -= damageAmount;
    }
}
