using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThirdPersonCamera : MonoBehaviour
{
    [Header("Reference Fields")]
    public LayerMask camRaycastMask;
    public Transform cameraTransform;
    public Transform cameraPivot;

    [Space]
    //public float controllerLookSmoothTime = 0.0f;
    public float lookSmoothTime = 0.0f;
    public float cameraDistance = 0.0f;

    [Space]
    public bool USE_CAMERA_SMOOTHING = true;

    [Space]
    public Vector3 pivotOffset = Vector3.zero;
    public Vector2 xRotationLimits = Vector2.zero;

    public float lookSensitivity = 1.0f;

    // Used on the Player Camera to smooth rotating
    private float currentCameraYRotation = 0.0f;
    private float currentCameraXRotation = 0.0f;
    private float xRotationVelocity = 0.0f;
    private float yRotationVelocity = 0.0f;
    private float targetXRotation = 0.0f;
    private float targetYRotation = 0.0f;



    void Update()
    {
        SetCameraLook();
    }

    private void SetCameraLook()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetYRotation += mouseX * lookSensitivity;
            targetXRotation -= mouseY * lookSensitivity;
            targetXRotation = Mathf.Clamp(targetXRotation, xRotationLimits.x, xRotationLimits.y);
        }

        if(USE_CAMERA_SMOOTHING)
        {
            currentCameraXRotation = Mathf.SmoothDampAngle(currentCameraXRotation, targetXRotation, ref xRotationVelocity, lookSmoothTime);
            currentCameraYRotation = Mathf.SmoothDampAngle(currentCameraYRotation, targetYRotation, ref yRotationVelocity, lookSmoothTime);
            cameraPivot.eulerAngles = new Vector3(currentCameraXRotation, currentCameraYRotation, 0.0f);
        }
        else
        {
            cameraPivot.eulerAngles = new Vector3(targetXRotation, targetYRotation, 0.0f);
        }

        Ray camRay = new Ray(cameraPivot.position, -cameraPivot.forward);
        float maxDistance = cameraDistance;
        if(Physics.SphereCast(camRay, 0.25f, out RaycastHit hitInfo, cameraDistance, camRaycastMask))
        {
            maxDistance = (hitInfo.point - cameraPivot.position).magnitude - 0.25f;
        }

        cameraTransform.localPosition = Vector3.forward * -(maxDistance - 0.1f);
    }
}
