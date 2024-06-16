using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarmer : MonoBehaviour
{
    [Header("Harm Values")]
    [Tooltip("Static Amount that the player will be hurt by")]
    public float staticHarmAmount = 1;
    [Tooltip("(UNUSED) Gradual Amount that the player will be hurt by")]
    public float gradualHarmAmount = 0.05F;
    [Tooltip("Ceiling of how much player will melt gradually")]
    [Range(0.0000001f, Meltometer.MAX_METER)]
    public float gradualHarmLimit = 1;
    [Tooltip("Time taken for gradual harm limit to be reached")]
    public float gradualHarmInterval = 10;

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
    /// Decreases the level of the Meltometer of the player by a given incremement
    /// </summary>
    /// <param name="terry"> The player </param>
    void StaticMeltPlayer(GameObject terry)
    {
        Meltometer melta = terry.transform.parent.GetComponent<Meltometer>();
        melta.ChangeMeter(staticHarmAmount);
    }

    /// <summary>
    /// Decreases the level of the Meltometer of the player by a given incremement
    /// </summary>
    /// <param name="terry"> The player </param>
    void GradualMeltPlayer(GameObject terry)
    {
        Meltometer melta = terry.transform.parent.GetComponent<Meltometer>();
        StartCoroutine(MeltOverTime(melta));
    }

    /// <summary>
    /// Enumerator to decrease Meltometer over a period of time
    /// </summary>
    /// <param name="melt"> Player Meltometer </param>
    /// <returns></returns>
    private IEnumerator MeltOverTime(Meltometer melt)
    {
        float currentTime = 0;
        while ((currentTime / gradualHarmInterval) < gradualHarmLimit)
        {
            melt.ChangeMeter((currentTime / gradualHarmInterval) * -1);
            currentTime += Time.deltaTime;
        }
        yield return null;
    }

    /// <summary>
    /// Trigger Logic
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter(Collider col)
    {
        other = col.gameObject;

        if (other.CompareTag("Player") && this.gameObject.CompareTag("Enemy"))
        {
            StaticMeltPlayer(other);
        }
        else if (other.CompareTag("Player") && this.gameObject.CompareTag("Heat"))
        {
            GradualMeltPlayer(other);
        }

        other.GetComponent<SFXPlayer>().PlayOuch();
    }
}
