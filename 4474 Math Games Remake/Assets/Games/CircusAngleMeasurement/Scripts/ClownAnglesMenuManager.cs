using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClownAnglesMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused;
    private ClownAnglesAudioManager audioManager;

    void Start()
    {
        pauseMenu.SetActive(false);
        audioManager = FindObjectOfType<ClownAnglesAudioManager>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseToggle();
        }
    }

    public void PauseToggle()
    {
        if (isPaused) {
            Resume();
        }
        else {
            Pause();
        }
        isPaused = !isPaused;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        audioManager.Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        audioManager.Resume();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
