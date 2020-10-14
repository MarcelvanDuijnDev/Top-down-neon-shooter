using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour {

    [SerializeField] private float _Range;
    [SerializeField] private float _Speed;
    [SerializeField] private float _ShiftSpeed;

    private Vector3 _Center;
    private Vector3 _NewPos;
    private float _Timer;


	void Start () {
        _Center = transform.position;
	}

	void Update () {
        _Timer += 1 * Time.deltaTime;
        if(_Timer >= _ShiftSpeed)
        {
            _NewPos = new Vector3(Random.Range(_Center.x - _Range, _Center.x + _Range), Random.Range(_Center.y - _Range, _Center.y + _Range), Random.Range(_Center.z - _Range, _Center.z + _Range));
            _Timer = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, _NewPos, _Speed * Time.deltaTime);
	}
}
