using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBehavior : MonoBehaviour
{
    [Header("Stats")]
    public float meltTime;

    private Rigidbody rb;

    private bool targetHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(targetHit)
        {
            Invoke(nameof(DeleteBlob), meltTime);
        }
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
