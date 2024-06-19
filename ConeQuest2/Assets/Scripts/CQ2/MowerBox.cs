using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerBox : MonoBehaviour
{

    GameObject other;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            openBox();
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
            openBox();
        }
    }


    private void openBox()
    {
        if (this.gameObject.transform.parent.gameObject.active == true)
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            this.gameObject.transform.parent.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.parent.transform.parent.transform.GetChild(2).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }


}
