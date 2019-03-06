using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    [SerializeField] private GameObject _PlayerObject;
    [SerializeField] private Transform _ShootPos;
    [SerializeField] private ObjectPool _ObjectPool;
    [SerializeField] private float _ShootSpeed;
    [SerializeField] private float _BulletSpeed;

    private float _Timer;

    void Update()
    {
        if(Input.GetMouseButton(0))
        _Timer += 1 * Time.deltaTime;
        if(_Timer > _ShootSpeed)
        {
            Spawn();
            _Timer = 0;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < _ObjectPool._P_Objects.Count; i++)
        {
            if (!_ObjectPool._P_Objects[i].activeInHierarchy)
            {
                _ObjectPool._P_Objects[i].transform.position = _ShootPos.position;
                _ObjectPool._P_Objects[i].transform.rotation = _PlayerObject.transform.rotation;
                _ObjectPool._P_Objects[i].SetActive(true);
                break;
            }
        }
    }
}
