using UnityEngine;
public class Player : MonoBehaviour
{
    enum STATE { SWIMMING, CAUGHT, STRIKING, DEAD }
    STATE currentState;

    [SerializeField] LayerMask woodWallLayer;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    SpriteRenderer sr;
    [SerializeField] GameObject deathScreen;
    [SerializeField] float _jumpForce;
    [SerializeField] float _gravityAmount;
    [SerializeField] float _caughtSpeed;
    [SerializeField] float _moveSpeed;
    public bool onFloor;

    Animator anim;
    int pressCount = 3;

    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip jumpingSound;
    [SerializeField] AudioClip actionSound;
    [SerializeField] AudioClip plasticBagSound;

    AudioSource audioSource;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource deathMusic;

    Spawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        StateChange();
    }

    void StateChange()
    {
        switch (currentState)
        {
            case STATE.SWIMMING:
                Swimming();
                break;
            case STATE.CAUGHT:
                Caught();
                break;
            case STATE.STRIKING:
                Striking();
                break;
            case STATE.DEAD:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    void Swimming()
    {
        pressCount = 3;

        rb.gravityScale = _gravityAmount;  
        
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, rb.velocity.y);

        if(onFloor == true)
        {
            anim.Play("Walk");
        }
        else if(onFloor == false)
        {
            anim.Play("Swim");
        }

        if (Input.GetButtonDown("Jump"))
        {
            audioSource.PlayOneShot(jumpingSound, 1f);
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }

        if (Input.GetButtonDown("Action"))
        {
            audioSource.Stop();
            currentState = STATE.STRIKING;
        }
    }

    void Caught()
    {        
        rb.velocity = new Vector2(-_caughtSpeed - (spawner.Speed / 5), 0);
        rb.gravityScale = 0;
        if (Input.GetButtonDown("Action"))
        {
            anim.SetTrigger("PlasticBagAction");
            audioSource.PlayOneShot(plasticBagSound, 0.5f);
            pressCount--;
        }
        if(pressCount <= 0)
        {
            audioSource.Play();
            currentState = STATE.SWIMMING;
        }
    }

    void Striking()
    {
        anim.Play("Strike");
        audioSource.PlayOneShot(actionSound, 0.03f);

        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.right, 2, woodWallLayer);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<WoodWall>().DestroyWall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlasticBag" && currentState == STATE.SWIMMING || collision.tag == "PlasticBag" && currentState == STATE.STRIKING)
        {
            audioSource.Stop();
            anim.Play("PlasticBag");
            currentState = STATE.CAUGHT;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound, 1f);
            sr.enabled = false;
            deathScreen.SetActive(true);
            if(spawner.intScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", spawner.intScore);
            }
            music.volume = 0;
            deathMusic.volume = 1;

            currentState = STATE.DEAD;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") && currentState == STATE.SWIMMING)
        {
            onFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && currentState == STATE.SWIMMING)
        {
            onFloor = false;
        }
    }

    public void FromStrikeToSwimming()
    {
        if(currentState == STATE.STRIKING)
        {
            audioSource.Play();
            currentState = STATE.SWIMMING;
        }
    }
}
