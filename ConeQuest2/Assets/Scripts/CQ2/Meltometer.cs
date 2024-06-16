using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

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
        if (CheckIfDead())
        {
            Vector3 lastCheckpointPos = lastCheckpoint.transform.position;
            lastCheckpointPos.y += 1;
            terry.transform.position = lastCheckpointPos;
            currentMeter = 3;
        }
    }

    /// <summary>
    /// Changes the meter level based on the incoming increment
    /// </summary>
    /// <param name="increment"> Amount meter will change by </param>
    public void ChangeMeter(float increment)
    {
        currentMeter += increment;
        if (currentMeter > MAX_METER)
        {
            currentMeter = MAX_METER;
        }
        else if (currentMeter < MIN_METER)
        {
            currentMeter = MIN_METER;
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
