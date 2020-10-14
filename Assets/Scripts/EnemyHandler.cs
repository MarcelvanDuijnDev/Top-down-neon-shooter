using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    [SerializeField] private float _SpawnRate;
    [SerializeField] private Transform[] _SpawnPoints;
    [SerializeField] private ObjectPool _ObjectPool;

    private float _Timer;
    private int _SpawnID;
    private int[] _SpawnLocationID = new int[100000];

    

	void Start () {
        Random.InitState(666);
        for (int i = 0; i < 100000; i++) {
            _SpawnLocationID[i] = Random.Range(0,_SpawnPoints.Length);
        }
	}
	
	void Update () {
        _Timer += 1 * Time.deltaTime;
        
        if(_Timer >= _SpawnRate) {
            Spawn();
            _Timer = 0;
        }
	}

    private void Spawn() {
        for (int i = 0; i < _ObjectPool._P_Objects.Count; i++) {
            if (!_ObjectPool._P_Objects[i].activeInHierarchy) {
                _ObjectPool._P_Objects[i].transform.position = _SpawnPoints[_SpawnLocationID[_SpawnID]].position;
                _ObjectPool._P_Objects[i].SetActive(true);
                _SpawnID++;
                break;
            }
        }
    }
}
