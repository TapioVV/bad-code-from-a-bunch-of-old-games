using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public Image fadeImage;
    public AudioMixer am;
    public Slider s;
    public VideoPlayer vp;

    public AudioSource menuMusic;

    public GameObject asd;

    public GameObject inGameMusic;
    GameObject duplicateInGameMusic;

    void Start()
    {
        s.value = PlayerPrefs.GetFloat("savedVolume");
        fadeImage.DOFade(0, 1);
        duplicateInGameMusic = GameObject.FindGameObjectWithTag("Music");
        if(duplicateInGameMusic != null)
        {
            Destroy(duplicateInGameMusic);
        }
    }

    public void PlayGamePress()
    {
        menuMusic.DOFade(0, 0.5f);
        vp.Play();
        asd.SetActive(false);
        vp.loopPointReached += PlayGame;
    }
    void PlayGame(VideoPlayer vp)
    {
        
        fadeImage.DOFade(1, 1).OnComplete(LoadNextScene);
    }
    void LoadNextScene()
    {
        GameObject inGameMusicPlay = Instantiate(inGameMusic);
        DontDestroyOnLoad(inGameMusicPlay);
        inGameMusicPlay.GetComponent<AudioSource>().Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGameButtonPress()
    {
        fadeImage.DOFade(1, 1).OnComplete(QuitGame);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        PlayerPrefs.SetFloat("savedVolume", volume);
        am.SetFloat("Volume", PlayerPrefs.GetFloat("savedVolume"));
    }
}
