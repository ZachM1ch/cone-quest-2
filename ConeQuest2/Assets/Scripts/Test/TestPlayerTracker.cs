using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerTracker : MonoBehaviour
{
    public Transform trackedObject;
    public float maxDistance = 10.0f;
    public float moveSpeed = 20.0f;
    public float updateSpeed = 10.0f;
    [Range(0.0f, 10.0f)]
    public float currentDistance = 5.0f;
    private string moveAxis = "Mouse ScrollWheel";
    private GameObject ahead;
    private MeshRenderer _renderer;
    public float hideDistance = 1.5f;

    private void Start()
    {
        ahead = new GameObject("ahead");
        _renderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    private void LateUpdate()
    {
        ahead.transform.position = trackedObject.position + trackedObject.forward * (maxDistance * 0.25f);
        currentDistance += Input.GetAxisRaw(moveAxis) * moveSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, 0, maxDistance);
        transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + Vector3.up * currentDistance - trackedObject.forward * (currentDistance + maxDistance * 0.5f), updateSpeed * Time.deltaTime);
        transform.LookAt(ahead.transform);
        _renderer.enabled = (currentDistance > hideDistance);
    }  

}
