using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] Transform center;

    [SerializeField] PlaceObject po;

    [SerializeField] BoxCollider2D bx;

    AudioSource aS;

    [SerializeField] AudioSource scrapCollectAudio;

    Animator anim;

    SpriteRenderer sr;

    public bool activated;

    private void Start()
    {
        bx = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        aS = GetComponent<AudioSource>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        if(inputX > 0)
        {
            sr.flipX = false;
        }
        if (inputX < 0)
        {
            sr.flipX = true;
        }
        if(inputX != 0)
        {
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }

        if (activated == true)
        {
            bx.enabled = true;
            transform.RotateAround(center.position, Vector3.forward, -inputX * moveSpeed * Time.deltaTime);
        }     
        if(activated == false)
        {
            bx.enabled = false;
        }
    }

    public void WalkSoundPlay()
    {
        if(activated == true)
        {
            aS.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gold")
        {
            po.Money += 5;
            scrapCollectAudio.Play();
            Destroy(collision.gameObject);
        }
    }
}
