using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPunish : MonoBehaviour
{
    public GameObject bar1;

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
            bar1.GetComponent<BoxCollider>().enabled = false;

        }
    }
}
