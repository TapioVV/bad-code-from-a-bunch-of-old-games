using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUi : MonoBehaviour
{
    public GameObject pauseScreen;
    public bool paused = false;
    public AudioSource pauseSFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        if(paused == true)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            pauseSFX.Play();
        }
        else if (paused == false)
        {

            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TogglePause();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        TogglePause();
        Destroy(gameObject, 0.5f);
    }
}
