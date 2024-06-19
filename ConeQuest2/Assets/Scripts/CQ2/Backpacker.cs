using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpacker : MonoBehaviour
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
            print(other.gameObject);
            other.transform.parent.GetComponent<Meltometer>().hasBackpack = true;
            other.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
