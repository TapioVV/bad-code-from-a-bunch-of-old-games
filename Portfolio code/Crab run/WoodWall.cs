using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWall : MonoBehaviour
{
    SpriteRenderer sr;
    ParticleSystem ps;
    BoxCollider2D bx;

    AudioSource audioSource;
    [SerializeField] AudioClip breakSound;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        bx = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void DestroyWall()
    {
        bx.enabled = false;
        sr.enabled = false;
        audioSource.PlayOneShot(breakSound, 1);
        ps.Play();
    }
}
