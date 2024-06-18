using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowing : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject throwObject;

    private bool throwing;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (throwing)
            Instantiate(throwObject, throwPoint, true);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throwing = context.ReadValueAsButton();
    }
}
