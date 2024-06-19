using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MowerMower : MonoBehaviour
{

    GameObject other;

    public GameObject tempTerry;

    bool wasActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       gameObject.transform.GetChild(0).transform.LookAt(tempTerry.transform);

        if (gameObject.transform.localPosition.x == 34.14)
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            GameObject text = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
            text.GetComponent<MeshRenderer>().enabled = true;


            if (Input.GetKeyDown(KeyCode.E))
            {
                randStart();
            }
        }
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerStay(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                randStart();
            }
        }
    }

    private void randStart()
    {
        System.Random rand = new System.Random();
        int chance = rand.Next(1, 4);
        if (chance == 1)
        {
            tempTerry.GetComponent<SFXPlayer>().PlayYesmower();
            gameObject.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            if (gameObject.transform.localPosition.x == 34.14)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            tempTerry.GetComponent<SFXPlayer>().PlayFailmower();
        }
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            if (!wasActivated)
            {
                GameObject text = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
                text.GetComponent<MeshRenderer>().enabled = false;

            }
        }
    }


}
