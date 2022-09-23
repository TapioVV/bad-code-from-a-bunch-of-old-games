using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image fadePanelM;
    public GameObject credits;
    public GameObject menuButtons;
    private Animation anim;

    bool inCredits = false;
    

    void Start()
    {
        fadePanelM.DOFade(0, 1.5f);
        anim = credits.GetComponent<Animation>();
    }

    void Update()
    {
        if(inCredits == true && Input.GetKeyDown(KeyCode.Mouse0)|| inCredits == true && Input.GetKeyDown(KeyCode.Escape))
        {
            anim.Stop();
            credits.SetActive(false);
            menuButtons.SetActive(true);

            inCredits = false;
        }
    }

    public void Startgame()
    {
        fadePanelM.DOFade(1, 1);
        StartCoroutine(Starting());
       
        IEnumerator Starting()
        {
            yield return new WaitForSecondsRealtime(1.1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScreen()
    {
        menuButtons.SetActive(false);
        credits.SetActive(true);
        anim.Play();
        inCredits = true;
    }
}
