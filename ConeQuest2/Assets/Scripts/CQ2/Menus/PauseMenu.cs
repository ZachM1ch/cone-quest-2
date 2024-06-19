using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuParentObj;

    [Header("Audio")]
    [SerializeField] private AudioMixer masterAudioMixer;
    [SerializeField] private Slider volumeSlider;

    [Space]
    [SerializeField] private ThirdPersonCamera cameraScript;
    [SerializeField] private PlayerThrowing throwingScript;

    private bool isPaused = false;

    private void Start()
    {
        if(!pauseMenuParentObj.activeSelf)
        {
            TogglePauseMenu(true);
        }

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
        }

        TogglePauseMenu(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu(!isPaused);
        }
    }

    private void TogglePauseMenu(bool state)
    {
        pauseMenuParentObj.SetActive(state);
        isPaused = state;
        cameraScript.SetCameraEnabled(!state);
        cameraScript.SetMouseLock(!state);
        throwingScript.ABLE_TO_THROW = !state;

        if(state == true)
        {
            // Pause game
            Time.timeScale = 0.0f;
        }
        else
        {
            // Resume game
            Time.timeScale = 1.0f;
        }
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        masterAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20.0f);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void ResumeButton()
    {
        TogglePauseMenu(false);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }

}
