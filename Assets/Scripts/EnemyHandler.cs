using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    [SerializeField] private float _SpawnRate;
    [SerializeField] private Transform[] _SpawnPoints;
    [SerializeField] private ObjectPool _ObjectPool;

    private float _Timer;
    private int _SpawnID;

	void Update () {
        _Timer += 1 * Time.deltaTime;
        
        if(_Timer >= _SpawnRate) {
            GameObject enemyobj = _ObjectPool.GetObject("Enemy1");
            enemyobj.transform.position = _SpawnPoints[Random.Range(0,_SpawnPoints.Length)].position;
            enemyobj.SetActive(true);
            _Timer = 0;
        }
	}
}
