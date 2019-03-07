using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private float _Health;

	void Start () {
        
    }
	
	void Update () {

    }

    public void DoDamage(float damage) {
        _Health -= damage;
    }
}
