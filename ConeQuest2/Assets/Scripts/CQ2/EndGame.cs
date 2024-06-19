using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
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
            SceneManager.LoadScene(2);
        }
    }

}
