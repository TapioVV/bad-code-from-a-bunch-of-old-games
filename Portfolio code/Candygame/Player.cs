using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
    public ParticleSystem ps;

    public CinemachineVirtualCamera cmCam;
    CinemachineBasicMultiChannelPerlin cmN;

    public float cameraShake;
    float cameraShakeR;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    public Image fadeImage;

    RaycastHit2D[] hitEnemies;

    public GameObject strikeStartPos;

    public AudioSource jumpSound;
    public AudioSource walkSound;

    public AudioSource gravityChangeSound;
    public AudioSource hitSound;
    public AudioSource deathSound;

    Animator playerAnimator;
    public Animator spearAnim;

    Rigidbody2D rb;
    BoxCollider2D bx;
    SpriteRenderer sr;

    public BoxCollider2D bxL;
    public BoxCollider2D bxR;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    float jumpForceNormal;
    float jumpForceReverse;
    [SerializeField] float gravityScale;

    bool changeGravity;

    float xInput;

    public bool jumped;

    public int health;

    public float knockBackLenght;
    public float knockBackHeight;

    public float invicibilityTimer;
    float invicibilityTimerR;

    public float strikeTimer;
    float strikeTimerR;
    bool struck;

    bool struckRight;
    bool struckLeft;

    public float jumpTimer;
    public float jumpTimerR;

    public float attackRange;

    bool damaged;
    bool died = false;

    void Start()
    {
        cmN = cmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        rb = GetComponent<Rigidbody2D>();
        bx = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();

        rb.gravityScale = gravityScale;

        jumpForceNormal = jumpForce;
        jumpForceReverse = -jumpForce;
    }

    void Update()
    {
        cmN.m_AmplitudeGain = cameraShakeR;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
        xInput = Input.GetAxisRaw("Horizontal");

        if(health > 0)
        {
            Movement();
            Flip();
            Animations();
            if (invicibilityTimerR >= 0)
            {
                invicibilityTimerR -= Time.deltaTime;
            }
            else
            {
                Strike();
            }
        }
        if (health <= 0 && died == false)
        {
            deathSound.Play();
            playerAnimator.Play("DonutDeath");
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            fadeImage.DOFade(1, 2).OnComplete(Die);
            died = true;
        }

    }
    private void FixedUpdate()
    {
        DamagedCheck();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            health -= 10;
            sr.DOColor(Color.red, 0.05f).SetLoops(2, LoopType.Yoyo);
            ScreenShake(cameraShake);
        }
        if (collision.gameObject.tag == "Win")
        {
            NextLevel();
        }

    }

    void NextLevel()
    {
        fadeImage.DOFade(1, 2).OnComplete(LoadNextLevel);
    }
    void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool Grounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(bx.bounds.center, bx.bounds.size, 0, -transform.up, 0.05f, groundLayer);
        return hit.collider != null;
    }
    
    void DamagedCheck()
    {
        if(invicibilityTimerR <= 0)
        {
                if (Physics2D.BoxCast(bxL.bounds.center, bxL.bounds.size, 0, -transform.right, 0.1f, enemyLayer))
                {
                    ThingsOnDamaged();
                    if(changeGravity == true)
                    {
                        rb.velocity = new Vector2(-knockBackLenght, -knockBackHeight);
                    }
                    if(changeGravity == false)
                    {
                        rb.velocity = new Vector2(knockBackLenght, knockBackHeight);
                    }
                }
                if (Physics2D.BoxCast(bxR.bounds.center, bxR.bounds.size, 0, transform.right, 0.1f, enemyLayer))
                {
                    ThingsOnDamaged();
                    if (changeGravity == true)
                    {
                        rb.velocity = new Vector2(knockBackLenght, -knockBackHeight);
                    }
                    if (changeGravity == false)
                    {
                        rb.velocity = new Vector2(-knockBackLenght, knockBackHeight);
                    }
                }          
        }     
    }

    
    void ThingsOnDamaged()
    {
        ps.Play();
        hitSound.Play();
        invicibilityTimerR = invicibilityTimer;
        damaged = true;
        jumped = true;
        health -= 1;
        sr.DOColor(Color.red, 0.05f).SetLoops(2, LoopType.Yoyo);
        ScreenShake(cameraShake);
    }
    void Movement()
    {
        if (jumpTimerR > 0)
        {
            jumpTimerR -= Time.deltaTime;
        }

        if (Grounded() && strikeTimerR <= 0)
        {
            jumped = false;
            damaged = false;

            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Z))
            {
                ToggleGravity();
                gravityChangeSound.Play();
            }
            if (Input.GetButtonDown("Jump"))
            {
                jumpTimerR = jumpTimer;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpSound.Play();
            }
        }

        if(jumpTimerR > 0)
        {
            jumped = true;
        }
        if (!Grounded() && jumped == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void ToggleGravity()
    {
        changeGravity = !changeGravity;

        if(changeGravity == true)
        {
            sr.flipX = !sr.flipX;
            jumpForce = jumpForceReverse;
            rb.gravityScale = -gravityScale;
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if(changeGravity == false)
        {
            sr.flipX = !sr.flipX;
            jumpForce = jumpForceNormal;
            rb.gravityScale = gravityScale;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void Animations()
    {
        if (Grounded())
        {
            if (xInput != 0 && strikeTimerR <= 0)
            {
                playerAnimator.Play("DonutWalk");
            }
            else
            {
                playerAnimator.Play("DonutIdle");
            }
        }

        if (!Grounded() && damaged == true)
        {
            playerAnimator.Play("DonutDamaged");
        }
        else if(!Grounded())
        {
            playerAnimator.Play("DonutJump");
        }
    }

    public void WalkSound()
    {
        walkSound.Play();
    }

    void Strike()
    {
        if(strikeTimerR > 0)
        {
            strikeTimerR -= Time.deltaTime;

            if(struckRight == true)
            {             
                Debug.DrawRay(strikeStartPos.transform.position, transform.right * attackRange, Color.green);

                hitEnemies = Physics2D.RaycastAll(strikeStartPos.transform.position, transform.right, attackRange, enemyLayer);
                for (int i = 0; i < hitEnemies.Length; i++)
                {
                    RaycastHit2D hitEnemy = hitEnemies[i];
                    GameObject enemyHitGameObject = hitEnemy.transform.gameObject;

                    if (enemyHitGameObject.GetComponent<TriangleEnemy>())
                    {
                        enemyHitGameObject.GetComponent<TriangleEnemy>().GetHit();
                    }
                    if (enemyHitGameObject.GetComponent<SkeletonEnemy>())
                    {
                        enemyHitGameObject.GetComponent<SkeletonEnemy>().GetHit();
                    }
                }
            }
            if (struckLeft == true)
            {
                Debug.DrawRay(strikeStartPos.transform.position, -transform.right * attackRange, Color.green);

                hitEnemies = Physics2D.RaycastAll(strikeStartPos.transform.position, -transform.right, attackRange, enemyLayer);
                for (int i = 0; i < hitEnemies.Length; i++)
                {
                    RaycastHit2D hitEnemy = hitEnemies[i];
                    GameObject enemyHitGameObject = hitEnemy.transform.gameObject;
                    
                    if (enemyHitGameObject.GetComponent<TriangleEnemy>())
                    {
                        enemyHitGameObject.GetComponent<TriangleEnemy>().GetHit();
                    }
                    if (enemyHitGameObject.GetComponent<SkeletonEnemy>())
                    {
                        enemyHitGameObject.GetComponent<SkeletonEnemy>().GetHit();                     
                    }
                }
            }
        }

        if(strikeTimerR <= 0)
        {
            if(sr.flipX == false)
            {
                spearAnim.Play("SpearIdle");
            }
            if(sr.flipX == true)
            {
                spearAnim.Play("SpearIdleL");
            }

            struckRight = false;
            struckLeft = false;

            if (Input.GetKeyDown(KeyCode.X) && sr.flipX == false)
            {
                struck = true;
                spearAnim.Play("HitAnimationR");

                struckRight = true;

                strikeTimerR = strikeTimer;
            }
            if (Input.GetKeyDown(KeyCode.X) && sr.flipX == true)
            {
                struck = true;
                spearAnim.Play("HitAnimationL");

                struckLeft = true;

                strikeTimerR = strikeTimer;
            }
        }
        if (Grounded() && struck == true)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            struck = false;
        }
    }

    public void ScreenShake(float shakeAmount)
    {
        DOTween.To(() => cameraShakeR, x => cameraShakeR = x, shakeAmount, 0.15f).SetLoops(2, LoopType.Yoyo);
    }

    void Flip()
    {
        if (strikeTimerR <= 0)
        {
            if (Grounded() && xInput == 1)
            {
                sr.flipX = false;
            }
            if (Grounded() && xInput == -1)
            {
                sr.flipX = true;
            }

            if (Grounded() && xInput == 1 && changeGravity == true)
            {
                sr.flipX = true;
            }
            if (Grounded() && xInput == -1 && changeGravity == true)
            {
                sr.flipX = false;
            }
        }
    }
}
