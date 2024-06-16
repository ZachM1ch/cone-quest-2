using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpointer : MonoBehaviour
{
    public float healValue = 5;

    private int CPID;
    
    // kitchen checkpoint should start with all active
    public bool wasTriggered = false;
    public bool isActive = false;
    public bool isBroken = false;

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
    /// Logic to break checkpoints and heal player
    /// </summary>
    /// <param name="terry"></param>
    public void breakCheckpoint(GameObject terry)
    {
        if (isActive)
        {
            if (Input.GetKey(KeyCode.E))
            {
                terry.GetComponent<Meltometer>().ChangeMeter(healValue);
                isActive = false;
                GameManager.GM.RemoveCheckpointFromList(CPID);
            }
        }
    }

    /// <summary>
    /// Get the Checkpoint ID
    /// </summary>
    /// <returns></returns>
    public int GetCPID()
    {
        return CPID;
    }

    /// <summary>
    /// Set the Checkpoint ID
    /// </summary>
    /// <returns></returns>
    public void SetCPID(int cpid)
    {
        CPID = cpid;
    }

    /// <summary>
    /// Clears isActive bool
    /// </summary>
    public void ClearActive()
    {
        isActive = false;
    }

    /// <summary>
    /// Sets isActive bool true
    /// </summary>
    public void MakeActive()
    {
        isActive = true;
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
            if (!isBroken) 
            {
                wasTriggered = true;
                isActive = true;
                GameObject text = this.gameObject.transform.GetChild(0).gameObject;
                if (other != null)
                {
                    transform.LookAt(other.transform);
                }
                breakCheckpoint(other);
            }
        }
    }
}
