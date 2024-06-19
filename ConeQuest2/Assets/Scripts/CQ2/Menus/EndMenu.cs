using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer endingVideoPlayer;
    [SerializeField] private GameObject videoImage;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MasterVolume");
            endingVideoPlayer.SetDirectAudioVolume(0, volume);
        }

        PlayVideo();
    }

    private void PlayVideo()
    {
        StartCoroutine(WaitForVideo((float)endingVideoPlayer.length));
    }

    private IEnumerator WaitForVideo(float clipLength)
    {
        endingVideoPlayer.Prepare();
        while (!endingVideoPlayer.isPrepared)
        {
            yield return null;
        }

        endingVideoPlayer.Play();
        videoImage.SetActive(true);
        yield return new WaitForSeconds(clipLength);
        SceneManager.LoadScene(0);
    }

}
