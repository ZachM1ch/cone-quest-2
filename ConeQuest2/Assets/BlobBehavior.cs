using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBehavior : MonoBehaviour
{
    [Header("Stats")]
    public float meltTime;


    public Mesh landedMesh;
    public Material landedMaterial;

    private Rigidbody rb;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    


    private bool targetHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(targetHit)
        {
            meshFilter.mesh = landedMesh;
            meshRenderer.material = landedMaterial;
            transform.up = Vector3.up;
            

            Invoke(nameof(DeleteBlob), meltTime);
        }


        // bad code formatting, don't drink Jadon's kool-aid
        /*if (rb.velocity.magnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
        }*/

        // good code formatting
        // :)
        if (rb.velocity.magnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.up);
            transform.Rotate(Vector3.up * -90, Space.Self);
        }

        
        Debug.DrawRay(transform.position, rb.velocity.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure a target hasn't been hit
        if (targetHit)
            return;
        else
            targetHit = true;

        Debug.Log("HIT " + collision.gameObject.name);

        rb.isKinematic = true;

        transform.SetParent(collision.transform);
    }

    private void DeleteBlob()
    {
        Destroy(gameObject);
    }

}
