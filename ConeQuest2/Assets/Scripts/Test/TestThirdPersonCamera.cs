using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThirdPersonCamera : MonoBehaviour
{
    [Header("Reference Fields")]
    public Transform cameraTransform;
    public Transform cameraPivot;

    public float lookSensitivity = 1.0f;

    // Used on the Player GameObject with the character controller to smooth rotating


    // Used on the Player Camera to smooth rotating
    private float currentCameraXRotation;
    [SerializeField]
    private float targetYRotation = 0.0f;
    [SerializeField]
    private float targetXRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraLook();
    }

    private void SetCameraLook()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Pressing mouse button 1");
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetYRotation += mouseX * lookSensitivity;
            targetXRotation -= mouseY * lookSensitivity;
            //targetXRotation = Mathf.Clamp(targetXRotation, )
        }

        cameraTransform.eulerAngles = new Vector3(targetXRotation, targetYRotation, 0.0f);

        //cameraTransform.localPosition = Vector3.forward * -(maxDistance - 0.1f);
        //yAngleOffset
    }
}
