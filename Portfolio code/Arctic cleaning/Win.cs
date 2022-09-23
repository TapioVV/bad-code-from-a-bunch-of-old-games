using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Win : MonoBehaviour
{
    [SerializeField] LayerMask snowLayer;

    RaycastHit hit;

    public Image fadeImage;

    public int snowNeeded;
    int snowAmount;

    public AudioSource victorySound;

    public GameObject victoryText;

    public bool cheatyFix;

    bool active = true;
    bool won = false;

    public bool lastStage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && won == true) 
        {
            if(lastStage == true)
            {
                fadeImage.DOFade(1, 1).OnComplete(LoadMainMenu);
                won = false;
            }
            else
            {
                fadeImage.DOFade(1, 1).OnComplete(LoadNextScene);
                won = false;
            }
        }

        if (active == true)
        {
            if (cheatyFix == false)
            {
                if (Physics.Raycast(transform.position, transform.up, out hit, 1, snowLayer))
                {
                    snowAmount = hit.transform.gameObject.GetComponent<Snow>().snowCount;
                    if (snowAmount == snowNeeded && hit.transform.gameObject.GetComponent<Snow>().moving == false)
                    {
                        victoryText.SetActive(true);
                        victorySound.Play();
                        won = true;
                        active = false;
                    }
                }
            }

            if (cheatyFix == true)
            {
                if (Physics.Raycast(transform.position, transform.up, out hit, 1, snowLayer))
                {
                    snowAmount = hit.transform.gameObject.GetComponent<Snow>().snowCount;
                    if (snowAmount == snowNeeded)
                    {
                        victoryText.SetActive(true);
                        victorySound.Play();
                        won = true;
                        active = false;
                    }
                }
            }

        }
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
