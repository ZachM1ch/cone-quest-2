using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform throwPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    private bool readyToThrow;
    private bool throwing;

    private void Start()
    {
        readyToThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is trying and able to throw blob
        if(throwing && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject blob = Instantiate(objectToThrow, throwPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody blobRb = blob.GetComponent<Rigidbody>();

        // add force 
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        blobRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throwing = context.ReadValueAsButton();
    }
}
