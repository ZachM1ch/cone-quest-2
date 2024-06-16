using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeVal = 0;
    public Text onScreenTimer;
    public Text NumChix;

    public GameObject counter;
    public GameObject endScr;

    [Header("UI Vars")]
    float highScoreNum;
    public Text highScoreT;
    public Text scoreT;

    public bool stoppingTime;
    public float dioSecondsLeft;

    AudioSource myBoombox;
    AudioClip soundClip;
    public AudioClip[] soundClips;

    public bool gameOn;

    public Color orig;

    public int shakeDur;
    public int shakeAmt;

    public int tock;

    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        highScoreNum = PlayerPrefs.GetFloat("highScoreNum1", 99999999999999999999999999999999999f);
        myBoombox = transform.GetComponent<AudioSource>();
        orig = onScreenTimer.color;
        tock = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn)
        {
            if (!stoppingTime)
            {
                if (counter.transform.localScale.x > 0)
                {
                    timeVal += Time.deltaTime;
                }
                else
                {
                    onScreenTimer.color = Color.blue;
                    endScr.SetActive(true);
                    DetermineScore();

                }
                NumChix.text = (9 - counter.transform.localScale.x) + "/9";
                DisplayTime(timeVal, onScreenTimer);
            }
            else
            {
                if (cam.GetComponent<BackgroundMusicPlayer>().myBoombox.isPlaying)
                {
                    cam.GetComponent<BackgroundMusicPlayer>().myBoombox.Pause();
                }
                dioSecondsLeft += Time.deltaTime;
                if (dioSecondsLeft > tock)
                {
                    tock++;
                    PlaySound(1);
                }
            }

            if (dioSecondsLeft > 55)
            {
                stoppingTime = false;
                dioSecondsLeft = 0;
                onScreenTimer.color = orig;
                cam.GetComponent<BackgroundMusicPlayer>().myBoombox.Play();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Za"))
        {
            Destroy(other.gameObject);
            ZaWarudo();
        }
    }

    void DisplayTime(float displayedTime, Text texts)
    {
        float min = Mathf.FloorToInt(displayedTime / 60);
        float sec = Mathf.FloorToInt(displayedTime % 60);
        float msec = displayedTime % 1 * 1000;

        texts.text = string.Format("{0:00}:{1:00}:{2:000}", min, sec, msec);
    }

    public void DetermineScore()
    {
        scoreT.text = onScreenTimer.text;
        if (timeVal < highScoreNum)
        {
            highScoreNum = timeVal;
            DisplayTime(highScoreNum, highScoreT);
        }
        DisplayTime(highScoreNum, highScoreT);
        PlayerPrefs.SetFloat("highScoreNum1", highScoreNum);
    }

    public void PlaySound(int num)
    {
        soundClip = soundClips[num];
        myBoombox.clip = soundClip;
        myBoombox.Play();
    }

    public void ZaWarudo()
    {
        PlaySound(0);
        onScreenTimer.color = Color.white;
        CameraShake.Shake(shakeDur / 2, shakeAmt / 2);
        stoppingTime = true;
    }

    public void Flip()
    {
        gameOn = !gameOn;
    }

}

