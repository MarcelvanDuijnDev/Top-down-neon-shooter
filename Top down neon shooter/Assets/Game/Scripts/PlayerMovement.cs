using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Movement
    [Header("Refrences")]
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _PlayerObject;
    [Header("Speed")]
    [SerializeField] private float _NormalSpeed;
    [SerializeField] private float _SprintSpeed, _DashSpeed, _JumpSpeed;
    [Header("Other Settings")]
    [SerializeField] private float _Gravity;
    [SerializeField] private float _DashDistance;

    private bool _Dashing;
    private Vector3 _DashPosition;
    private float _Speed;
    private Vector3 moveDirection = Vector3.zero;
    private BoxCollider _BC;

    void Start()
    {
        _BC = GetComponent<BoxCollider>();
    }

    void Update()
    {
        //Movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= _Speed;
            //if (Input.GetButton("Jump"))
            //    moveDirection.y = _JumpSpeed;
        }
        moveDirection.y -= _Gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //Dash
        if(Input.GetButtonDown("Jump"))
            Dash();
        if(_Dashing)
        {
            transform.position = Vector3.MoveTowards(transform.position,_DashPosition, _DashSpeed * Time.deltaTime);
            if(transform.position == _DashPosition)
            {
                _BC.enabled = true;
                _Dashing = false;
            }
        }

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            _Speed = _SprintSpeed;
        else
            _Speed = _NormalSpeed;

        Ray cameraRay = _Camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            _PlayerObject.transform.LookAt(new Vector3(pointToLook.x, _PlayerObject.transform.position.y, pointToLook.z));
        }
    }

    private void Dash()
    {
        RaycastHit hit;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * _DashDistance,0, Input.GetAxis("Vertical") * _DashDistance);
        Vector3 destination = _PlayerObject.transform.position + direction;

        if (!Physics.Linecast(_PlayerObject.transform.position, destination, out hit))
        {
            _Dashing = true;
            _BC.enabled = false;
            if (Physics.Raycast(destination, -Vector3.up, out hit))
            {
                destination = hit.point;
                destination.y = transform.position.y;
                _DashPosition = destination;
            }
        }
    }
}
