using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhurt : MonoBehaviour
{
    public float embiggen = 0.05f;

    GameObject other;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //decreases the size of the object by a given Vector3
    void SquinkObject(Vector3 scaleChange)
    {
        other.transform.localScale -= scaleChange;
    }

    public void OnTriggerEnter(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            SquinkObject(new Vector3(embiggen, embiggen, embiggen));
           
        }
    }

}
