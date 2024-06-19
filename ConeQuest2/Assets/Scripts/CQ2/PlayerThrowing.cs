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
    public Transform orientaion;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    private bool readyToThrow;
    private bool throwInput;

    private void Start()
    {
        readyToThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is trying and able to throw blob
        if (throwInput && readyToThrow && totalThrows > 0)
        {
            Throw();
            this.gameObject.GetComponent<Meltometer>().ChangeMeter(-1);
            this.gameObject.GetComponent<SFXPlayer>().PlayOuch();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate object to throw
        GameObject blob = Instantiate(objectToThrow, throwPoint.position, cam.rotation);


        // get rigidbody component
        Rigidbody blobRb = blob.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(throwPoint.position, cam.forward, out hit, 500f, 9 << 1))
        {
            Debug.DrawRay(throwPoint.position, cam.forward, Color.blue, 40);
            forceDirection = (hit.point - throwPoint.position).normalized;
        }

        // add force 
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

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
        throwInput = context.ReadValueAsButton();
    }
}
