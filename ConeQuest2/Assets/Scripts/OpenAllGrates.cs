using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAllGrates : MonoBehaviour
{
    public GameObject grate1;
    public GameObject grate2;

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
            grate1.GetComponent<BoxCollider>().enabled = false;
            grate2.GetComponent<BoxCollider>().enabled = false;

        }
    }
}
