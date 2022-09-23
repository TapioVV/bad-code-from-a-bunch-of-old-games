using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuSounds : MonoBehaviour
{
    public AudioSource wave1;
    public AudioSource wave2;
    public AudioSource wave3;
    public AudioSource wave4;

    public AudioSource music;

    float Timer;
    float TimeR;

    public float randomTimerMin;
    public float randomTimerMax;

    private void Start()
    {
        TimeR = Timer;
        RandomSound();
    }
    void Update()
    {
        if(TimeR > 0)
        {
            TimeR -= Time.deltaTime;
        }
        if(TimeR <= 0)
        {
            RandomSound();
            TimeR = Timer;
        }
    }
    void RandomSound()
    {
        int randomNumber = Random.Range(1, 5);
        Timer = Random.Range(randomTimerMin, randomTimerMax);

        if(randomNumber == 1)
        {
            wave1.Play();
        }
        if (randomNumber == 2)
        {
            wave2.Play();
        }
        if (randomNumber == 3)
        {
            wave3.Play();
        }
        if (randomNumber == 4)
        {
            wave4.Play();
        }
    }
    public void VolumeDown()
    {
            wave1.DOFade(0, 3).SetEase(Ease.Linear);
            wave2.DOFade(0, 3).SetEase(Ease.Linear);
            wave3.DOFade(0, 3).SetEase(Ease.Linear);
            wave1.DOFade(0, 3).SetEase(Ease.Linear);
            music.DOFade(0, 3).SetEase(Ease.Linear);
    }
}
