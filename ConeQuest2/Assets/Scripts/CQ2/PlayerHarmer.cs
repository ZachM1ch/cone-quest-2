using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class PlayerHarmer : MonoBehaviour
{
    [Header("Harm Values")]
    [Tooltip("Static Amount that the player will be hurt by")]
    public float staticHarmAmount = -1;
    [Tooltip("(UNUSED) Gradual Amount that the player will be hurt by")]
    public float gradualHarmAmount = 0.05F;
    [Tooltip("Ceiling of how much player will melt gradually")]
    [Range(0.0000001f, Meltometer.MAX_METER)]
    public float gradualHarmLimit = 1;
    [Tooltip("Time taken for gradual harm limit to be reached")]
    public float gradualHarmInterval = 10;

    GameObject other;

    bool gradualHarmActive = false;

    float startOfMeter;

    public GameObject tempTerry;

    public GameObject meterMask;

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
        Meltometer melta = tempTerry.transform.parent.GetComponent<Meltometer>();
        MeltOverTime2(melta);
    }

    /// <summary>
    /// Decreases the level of the Meltometer of the player by a given incremement
    /// </summary>
    /// <param name="terry"> The player </param>
    void StaticMeltPlayer(GameObject terry)
    {
        Meltometer melta = terry.GetComponent<Meltometer>();
        melta.ChangeMeter(staticHarmAmount);

    }

    /// <summary>
    /// Decreases the level of the Meltometer of the player by a given incremement
    /// </summary>
    /// <param name="terry"> The player </param>
    void GradualMeltPlayer(GameObject terry)
    {
        Meltometer melta = terry.transform.parent.GetComponent<Meltometer>();
        MeltOverTime2(melta);
    }

    void MeltOverTime2(Meltometer melt)
    {

        if (melt.currentMeter > startOfMeter - gradualHarmLimit && gradualHarmActive)
        {
            float temp = melt.currentMeter;
            if ((temp - gradualHarmAmount * Time.deltaTime) < startOfMeter - gradualHarmLimit)
            {
                melt.currentMeter = (float)Math.Floor(melt.currentMeter);
                gradualHarmActive = false;
            }
            else
            {
                melt.ChangeMeter(gradualHarmAmount * Time.deltaTime * -1);
            }
        }
        else
        {
            gradualHarmActive = false;
        }
        //print("ghi: " + gradualHarmInterval + "\nghl: " + gradualHarmLimit + "\nmeter: " + melt.currentMeter);
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player") && this.gameObject.CompareTag("Heat") && other)
        {
            StaticMeltPlayer(other.transform.parent.gameObject);

            var sfx = other.transform.parent.gameObject.GetComponent<SFXPlayer>();

            if (sfx)
                sfx.PlayOuch();
        }
        /*
        else if (other.CompareTag("Player") && this.gameObject.CompareTag("Heat"))
        {
            startOfMeter = other.transform.parent.gameObject.GetComponent<Meltometer>().currentMeter;
            gradualHarmActive = true;
            other.transform.parent.gameObject.GetComponent<Meltometer>().currentGradual = gradualHarmAmount;

            meterMask.GetComponent<UIMeltometer>().setupForGradual();

            GradualMeltPlayer(other);
        }
        */
    }

    /// <summary>
    /// Collision Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter(Collision col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player") && this.gameObject.CompareTag("Enemy") && other)
        {
            StaticMeltPlayer(other);

            var sfx = other.transform.GetComponent<SFXPlayer>();

            if (sfx)
                sfx.PlayOuch();
        }
    }
}
