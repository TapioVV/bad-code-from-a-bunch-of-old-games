using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaveSounds : MonoBehaviour
{
    float Timer;
    float TimeR;

    public AudioSource wave1;
    public AudioSource wave2;
    public AudioSource wave3;
    public AudioSource wave4;

    public float randomTimerMin;
    public float randomTimerMax;

    private void Start()
    {
        TimeR = 1.5f;
    }

    void Update()
    {
        if (TimeR > 0)
        {
            TimeR -= Time.deltaTime;
        }
        if (TimeR <= 0)
        {
            RandomSound();
            TimeR = Timer;
        }
    }
    void RandomSound()
    {
        int randomNumber = Random.Range(1, 5);
        Timer = Random.Range(randomTimerMin, randomTimerMax);

        if (randomNumber == 1)
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
}
