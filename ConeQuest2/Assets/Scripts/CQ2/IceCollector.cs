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
        Meltometer melta = terry.GetComponent<Meltometer>();
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
            UnmeltPlayer(other);
            Destroy(this.gameObject);
        }
    }

}
