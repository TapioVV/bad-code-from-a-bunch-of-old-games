using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public BoxCollider2D moveArea;

    [SerializeField] EnemyBullet eb;

    GameManager gm;

    public float shootTime;
    float shootTimeR;

    Rigidbody2D rb;

    [SerializeField] GameObject Gold;

    [SerializeField] float moveSpeed;

    [SerializeField] float radiusChangeSpeed;

    public float rotateSpeedMin;
    public float rotateSpeedMax;
    [SerializeField] float rotateSpeed;

    Animator anim;

    bool canCollide = true;

    int randomDir;
    bool toggleDir = true;

    bool canDie = true;

    public int myId;

    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource explosionSound;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        shootTime = gm.eShootTime;
        rotateSpeedMax = gm.eMaxRotSpeed;
        rotateSpeedMin = gm.eMinRotSpeed;

        anim = GetComponent<Animator>();

        shootTimeR = shootTime;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.zero - transform.position, ForceMode2D.Impulse);
    }
    void Update()
    {
        if(canDie == true)
        {
            var dir = moveArea.bounds.center - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (canDie == false)
        {
            var dir = moveArea.bounds.center - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (canDie == true)
        {
            if(shootTimeR > 0)
            {
                shootTimeR -= Time.deltaTime;
            }
            if(shootTimeR <= 0)
            {
                Instantiate(eb, transform.position, transform.rotation, transform.parent = null);
                shootSound.Play();
                shootTimeR = shootTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeRadius();
        }
    }

    public void StopAllTweens()
    {
        DOTween.Kill(myId);
    }

    void MoveOrRotate()
    {
        int randomNumber = Random.Range(0, 2);

        randomDir = Random.Range(0, 2);

        if (randomNumber == 0)
        {
            ChangeRadius();
        }
        if (randomNumber == 1)
        {
            Rotate();
        }
    }
    void ChangeRadius()
    {
        toggleDir = !toggleDir;
        float radiusChangeNumber = 3;    

        DOTween.To(() => radiusChangeNumber, x => radiusChangeNumber = x, 0, 1).OnComplete(MoveOrRotate).OnUpdate(ActuallyMove).SetId(myId);
    }
    void ActuallyMove()
    {
        if (toggleDir == false)
        {
            transform.position += -transform.up * 5 * Time.deltaTime;
        }
        if (toggleDir == true)
        {
            transform.position += transform.up * 5 * Time.deltaTime;
        }       
    }
    void Rotate()
    {
        float rotateTimer = Random.Range(1, 4);
        float rotateNumber = rotateTimer;

        rotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);

        DOTween.To(() => rotateNumber, x => rotateNumber = x, 0, rotateTimer).OnComplete(MoveOrRotate).OnUpdate(ActuallyRotate).SetId(myId);
    }
    void ActuallyRotate()
    {
        if (randomDir == 0)
        {
            transform.RotateAround(moveArea.bounds.center, Vector3.forward, -rotateSpeed * Time.deltaTime);
        }
        if (randomDir == 1)
        {
            transform.RotateAround(moveArea.bounds.center, Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if(collision.gameObject.tag == "CenterArea" && canCollide == true && canDie == true)
        {
            rb.velocity = Vector2.zero;
            ChangeRadius();
            canCollide = false;
        }
        if(collision.gameObject.tag == "Planet")
        {
            Instantiate(Gold, transform.position, transform.rotation =  Quaternion.identity, transform.parent = GameObject.FindGameObjectWithTag("Planet").transform);
            rb.velocity = Vector2.zero;
            explosionSound.Play();
            anim.Play("Explosion");
        }
        if (collision.gameObject.tag == "Bullet" && canDie == true)
        {           
            Die();
        }      
    }
    void Die()
    {
        StopAllTweens();
        deathSound.Play();
        rb.AddForce(Vector3.zero - transform.position, ForceMode2D.Impulse);
        canDie = false;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    
}
