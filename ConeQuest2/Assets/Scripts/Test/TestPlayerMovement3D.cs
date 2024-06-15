using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement3D : MonoBehaviour
{
    public float speed = 20.0f;
    private Vector3 motion;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        motion = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        rb.velocity = motion * speed;
    }
}
