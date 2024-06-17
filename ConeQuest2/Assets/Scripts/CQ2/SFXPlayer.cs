using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{

    AudioSource myBoombox;
    AudioClip soundClip;
    public AudioClip[] genericClips;
    public AudioClip[] jumpClips;
    public AudioClip[] impactClips;

    public AudioClip[] sfx;

    public AudioClip[] organicClips;
    public AudioClip[] bounceClips;
    public AudioClip[] glassClips;
    public AudioClip[] hurtClips;
    public AudioClip[] paperClips;
    public AudioClip[] plasticClips;
    public AudioClip[] metalClips;


    // Start is called before the first frame update
    void Start()
    {
        myBoombox = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayRandomSound(jumpClips);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Organic"))
        {
            PlayRandomSound(organicClips);
        }
        if (other.transform.CompareTag("Bounce"))
        {
            PlayRandomSound(bounceClips);
        }
        if (other.transform.CompareTag("Glass"))
        {
            PlayRandomSound(glassClips);
        }
        if (other.transform.CompareTag("Organic"))
        {
            PlayRandomSound(organicClips);
        }
        if (other.transform.CompareTag("Paper"))
        {
            PlayRandomSound(paperClips);
        }
        if (other.transform.CompareTag("Plastic"))
        {
            PlayRandomSound(plasticClips);
        }
        if (other.transform.CompareTag("Metal"))
        {
            PlayRandomSound(metalClips);
        }
        if (other.transform.CompareTag("Other"))
        {
            PlayRandomSound(genericClips);
        }
    }

    /// <summary>
    /// Play hurt sound
    /// </summary>
    public void PlayOuch()
    {
        PlayRandomSound(hurtClips);
    }
    
    /// <summary>
    /// Plays the sound for interacting with a checkpoint
    /// </summary>
    public void PlayCheckpoint()
    {
        PlaySound(sfx, 0);
    }

    /// <summary>
    /// Plays the sound for breaking a checkpoint
    /// </summary>
    public void PlayBreakCheckpoint()
    {
        PlaySound(sfx, 1);
    }

    /// <summary>
    /// Plays the sound for collecting an ice cube
    /// </summary>
    public void PlayCollect()
    {
        PlayRandomSound(impactClips);
    }

    /// <summary>
    /// Plays a random sound from a list of sounds
    /// </summary>
    /// <param name="ac"></param>
    public void PlayRandomSound(AudioClip[] ac)
    {
        int rand = Random.Range(0, ac.Length);
        soundClip = ac[rand];
        myBoombox.clip = soundClip;
        myBoombox.PlayOneShot(myBoombox.clip);
    }

    /// <summary>
    /// Plays a sound from a list
    /// </summary>
    /// <param name="num"></param>
    public void PlaySound(AudioClip[] soundClips, int num)
    {
        soundClip = soundClips[num];
        myBoombox.clip = soundClip;
        myBoombox.PlayOneShot(myBoombox.clip);
    }
}