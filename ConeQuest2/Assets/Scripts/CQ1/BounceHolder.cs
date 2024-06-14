using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHolder : MonoBehaviour
{

    public float toBounce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject other = col.gameObject;

        if (other.CompareTag("Player1"))
        {
            other.GetComponentInParent<ThirdPersonController>().bounceHeight = toBounce;
        }
    }
}
