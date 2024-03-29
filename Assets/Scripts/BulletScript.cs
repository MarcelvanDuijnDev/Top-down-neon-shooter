﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _Speed;
    [SerializeField] private float _Damage;

    [SerializeField] private GameObject _BulletObj;
    [SerializeField] private Disable _DisableScript;
    [SerializeField] private ParticleSystem _Particle;

    void FixedUpdate()
    {
        if(_BulletObj.activeSelf)
        transform.Translate(Vector3.forward * _Speed * Time.deltaTime);
    }

    void OnEnable() {
        _BulletObj.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_BulletObj.activeSelf) {
            if (other.tag == "Wall") {
                _Particle.Play();
                _DisableScript.DisableObject(5);
                _BulletObj.gameObject.SetActive(false);
            }
            if (other.tag == "Enemy") {
                _Particle.Play();
                other.gameObject.GetComponent<Enemy>().DoDamage(_Damage);
                _DisableScript.DisableObject(5);
                _BulletObj.gameObject.SetActive(false);
            }
            if (other.tag == "Boss")
            {
                _Particle.Play();
                other.gameObject.GetComponent<EnemyBoss>().DoDamage(_Damage);
                _DisableScript.DisableObject(5);
                _BulletObj.gameObject.SetActive(false);
            }
        }
    }
}
