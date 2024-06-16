using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class StatTracker : MonoBehaviour
{
    public Rigidbody playerRb;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Speed: " + playerRb.velocity.magnitude.ToString());
    }
}
