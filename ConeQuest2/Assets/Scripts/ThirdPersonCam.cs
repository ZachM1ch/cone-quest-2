using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    private Vector3 inputDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    private void FixedUpdate()
    {
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        Vector3 lookDirection = orientation.forward * inputDirection.z + orientation.right * inputDirection.x;

        if (inputDirection != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, lookDirection.normalized, Time.deltaTime * rotationSpeed);
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 direction = context.ReadValue<Vector3>();

        inputDirection = new Vector3(direction.x, 0, direction.z);
    }
}
