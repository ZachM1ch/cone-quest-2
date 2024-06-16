using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class StatTracker : MonoBehaviour
{
    public Rigidbody playerRb;
    public TMP_Text text;

    void Update()
    {
        text.SetText("Speed: " + playerRb.velocity.magnitude.ToString());
    }
}
