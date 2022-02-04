using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Movement
    [Header("Refrences")]
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _PlayerObject;

    [Header("Speed")]
    [SerializeField] private float _NormalSpeed = 10;
    [SerializeField] private float _SprintSpeed = 15;

    [Header("Gravity")]
    [SerializeField] private float _Gravity = 3;

    [Header("Roll Dash")]
    [SerializeField] private float _RollDuration = 2;
    [SerializeField] private float _DashSpeed = 20;

    [Header("Dash Visual")]
    [SerializeField] private GameObject _PlayerObj_Cube = null;
    [SerializeField] private GameObject _PlayerObj_Sphere = null;

    private Vector3 _DashDirection = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController _CC;
    private bool _Dashing;
    private float _Speed;

    void Start()
    {
        _CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Movement
        if (!_Dashing)
        {
            if (_CC.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= _Speed;
            }
            moveDirection.y -= _Gravity * Time.deltaTime;
            _CC.Move(moveDirection * Time.deltaTime);

            //Dash
            if (Input.GetButtonDown("Jump"))
                StartCoroutine(IDash());
        }
        else
        {
            _CC.Move(_DashDirection * _DashSpeed * Time.deltaTime);
        } 

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            _Speed = _SprintSpeed;
        else
            _Speed = _NormalSpeed;

        //Look At Mouse Position
        Ray cameraRay = _Camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            _PlayerObject.transform.LookAt(new Vector3(pointToLook.x, _PlayerObject.transform.position.y, pointToLook.z));
        }

        //Visual
        if(_Dashing)
        {
            _PlayerObj_Cube.SetActive(false);
            _PlayerObj_Sphere.SetActive(true);
        }    
        else
        {
            _PlayerObj_Cube.SetActive(true);
            _PlayerObj_Sphere.SetActive(false);
        }
    }

    private IEnumerator IDash()
    {
        //Dash Visual On
        _PlayerObj_Cube.SetActive(false);
        _PlayerObj_Sphere.SetActive(true);

        //Apply Dash
        _DashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _Dashing = true;
        yield return new WaitForSeconds(_RollDuration);
        _Dashing = false;

        //Dash Visual Off
        _PlayerObj_Cube.SetActive(true);
        _PlayerObj_Sphere.SetActive(false);
    }
}
