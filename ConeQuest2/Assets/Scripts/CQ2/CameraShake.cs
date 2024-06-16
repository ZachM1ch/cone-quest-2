using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 originalPos;
    public static CameraShake _instance; //this is a static version of the script so we can easily talk to it from other classes

    //Awake() happens before Start():
    private void Awake()
    {
        _instance = this; //assign this GameObject/script into _instance
        originalPos = transform.localPosition;
    }

    public static void Shake(float dur, float amt)
    {
        _instance.StopAllCoroutines(); //if the cam is in the middle of shaking, stop doing that.
        _instance.StartCoroutine(_instance.cShake(dur, amt)); //this will call the coroutine:
    }

    public IEnumerator cShake(float d, float a)
    {
        //first, calculate a time where it should end:
        float endTime = Time.time + d; //Time.time = time since beginning of game

        while (Time.time < endTime)
        {
            //shake:
            transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * a;
            d -= Time.deltaTime;
            yield return null; //this tells the loop to go back
        }
        transform.localPosition = originalPos; //reset the camera's position.
    }

}
