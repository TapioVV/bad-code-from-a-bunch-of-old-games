using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class End : MonoBehaviour
{
    Image fade;

    GameObject win;
    AudioSource audioS;

    private void Start()
    {
        fade = Image.FindObjectOfType<Image>();
        fade.DOFade(0, 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Win")
        {

            win = collision.gameObject;
            audioS = win.GetComponent<AudioSource>();
            audioS.Play();
            fade.DOFade(1, 1);
            StartCoroutine(Starting());

            IEnumerator Starting()
            {
                yield return new WaitForSecondsRealtime(1.1f);
                SceneManager.LoadScene(0);
            }
        }
    }
}