using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    public float gravity;
    public float accelFactor;

    bool readyToJump;

    public Animator playerAnimator;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
    public Transform orientation;

    private Vector3 inputDirection;
    private Vector3 moveDirection;

    private Rigidbody rb;

    private SkinnedMeshRenderer mesh;

    private float accel;

    private enum ANIM_STATE
    {
        IDLE = 0,
        WALK,
        JUMP
    }

    private void Awake()
    {
        Screen.SetResolution(1024, 768, true);   
    }
    private void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // make sure jump is possible
        ResetJump();
    }

    private void Update()
    {
        // rotate the mesh's root to the game object's rotation
        mesh.rootBone.transform.localEulerAngles = new Vector3(mesh.rootBone.localEulerAngles.x, mesh.gameObject.transform.localEulerAngles.y, mesh.rootBone.localEulerAngles.z);

        //shoot a raycast down to check if you are grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        SpeedControl();

        // only add ground drag if grounded
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0.0f;

        if(!grounded)
        {
            SetAnimatorState(ANIM_STATE.JUMP);
        }
        else if(inputDirection != Vector3.zero && grounded && rb.velocity.y <= 0.0f)
        {
            SetAnimatorState(ANIM_STATE.WALK);
        }
        else if (grounded)
        {
            SetAnimatorState(ANIM_STATE.IDLE);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // calculate movement direction based on camera orientation
        moveDirection = orientation.forward * inputDirection.z + orientation.right * inputDirection.x;

        // if there is an "up" input, no jump cooldown, and on the ground, then jump
        if(inputDirection.y > 0f && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // if grounded, move in the input direction
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * accel, ForceMode.Force);
        else
        {
            // if jumping, apply air movement multiplier
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
            // artificial gravity to make it feel less floaty
            rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * gravity);
        }

        // if moving, apply an acceleration
        if (moveDirection.normalized != Vector3.zero)
            accel = accel < 1.0f ? accel + accelFactor : accel;
        else
        {
            accel = 0.0f;
            if(grounded)
                rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        }
    }

    private void SpeedControl()
    {
        // get the flat velocity of the rigid body
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit it to the move speed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void Jump()
    {
        // make sure that every jump is exactly the same
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // add jump force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // get directional input vector
        inputDirection = context.ReadValue<Vector3>();
    }

    private void SetAnimatorState(ANIM_STATE state)
    {
        playerAnimator.SetInteger("AnimState", (int)state);
    }

    private ANIM_STATE GetAnimatorState()
    {
        return (ANIM_STATE)playerAnimator.GetInteger("AnimState");
    }
}
