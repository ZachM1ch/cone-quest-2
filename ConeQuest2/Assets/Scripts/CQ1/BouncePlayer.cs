using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    public GameObject player;
    public bool didTouch = false;
    public double yo = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (didTouch == true)
        {

            //player.GetComponent<Rigidbody>().AddForce(0,50,0,ForceMode.Impulse);

            /*
            if (player.GetComponent<Transform>().position.y < player.GetComponent<ThirdPersonController>().bounceHeight)
            {

                yo += 10;// * Time.deltaTime;
                player.transform.position = new Vector3(0, (float)yo, 0);
            }
            else
            {
                //didTouch = false;
            }
            */
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject other = col.gameObject;

        if (other.CompareTag("Player1"))
        {
            float height = other.GetComponentInParent<ThirdPersonController>().bounceHeight;
            didTouch = true;
        }
    }

}
