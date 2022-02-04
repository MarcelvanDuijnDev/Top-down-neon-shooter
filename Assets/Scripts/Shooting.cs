using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    [SerializeField] private CameraController _Camera;
    [SerializeField] private GameObject _PlayerObject;
    [SerializeField] private Transform _ShootPos;
    [SerializeField] private ObjectPool _ObjectPool;
    [SerializeField] private float _ShootSpeed;
    [SerializeField] private float _Accuracty;

    private float _Timer;

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            _Timer += 1 * Time.deltaTime;
        }
        if(_Timer > _ShootSpeed)
        {
            Spawn();
            _Camera.ScreenShake(0.1f, 0.1f);
            _Timer = 0;
        }
    }

    private void Spawn()
    {
        AudioHandler.AUDIO.PlayTrack("GunShot");
        GameObject bulletobj = _ObjectPool.GetObject("Bullet1");
        bulletobj.transform.position = _ShootPos.position;
        bulletobj.transform.rotation = Quaternion.Euler(_PlayerObject.transform.rotation.eulerAngles.x, _PlayerObject.transform.rotation.eulerAngles.y + Random.Range(-_Accuracty, _Accuracty), _PlayerObject.transform.rotation.eulerAngles.z);
        bulletobj.SetActive(true);
    }
}
