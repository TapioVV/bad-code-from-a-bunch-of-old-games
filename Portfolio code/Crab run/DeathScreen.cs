using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject deathScreen;
    [SerializeField] TMP_Text scoreText;

    Spawner spawner;
    
    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        fadeImage.DOFade(0, 0.3f);       
    }
    void Update()
    {
        scoreText.text = spawner.intScore.ToString();
        if(deathScreen.activeInHierarchy == true)
        {
            spawner.gameEnd = true;
            spawner.scoreText.enabled = false;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void RetryButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }

    public void MainMenuButtonPress()
    {
        fadeImage.DOFade(1, 1).OnComplete(ToMainMenu);
    }
    void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
