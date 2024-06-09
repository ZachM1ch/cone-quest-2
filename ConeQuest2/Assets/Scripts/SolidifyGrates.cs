using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SolidifyGrates : MonoBehaviour
{
    public GameObject grate;
    public float height;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider col)
    {
        GameObject other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y > height)
            grate.GetComponent<BoxCollider>().enabled = true;

        }
    }
}


