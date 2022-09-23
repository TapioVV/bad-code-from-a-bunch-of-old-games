using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Image fadeImage;
    public RectTransform playButton;
    public RectTransform quitButton;

    private void Start()
    {
        fadeImage.DOFade(0, 4).SetEase(Ease.Linear);
    }

    public void PlayGame()
    {
        fadeImage.DOFade(1, 2.25f).SetEase(Ease.Linear).OnComplete(NextScene);
        playButton.DOScale(1.75f, 0.4f).SetLoops(2, LoopType.Yoyo) ;
    }

    public void PlayButtonMouseEnter()
    {
        playButton.DOScale(1.2f, 0.3f);
    }
    public void PlayButtonMouseExit()
    {
        playButton.DOScale(1, 0.3f);
    }
    public void QuitButtonMouseEnter()
    {
        quitButton.DOScale(1.2f, 0.3f);
    }
    public void QuitButtonMouseExit()
    {
        quitButton.DOScale(1, 0.3f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        fadeImage.DOFade(1, 2.25f).SetEase(Ease.Linear).OnComplete(Application.Quit);
        quitButton.DOScale(1.75f, 0.4f).SetLoops(2, LoopType.Yoyo);
    }
}
