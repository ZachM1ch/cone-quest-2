using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Meltometer : MonoBehaviour
{
    // Meter level bounds
    [Tooltip("Max Meter Level")]
    public const float MAX_METER = 5;
    private const float MIN_METER = 0;

    [Tooltip("Current Meter Level")]
    public float currentMeter = 5;

    [Tooltip("Most Recent CheckPoint Touched")]
    public GameObject lastCheckpoint;

    GameObject terry;

    public GameObject meterMask;

    public float currentGradual;

    public bool hasBackpack;
    public bool hasCube;

    private void Awake()
    {
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        terry = this.gameObject;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeMeter(-6);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //if (currentMeter > 2)
                ChangeMeter(-3);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeMeter(-1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMeter(1);
        }
        */

        if (CheckIfDead())
        {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.GetComponent<PlayerInput>().enabled = false;
            this.gameObject.GetComponent<PlayerThrowing>().enabled = false;

            Vector3 lastCheckpointPos = lastCheckpoint.transform.position;
            lastCheckpointPos.y += 1;
            terry.transform.position = lastCheckpointPos;

            this.gameObject.GetComponent<PlayerMovement>().enabled = true;
            this.gameObject.GetComponent<PlayerInput>().enabled = true;
            this.gameObject.GetComponent<PlayerThrowing>().enabled = false;

            ChangeMeter(((float)Math.Ceiling(MAX_METER / 2)) - 1);
            currentMeter += 1;

            
            
        }

    }

    /// <summary>
    /// Changes the meter level based on the incoming increment
    /// </summary>
    /// <param name="increment"> Amount meter will change by </param>
    public void ChangeMeter(float increment)
    {
        if (hasBackpack && hasCube && increment < 0)
        {
            hasCube = false;
            this.gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            currentMeter += increment;

            meterMask.GetComponent<UIMeltometer>().MoveMeltBar(increment);

            if (currentMeter > MAX_METER)
            {
                currentMeter = MAX_METER;
            }
            else if (currentMeter < MIN_METER)
            {
                currentMeter = MIN_METER;
            }
        }   
    }

    /// <summary>
    /// Checks to see if the player is dead based on meter level
    /// </summary>
    /// <returns> T if dead, F otherwise </returns>
    private bool CheckIfDead()
    {
        return currentMeter == MIN_METER;
    }

    /// <summary>
    /// Set last touched checkpoint
    /// </summary>
    /// <param name="checkpoint"></param>
    public void SetLastCheckpoint(GameObject checkpoint)
    {
        lastCheckpoint = checkpoint;
    }
}
