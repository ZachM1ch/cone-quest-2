using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionalityController : MonoBehaviour
{
    AudioSource myBoombox;
    AudioClip soundClip;
    public AudioClip[] soundClips;

    // Start is called before the first frame update
    void Start()
    {
        myBoombox = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            RestartGame();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            EndGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        soundClip = soundClips[0];
        myBoombox.clip = soundClip;
        myBoombox.Play();
    }
}
