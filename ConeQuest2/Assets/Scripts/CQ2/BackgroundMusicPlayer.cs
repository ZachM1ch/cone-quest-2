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
        /*
        if(Input.GetKeyDown(KeyCode.U))
        {
            kitchen.GetComponent<Checkpointer>().wasTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            livingRoom.GetComponent<Checkpointer>().wasTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            garage.GetComponent<Checkpointer>().wasTriggered = true;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            outside.GetComponent<Checkpointer>().wasTriggered = true;
        }
        */


        if (kitchen.GetComponent<MusicTrigger>().wasTriggered == true)
        {
            PlaySound(0);
            kitchen.GetComponent<MusicTrigger>().wasTriggered = false;
        }
        if (livingRoom.GetComponent<MusicTrigger>().wasTriggered == true)
        {
            PlaySound(1);
            livingRoom.GetComponent<MusicTrigger>().wasTriggered = false;
        }
        if (garage.GetComponent<MusicTrigger>().wasTriggered == true)
        {
            PlaySound(2);
            garage.GetComponent<MusicTrigger>().wasTriggered = false;
        }
        if (outside.GetComponent<MusicTrigger>().wasTriggered == true)
        {
            PlaySound(3);
            outside.GetComponent<MusicTrigger>().wasTriggered = false;
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
