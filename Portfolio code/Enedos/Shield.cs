using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float shieldTime;
    float shieldTimeR;

    EdgeCollider2D ec;
    SpriteRenderer sr;

    [SerializeField] AudioSource deactivateSound;
    [SerializeField] AudioSource activateSound;


    bool soundPlayed;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ec = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        if(shieldTimeR > 0)
        {
            shieldTimeR -= Time.deltaTime;
            sr.enabled = false;
            ec.enabled = false;
        }
        if(shieldTimeR <= 0)
        {
            sr.enabled = true;
            ec.enabled = true;

            if(soundPlayed == false)
            {
                activateSound.Play();
                soundPlayed = true;
            }
        }
    }

    void ShieldTimer()
    {
        shieldTimeR = shieldTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            ShieldTimer();
            soundPlayed = false;
            deactivateSound.Play();
        }
    }
}
