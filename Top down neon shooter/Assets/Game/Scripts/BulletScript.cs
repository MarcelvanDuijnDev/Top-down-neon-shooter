using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _Speed;
    [SerializeField] private GameObject _HitEffect;

    void Update()
    {
        transform.Translate(Vector3.forward * _Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Instantiate(_HitEffect, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
        if (other.tag == "Enemy")
        {
            Instantiate(_HitEffect, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<Enemy>().DoDamage(1);
            this.gameObject.SetActive(false);
        }
    }
}
