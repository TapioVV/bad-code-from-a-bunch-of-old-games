using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{
    AudioSource audioS;
    BoxCollider2D bx;
    SpriteRenderer sr;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BreakingPot")
        {
            audioS.Play();
            Destroy(collision.gameObject);
            sr.enabled = false;
            bx.enabled = false;
            Destroy(gameObject, 2);
        }
        
    }
}
