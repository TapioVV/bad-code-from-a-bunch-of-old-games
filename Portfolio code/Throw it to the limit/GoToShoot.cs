using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShoot : MonoBehaviour
{
    [SerializeField] ThrowingAndStates tas;
    AudioSource audioS;
    [SerializeField] AudioClip throwSound;
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    public void ThrowTheItem()
    {
        tas.ToShoot();
    }
    public void GameOver()
    {
        tas.reallyGameOVer = true;
    }
    public void PlayThrowSound()
    {
        audioS.PlayOneShot(throwSound, audioS.volume);
    }
}
