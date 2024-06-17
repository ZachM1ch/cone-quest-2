using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpointer : MonoBehaviour
{
    public float healValue = 5;

    public int CPID;
    
    // kitchen checkpoint should start with all active
    public bool wasTriggered = false;
    public bool isActive = false;
    public bool isBroken = false;

    GameObject other;

    public GameObject tempTerry;

    public GameObject particleExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(tempTerry.transform);
        
    }

    /// <summary>
    /// Logic to break checkpoints and heal player
    /// </summary>
    /// <param name="terry"></param>
    public void breakCheckpoint(GameObject terry)
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CPID != 0)
                {
                    terry.transform.parent.GetComponent<SFXPlayer>().PlayBreakCheckpoint();
                    terry.transform.parent.GetComponent<Meltometer>().ChangeMeter(healValue);
                    isActive = false;
                    GameManager.GM.RemoveCheckpointFromList(CPID);
                    particleExplosion.SetActive(true);
                    Destroy(this.gameObject.transform.parent.gameObject);
                }
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
                if (!isActive)
                {
                    other.transform.parent.GetComponent<SFXPlayer>().PlayCheckpoint();
                }

                wasTriggered = true;
                isActive = true;
                GameObject text = this.gameObject.transform.GetChild(0).gameObject;
                text.GetComponent<MeshRenderer>().enabled = true;
                other.transform.parent.GetComponent<Meltometer>().SetLastCheckpoint(this.gameObject);
                
                if (other != null)
                {
                    transform.LookAt(other.transform);
                }
                breakCheckpoint(other);
            }
        }
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerStay(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            breakCheckpoint(other);
        }
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player"))
        {
            if (!isBroken)
            {
                GameObject text = this.gameObject.transform.GetChild(0).gameObject;
                text.GetComponent<MeshRenderer>().enabled = false;

            }
        }
    }
}
