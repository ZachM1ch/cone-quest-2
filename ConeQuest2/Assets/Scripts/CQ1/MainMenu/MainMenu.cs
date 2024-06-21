using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickButtonClip;
    private bool startedGame = false;
    private bool quitGame = false;

    public void StartButton()
    {
        if(!startedGame || !quitGame)
        {
            StartCoroutine(StartGameRoutine());
        }
    }

    public void QuitGame()
    {
        if (!startedGame || !quitGame)
        {
            StartCoroutine(QuitGameRoutine());
        }
    }

    private IEnumerator StartGameRoutine()
    {
        Debug.Log("Start game coroutine");

        startedGame = true;
        audioSource.PlayOneShot(clickButtonClip);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(gameSceneIndex);
    }

    private IEnumerator QuitGameRoutine()
    {
        Debug.Log("Quit game coroutine");

        quitGame = true;
        audioSource.PlayOneShot(clickButtonClip);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

}
