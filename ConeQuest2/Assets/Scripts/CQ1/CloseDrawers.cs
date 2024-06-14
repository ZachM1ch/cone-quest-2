using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDrawers : MonoBehaviour
{
    public GameObject bar1;
    public GameObject bar2;


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
            Debug.Log("Did it");
            bar1.GetComponent<BoxCollider>().enabled = true;
            bar2.GetComponent<BoxCollider>().enabled = true;

        }
    }
}
