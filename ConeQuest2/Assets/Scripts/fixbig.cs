using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixbig : MonoBehaviour
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

    void SquinkObject(Vector3 scaleChange)
    {
        other.transform.localScale = scaleChange;
    }

    public void OnTriggerEnter(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            SquinkObject(new Vector3(1f, 1f, 1f));
            
        }
    }
}
