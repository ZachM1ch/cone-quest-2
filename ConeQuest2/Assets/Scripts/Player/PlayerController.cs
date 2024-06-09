using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movementInput = Vector3.zero;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform orientationTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [Space]
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float moveSpeed = 4.0f;

    [SerializeField] private float rotationSpeed = 10.0f;

    [Range(0.0f, 0.3f)]
    [SerializeField] private float rotationSmoothTime = 0.12f;

    [SerializeField] private float jumpHeight = 2.0f;

    private Vector3 rigidbodyVelocity = Vector3.zero;

    private void Update()
    {
        GetOrientation();
        GetPlayerInput();
        Jump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        rigidbodyVelocity = playerRigidbody.velocity;
    }

    private void GetOrientation()
    {
        Vector3 viewDirection = playerTransform.position - new Vector3(cameraTransform.position.x, 
            playerTransform.position.y, cameraTransform.position.z);
        orientationTransform.forward = viewDirection.normalized;
    }

    private void GetPlayerInput()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMP");
            movementInput.y = 1.0f;
        }
    }

    private void MovePlayer()
    {
        if(movementInput == Vector3.zero)
        {
            // No movement input, so don't apply movement
            return;
        }

        //Vector3 inputDirection = new Vector3(movementInput.x, 0.0f, movementInput.z).normalized;
        Vector3 inputDirection = orientationTransform.right * movementInput.x +
            orientationTransform.forward * movementInput.z;
        inputDirection.y = 0.0f;

        // Rotate player
        /*
        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
            cameraTransform.eulerAngles.y;
        float rotationVelocity = 0.0f;
        float playerRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
            ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, playerRotation, 0.0f);
        */

        // Player movement
        Vector3 movementVector = inputDirection;

        // Apply move speed
        movementVector.x *= moveSpeed;
        movementVector.z *= moveSpeed;

        playerRigidbody.velocity = new Vector3(movementVector.x, playerRigidbody.velocity.y, movementVector.z);
        //playerRigidbody.MovePosition(new Vector3(movementVector.x, 0.0f, movementVector.z));
    }

    private void Jump()
    {
        if (movementInput.y == 0.0f)
        {
            return;
        }

        playerRigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

}
