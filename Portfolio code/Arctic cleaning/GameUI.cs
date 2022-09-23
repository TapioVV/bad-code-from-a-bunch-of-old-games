using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    public Image fadeImage;

    public TMP_Text resetText;

    GameObject player;

    public GameObject pauseScreen;

    public RectTransform resumeButtonTransform;
    public RectTransform mainMenuButtonTransform;

    public Button resumeButton;
    public Button mainMenuButton;

    public EventTrigger resumeEvent;
    public EventTrigger mainMenuEvent;

    public bool resetTextActivated;

    bool paused;

    private void Start()
    {
        fadeImage.DOFade(0, 1f);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if (resetTextActivated == true)
        {
            resetText.DOFade(1, 1f);
            resetTextActivated = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


    public void Pause()
    {
        paused = !paused;
        
        if(paused == true)
        {
            player.GetComponent<Player>().activated = false;
            pauseScreen.SetActive(true);
            mainMenuButton.enabled = true;
            resumeButton.enabled = true;
            mainMenuEvent.enabled = true;
            resumeEvent.enabled = true;
            
        }
        else if(paused == false)
        {
            player.GetComponent<Player>().activated = true;
            pauseScreen.SetActive(false);
            resumeButtonTransform.DOScale(1, 0.1f);
        }           
    }

    public void ResumeButtonEnter()
    {
        resumeButtonTransform.DOScale(1.2f, 0.3f);
    }
    public void ResumeButtonExit()
    {
        resumeButtonTransform.DOScale(1, 0.3f);
    }

    public void MainMenuButtonEnter()
    {
        mainMenuButtonTransform.DOScale(1.2f, 0.3f);
    }
    public void MainMenuButtonExit()
    {
        mainMenuButtonTransform.DOScale(1, 0.3f);
    }

    public void Resume()
    {
        resumeButtonTransform.DOScale(1.7f, 0.3f).OnComplete(Pause);
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        mainMenuButtonTransform.DOScale(1.7f, 0.5f).OnComplete(LoadCurrentScene);
        fadeImage.DOFade(1, 1f);
    }
    void LoadCurrentScene()
    {
        SceneManager.LoadScene(0);
    }
}