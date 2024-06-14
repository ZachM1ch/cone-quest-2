using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShrinker : MonoBehaviour
{
    public float toShrink = 0.00015f;
    public GameObject grate1;
    public GameObject grate2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y > 0.15f)
        {
            ShrinkObject(new Vector3(toShrink, toShrink, toShrink));
        }
        else
        {
            grate1.GetComponent<BoxCollider>().enabled = false;
            grate2.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //decreases the size of the object by a given Vector3
    void ShrinkObject(Vector3 scaleChange)
    {
        transform.localScale -= scaleChange;
    }
}
