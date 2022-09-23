using UnityEngine;

public class SpearAudio : MonoBehaviour
{
    AudioSource audioS;
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlayStrikeSound()
    {
        audioS.Play();
    }
}
