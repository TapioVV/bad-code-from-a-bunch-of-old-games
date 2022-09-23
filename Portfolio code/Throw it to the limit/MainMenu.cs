using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Audio;

// MainMenu code;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    [SerializeField] float infoMenuSpeed;
    [SerializeField] GameObject infoMenu;
    bool moved = false;

    [SerializeField] TMP_Text highScoreText;

    Vector2 infoMenuEndPosition;
    Vector2 infoMenuStartPosition;
    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        fadeImage.DOFade(0, 1);
        infoMenuEndPosition = infoMenu.transform.position;
        infoMenuStartPosition = new Vector2(infoMenu.transform.position.x - 1100, infoMenu.transform.position.y);
        infoMenu.transform.position = infoMenuStartPosition;
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

    public void InfoMenuMove()
    {
        moved = !moved;
        if(moved == false)
        {
            infoMenu.transform.DOMove(infoMenuStartPosition, infoMenuSpeed);
        }
        if(moved == true)
        {
            infoMenu.transform.DOMove(infoMenuEndPosition, infoMenuSpeed);
        }
    }

    //Fades to black and then exits the game;
    public void QuitButtonPress()
    {      
        fadeImage.DOFade(1, 1).OnComplete(QuitGame);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
