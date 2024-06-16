using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    //Los Globos
    //audio functionality:
    public AudioSource myBoombox; //this is what will play a clip.
    public AudioClip[] soundClips; //we will assign these in the Inspector.
    public AudioClip soundClip;

    public bool bgmOn = true;
    public bool bgm2On = true;

    public int index;

    public GameObject kitchen;
    public GameObject livingRoom;
    public GameObject garage;
    public GameObject outside;

    // Start is called before the first frame update
    void Start()
    {
        myBoombox = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.U))
        {
            kitchen.GetComponent<Checkpointer>().wasTriggered = true;
        }
        if (Input.GetKey(KeyCode.I))
        {
            livingRoom.GetComponent<Checkpointer>().wasTriggered = true;
        }
        if (Input.GetKey(KeyCode.O))
        {
            garage.GetComponent<Checkpointer>().wasTriggered = true;
        }
        if (Input.GetKey(KeyCode.P))
        {
            outside.GetComponent<Checkpointer>().wasTriggered = true;
        }

        if (kitchen.GetComponent<Checkpointer>().wasTriggered == true)
        {
            PlaySound(0);
            kitchen.GetComponent<Checkpointer>().wasTriggered = false;
        }
        if (livingRoom.GetComponent<Checkpointer>().wasTriggered == true)
        {
            PlaySound(1);
            livingRoom.GetComponent<Checkpointer>().wasTriggered = false;
        }
        if (garage.GetComponent<Checkpointer>().wasTriggered == true)
        {
            PlaySound(2);
            garage.GetComponent<Checkpointer>().wasTriggered = false;
        }
        if (outside.GetComponent<Checkpointer>().wasTriggered == true)
        {
            PlaySound(3);
            outside.GetComponent<Checkpointer>().wasTriggered = false;
        }

    }

    /// <summary>
    /// Plays a sound from a list
    /// </summary>
    /// <param name="num"></param>
    public void PlaySound(int num)
    {
        soundClip = soundClips[num];
        myBoombox.clip = soundClip;
        myBoombox.Play();
    }

    public void setIndex(int num)
    {
        index = num;
    }
}
