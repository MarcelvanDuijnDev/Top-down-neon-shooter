﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public ParticleSystem particle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)) {
            particle.Pause();
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            particle.Play();
        }
    }
}
