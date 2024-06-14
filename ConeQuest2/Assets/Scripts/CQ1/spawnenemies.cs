using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnenemies : MonoBehaviour
{

    public GameObject enemey1;
    public GameObject enemey2;
    public GameObject enemey3;


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
            enemey1.SetActive(true);
            enemey2.SetActive(true);
            enemey3.SetActive(true);
        }
    }

}
