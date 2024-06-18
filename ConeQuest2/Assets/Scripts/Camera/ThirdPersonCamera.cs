using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Reference Fields")]
    public LayerMask camRaycastMask;
    public Transform cameraTransform;
    public Transform cameraPivot;

    [Space]
    //public float controllerLookSmoothTime = 0.0f;
    public float lookSmoothTime = 0.0f;
    public float cameraDistance = 0.0f;
    public float cameraMaxDistance = 0.0f;
    public float cameraMinDistance = 0.0f;

    public float cameraScollSpeed = 0.0f;

    public float sphereCastRadius = 0.25f;

    [Space]
    public bool LOCK_MOUSE = true;
    public bool CAMERA_ENABLED = true;
    public bool USE_CAMERA_SMOOTHING = true;

    [Space]
    public Vector3 pivotOffset = Vector3.zero;
    public Vector2 xRotationLimits = Vector2.zero;

    public float lookSensitivity = 1.0f;

    // Used on the Player Camera to smooth rotating
    private float currentCameraDistance = 0.0f;
    private float currentCameraYRotation = 0.0f;
    private float currentCameraXRotation = 0.0f;
    private float xRotationVelocity = 0.0f;
    private float yRotationVelocity = 0.0f;
    private float targetXRotation = 0.0f;
    private float targetYRotation = 0.0f;

    // --- METHODS ---
    private void Start()
    {
        currentCameraDistance = cameraDistance;
        CheckMouseLock();
    }

    private void Update()
    {
        CheckMouseLock();
        SetCameraLook();
    }

    private void LateUpdate()
    {
        currentCameraDistance -= Input.GetAxisRaw("Mouse ScrollWheel") * cameraScollSpeed;
        currentCameraDistance = Mathf.Clamp(currentCameraDistance, cameraMinDistance, cameraMaxDistance);
    }

    private void SetCameraLook()
    {
        if (CAMERA_ENABLED)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetYRotation += mouseX * lookSensitivity;
            targetXRotation -= mouseY * lookSensitivity;
            targetXRotation = Mathf.Clamp(targetXRotation, xRotationLimits.x, xRotationLimits.y);
        }

        if (USE_CAMERA_SMOOTHING)
        {
            currentCameraXRotation = Mathf.SmoothDampAngle(currentCameraXRotation, targetXRotation, ref xRotationVelocity, lookSmoothTime);
            currentCameraYRotation = Mathf.SmoothDampAngle(currentCameraYRotation, targetYRotation, ref yRotationVelocity, lookSmoothTime);
            cameraPivot.eulerAngles = new Vector3(currentCameraXRotation, currentCameraYRotation, 0.0f);
            cameraTransform.eulerAngles = cameraPivot.eulerAngles;
        }
        else
        {
            cameraPivot.eulerAngles = new Vector3(targetXRotation, targetYRotation, 0.0f);
            cameraTransform.eulerAngles = cameraPivot.eulerAngles;
            //cameraTransform.eulerAngles = new Vector3(targetXRotation, targetYRotation, 0.0f);
        }

        Ray camRay = new Ray(cameraPivot.position, -cameraPivot.forward);
        float maxDistance = currentCameraDistance;
        if (Physics.SphereCast(camRay, sphereCastRadius, out RaycastHit hitInfo, maxDistance, camRaycastMask))
        {
            maxDistance = (hitInfo.point - cameraPivot.position).magnitude - sphereCastRadius;
        }
        Debug.DrawRay(cameraPivot.position, -cameraPivot.forward * maxDistance, Color.red);

        //maxDistance = Mathf.Clamp(maxDistance, cameraMinDistance, cameraMaxDistance);
        //cameraTransform.localPosition = Vector3.forward * -(maxDistance - 0.1f);
        cameraTransform.localPosition = cameraPivot.position + (-cameraPivot.forward) * (maxDistance - 0.1f);
    }
    
    private void CheckMouseLock()
    {
        if(LOCK_MOUSE)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetMouseLock(bool state)
    {
        LOCK_MOUSE = state;
    }

    public void SetCameraEnabled(bool state)
    {
        CAMERA_ENABLED = state;
    }
}
