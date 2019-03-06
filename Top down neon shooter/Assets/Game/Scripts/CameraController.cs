using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Vector3 _CameraOffset;
    [SerializeField] private GameObject _PlayerObject;

	void Update () {
        transform.position = new Vector3(_PlayerObject.transform.position.x, _PlayerObject.transform.position.y + _CameraOffset.y, _PlayerObject.transform.position.z + _CameraOffset.z);
	}
}
