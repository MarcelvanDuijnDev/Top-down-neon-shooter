using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Camera : MonoBehaviour
{
    private enum CameraOptionsPos { None, Follow }
    private enum CameraOptionsRot { None, Follow }

    [Header("Options")]
    [SerializeField] private CameraOptionsPos _CameraOptionPos = CameraOptionsPos.Follow;
    [SerializeField] private CameraOptionsRot _CameraOptionRot = CameraOptionsRot.Follow;

    [Header("Settings - Position")]
    [SerializeField] private Vector3 _OffsetPosition = new Vector3(0,12,-4);
    [SerializeField] private bool _LerpPosition = true;

    [Header("Settings - Rotation")]
    [SerializeField] private Vector3 _OffsetRotation = Vector3.zero;

    [Header("Settings")]
    [SerializeField] private float _Speed_Position = 3;

    [Header("Aim Range")]
    [SerializeField] private float _AimRange = 10;

    [Header("Contraints")]
    [SerializeField] private bool _LockAxis_X = false;
    [SerializeField] private bool _LockAxis_Y = false;
    [SerializeField] private bool _LockAxis_Z = false;

    [Header("Other")]
    [SerializeField] private bool _EnableMouseCursor = false;
    [SerializeField] private Transform _Target = null;
    [SerializeField] private Transform _TargetAimObj = null;
    [SerializeField] private Transform _MouseTarget = null;

    private Vector3 _TargetPosition;
    private float _ScreenShakeDuration;
    private float _ScreenShakeIntensity;
    private Camera _Camera;

    private void Start()
    {
        _Camera = GetComponentInChildren<Camera>();
        Cursor.visible = _EnableMouseCursor;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Effect_ScreenShake(3, 0.5f);
        }

        //Update Target Location
        float x_axis = _Target.transform.position.x + _OffsetPosition.x;
        float y_axis = _Target.transform.position.y + _OffsetPosition.y;
        float z_axis = _Target.transform.position.z + _OffsetPosition.z;

        if (_LockAxis_X)
            x_axis = _OffsetPosition.x;
        if (_LockAxis_Y)
            y_axis = _OffsetPosition.y;
        if (_LockAxis_Z)
            z_axis = _OffsetPosition.z;

        _TargetPosition = new Vector3(x_axis, y_axis, z_axis);

        //Aim
        if (Input.GetMouseButton(1))
        {
            Vector3 mousetarget = _TargetAimObj.forward * _AimRange + _Target.position;
            mousetarget.y = _TargetPosition.y;
            _TargetPosition = Vector3.Lerp(_TargetPosition, mousetarget, 0.5f);
            _TargetPosition.z -= _AimRange * 0.5f + -3;
        }

        // Movement
        switch (_CameraOptionPos)
        {
            case CameraOptionsPos.Follow:
                if (_LerpPosition)
                    transform.position = Vector3.Lerp(transform.position, _TargetPosition, _Speed_Position * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, _TargetPosition, _Speed_Position * Time.deltaTime);
                break;
        }

        //ScreenShake
        if(_ScreenShakeDuration > 0)
        {
            transform.localPosition = new Vector3(transform.position.x + Random.insideUnitSphere.x * _ScreenShakeIntensity, transform.position.y + Random.insideUnitSphere.y * _ScreenShakeIntensity, transform.position.z);
            _ScreenShakeDuration -= 1 * Time.deltaTime;
        }
        else
        {
            // Rotation
            switch (_CameraOptionRot)
            {
                case CameraOptionsRot.Follow:
                    Vector3 rpos = _Target.position - transform.position;
                    Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
                    transform.eulerAngles = new Vector3(lookrotation.eulerAngles.x + _OffsetRotation.x, lookrotation.eulerAngles.y + _OffsetRotation.y, lookrotation.eulerAngles.z + _OffsetRotation.z);
                    break;
            }
        }

        //Mouse Target
        Plane plane = new Plane(Vector3.up, -_Target.position.y);
        float distance;
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            _MouseTarget.position = ray.GetPoint(distance);
        }
        _MouseTarget.rotation = _TargetAimObj.rotation;
    }

    //Effects
    public void Effect_ScreenShake(float duration, float intesity)
    {
        _ScreenShakeDuration = duration;
        _ScreenShakeIntensity = intesity;
    }

    //Set
    public void Set_CameraTarget(GameObject targetobj)
    {
        _Target = targetobj.transform;
    }
    public void Set_OffSet(Vector3 offset)
    {
        _OffsetPosition = offset;
    }
}
