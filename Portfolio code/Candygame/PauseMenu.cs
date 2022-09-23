using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
    public Image fadeImage;
    public GameObject pauseMenu;
    bool paused;


    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = !paused;

        if (paused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        if(paused == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ToMainMenu()
    {
        fadeImage.DOFade(1, 1).OnComplete(LoadMainMenu).SetUpdate(UpdateType.Normal, true);
    }
    void LoadMainMenu()
    {
        PauseGame();
        SceneManager.LoadScene(0);
    }
}
