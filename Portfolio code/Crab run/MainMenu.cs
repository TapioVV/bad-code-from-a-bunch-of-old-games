using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Image fadeImage;

    [SerializeField] RectTransform infoStartPos;
    RectTransform infoEndPos;
    [SerializeField] RectTransform infoPos;

    bool infoPressed;
    private void Start()
    {
        infoEndPos = infoPos;
        infoPos.position = infoStartPos.position;
        fadeImage.DOFade(0, 1);
    }

    public void SetVolume(float sliderValue)
    {
        Debug.Log(sliderValue);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    //Fades to black and then loads the next scene
    public void PlayButtonPress()
    {
        fadeImage.DOFade(1, 1).OnComplete(LoadNextScene);
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Fades to black and then exits the game
    public void QuitButtonPress()
    {
        fadeImage.DOFade(1, 1).OnComplete(QuitGame);
    }
    void QuitGame()
    {
        Application.Quit();
    }

    public void InfoButton()
    {
        Debug.Log("a");
        infoPressed = !infoPressed;
        if(infoPressed == true)
        {
            infoPos.DOAnchorPosY(450, 1f);
            Debug.Log("b");
        }
        if(infoPressed == false)
        {
            infoPos.DOAnchorPosY(infoStartPos.anchoredPosition.y, 1f);
            Debug.Log("c");
        }
    }
}
