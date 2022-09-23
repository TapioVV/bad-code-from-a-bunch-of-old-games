using DG.Tweening;
using UnityEngine;
public class SkeletonEnemy : MonoBehaviour
{
    public ParticleSystem ps;

    public float camShakeAmount;

    SpriteRenderer sr;

    public Transform shootPosNormal;
    public Transform shootPosReverse;
    Transform shootPos;

    public AudioSource damagedSound;
    public AudioSource shootSound;

    public GameObject bullet;

    GameObject player;

    Animator anim;

    public int health;
    public float stunTimer;
    float stunTimerR;

    public float shootTimer;
    float shootTimerR;

    public bool yFlip;
    public bool lookRight;

    bool idle = false;
    bool dead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (lookRight == true)
        {
            sr.flipX = false;
        }
        if (lookRight == false)
        {
            sr.flipX = true;
        }

        if(sr.flipY == false)
        {
            shootPos = shootPosNormal;
        }
        if (sr.flipY == true)
        {
            shootPos = shootPosReverse;
        }
    }

    void Update()
    {
        if(health > 0)
        {
            if(idle == true)
            {
                anim.Play("Idle");
            }

            if (stunTimerR >= 0)
            {
                stunTimerR -= Time.deltaTime;
            }

            if (shootTimerR > 0)
            {
                shootTimerR -= Time.deltaTime;
            }

            if (shootTimerR <= 0)
            {
                anim.Play("Shoot");
            }
        }
     
        if (health <= 0 && dead == false)
        {
            anim.Play("Dead");
            sr.DOColor(Color.red, 0.5f).OnComplete(Die);
            dead = true;  
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void ShootBullet()
    {
        shootSound.Play();
        GameObject shotBullet = Instantiate(bullet, shootPos.transform.position, Quaternion.identity);

        if (lookRight == true)
        {
            shotBullet.GetComponent<Bullet>().shootRight = true;
        }
        if (lookRight == false)
        {
            shotBullet.GetComponent<Bullet>().shootRight = false;
        }

        shootTimerR = shootTimer;
    }

    public void trueIdle()
    {
        idle = true;
    }
    public void falseIdle()
    {
        idle = false;
    }

    public void GetHit()
    {
        if (stunTimerR <= 0)
        {
            ps.Play();
            player.GetComponent<Player>().ScreenShake(camShakeAmount);
            damagedSound.Play();
            sr.DOColor(Color.red, 0.05f).SetLoops(2, LoopType.Yoyo).OnComplete(trueIdle);
            anim.Play("Damaged");
            health -= 1;
            stunTimerR = stunTimer;
        }
    }
}
