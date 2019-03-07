using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject _Camera;
    [SerializeField] private Vector3 _CameraOffset;
    [SerializeField] private GameObject _PlayerObject;
    private Vector2 _ScreenshakeSettings;

    void Update () {
        transform.position = new Vector3(_PlayerObject.transform.position.x, _PlayerObject.transform.position.y + _CameraOffset.y, _PlayerObject.transform.position.z + _CameraOffset.z);

        #region ScreenShake
        if (_ScreenshakeSettings.x > 0) {
            _Camera.transform.localPosition = new Vector3(Random.insideUnitSphere.x * _ScreenshakeSettings.y, Random.insideUnitSphere.y * _ScreenshakeSettings.y, 0);

            _ScreenshakeSettings.x -= 1 * Time.deltaTime;
        }
        else {
            _ScreenshakeSettings.x = 0f;
            _Camera.transform.localPosition = new Vector3(0, 0, 0);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.P)) { ScreenShake(1, 1); }

    }

    public void ScreenShake(float duration, float intensity) {
        _ScreenshakeSettings = new Vector2(duration, intensity);
    }
}
