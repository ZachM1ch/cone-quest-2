using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //need rigidbody to add force
    Rigidbody rb; //if we dont type public, it assumes private

    public float speed, jumpForce;
    public bool isGrounded;

    //in order to double jump
    public int jumpsOG = 2;
    int curJumps;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //assigns comopnent to rb
        curJumps = jumpsOG;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //generally want physics operations to be done in FixedUpdate
    private void FixedUpdate()
    {
        //create locals to showcase what we're doing
        float xVel = Input.GetAxis("Horizontal"); //GetAxis only returns -1 to 1
        float zVel = Input.GetAxis("Vertical");
        Vector3 moveForce = Vector3.zero; //zero = (0, 0, 0)

        moveForce.x = xVel * speed; //remember xVel's max value is 1
        moveForce.z = zVel * speed;

        //checking for input should be done in regular update
        //physics should be done in fixed update
        if (isGrounded == true && Input.GetButtonDown("Jump") && curJumps > 0)
        {
            moveForce.y = jumpForce;

            curJumps--;
            if (curJumps <= 0)
            {
                isGrounded = false;
            }
        }

        //lets actually add the force
        rb.AddForce(moveForce);
    }

    //fires exactly once when game object collies with anything
    private void OnCollisionEnter(Collision other)
    {
        print("I just hit " + other.transform.name);

        //if we collide with something tagged Ground...
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            curJumps = jumpsOG;
        }
    }
}