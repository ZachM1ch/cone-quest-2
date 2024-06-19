using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer introVideoPlayer;
    [SerializeField] private GameObject videoImage;

    public bool SKIP_VIDEO = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MasterVolume");
            introVideoPlayer.SetDirectAudioVolume(0, volume);
        }
    }

    private void PlayVideo()
    {
        StartCoroutine(WaitForVideo((float)introVideoPlayer.length));
    }

    private IEnumerator WaitForVideo(float clipLength)
    {
        introVideoPlayer.Prepare();
        while (!introVideoPlayer.isPrepared)
        {
            yield return null;
        }

        introVideoPlayer.Play();
        videoImage.SetActive(true);
        yield return new WaitForSeconds(clipLength);
        SceneManager.LoadScene(1);
    }

    public void StartButton()
    {
#if UNITY_EDITOR
        if(SKIP_VIDEO)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayVideo();
        }
#else
        PlayVideo();
#endif
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
