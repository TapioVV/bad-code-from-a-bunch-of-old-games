using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class TriangleEnemy : MonoBehaviour
{
    public float camShakeAmount;

    public ParticleSystem ps;

    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D bx;

    Animator anim;

    public AudioSource deathSound;
    public AudioSource damagedSound;

    public int health;
    public float stunTimer;
    float stunTimerR;

    public GameObject wallCheckR;
    public GameObject groundCheckR;
    public GameObject wallCheckL;
    public GameObject groundCheckL;

    GameObject player;

    public bool reverseGravity;

    public float moveSpeed;

    public bool flipped;

    bool dead = false;
    void Start()
    {
        bx = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        GravityCheck();
    }

    void Update()
    {
        if (health > 0)
        {
            if (stunTimerR >= 0)
            {
                rb.velocity = Vector2.zero;
                stunTimerR -= Time.deltaTime;
                anim.Play("TriangleEnemyDamaged");
            }
            else
            {
                bx.enabled = true;
                anim.Play("TriangleEnemyWalk");
                Move();
            }
        }

        if(health <= 0 && dead == false)
        {
            bx.enabled = false;
            deathSound.Play();
            rb.velocity = Vector2.zero;
            anim.Play("TriangleEnemyDeath");
            sr.DOColor(Color.red, 0.5f).OnComplete(Die);
            dead = true;
        }
    }
    private void FixedUpdate()
    {
        Flip();
    }
    void Die()
    {
        Destroy(gameObject);
    }


    public void GetHit()
    {
        if(stunTimerR <= 0)
        {
            ps.Play();
            bx.enabled = false;
            player.GetComponent<Player>().ScreenShake(camShakeAmount);
            damagedSound.Play();
            sr.DOColor(Color.red, 0.05f).SetLoops(2, LoopType.Yoyo);
            health -= 1;
            stunTimerR = stunTimer;
        }
    }

    void Move()
    {
        if(flipped == false)
        {
            rb.velocity = transform.right * moveSpeed;
        }
        if (flipped == true)
        {
            rb.velocity = -transform.right * moveSpeed;
        }
    }

    void Flip()
    {
        if(flipped == false)
        {
            if (Physics2D.Raycast(wallCheckR.transform.position, -transform.up, 0.2f, groundLayer) || !Physics2D.Raycast(groundCheckR.transform.position, -transform.up, 0.4f, groundLayer))
            {
                flipped = !flipped;
                sr.flipX = !sr.flipX;
            }
        }
        if(flipped == true)
        {
            if (Physics2D.Raycast(wallCheckL.transform.position, -transform.up, 0.2f, groundLayer) || !Physics2D.Raycast(groundCheckL.transform.position, -transform.up, 0.4f, groundLayer))
            {
                flipped = !flipped;
                sr.flipX = !sr.flipX;
            }
        }
    }

    void GravityCheck()
    {
        if(reverseGravity == true)
        {
            rb.gravityScale = -1;
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (reverseGravity == false)
        {
            rb.gravityScale = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
