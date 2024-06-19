using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceCollector : MonoBehaviour
{
    [Tooltip("Amount that the player will heal by")]
    public float healAmount = 1;

    GameObject other;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Increases the level of the Meltometer of the player by a given incremement
    /// </summary>
    /// <param name="terry"> The player </param>
    void UnmeltPlayer(GameObject terry)
    {
        Meltometer melta = terry.transform.parent.GetComponent<Meltometer>();
        melta.ChangeMeter(healAmount);
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

            if (other.transform.parent.GetComponent<Meltometer>().currentMeter < Meltometer.MAX_METER)
            {
                UnmeltPlayer(other);
                other.transform.parent.GetComponent<SFXPlayer>().PlayCollect();
                Destroy(this.gameObject);
            }
            else if (other.transform.parent.GetComponent<Meltometer>().hasBackpack == true && other.transform.parent.GetComponent<Meltometer>().hasCube == false)
            {
                other.transform.parent.GetComponent<Meltometer>().hasCube = true;
                print(other.transform.GetChild(0).transform.GetChild(0).gameObject);
                other.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                other.transform.parent.GetComponent<SFXPlayer>().PlayCollect();
                Destroy(this.gameObject);
            }
        }
    }

}
