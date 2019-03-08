using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Movement
    [SerializeField] private Camera _Camera;
    [SerializeField] private GameObject _PlayerObject;
    [SerializeField] private float normalSpeed, sprintSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float _DashDistance;

    private float speed;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        //Movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //Dash
        if(Input.GetMouseButtonDown(1))
        {
            Dash();
        }

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

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
        Vector3 destination = _PlayerObject.transform.position + _PlayerObject.transform.forward * _DashDistance;

        if (Physics.Linecast(_PlayerObject.transform.position, destination, out hit))
        {
            destination = _PlayerObject.transform.position + _PlayerObject.transform.forward * (hit.distance - 1);
        }

        if (Physics.Raycast(destination, -Vector3.up, out hit))
        {
            destination = hit.point;
            destination.y = transform.position.y;
            transform.position = destination;
        }
    }
}
