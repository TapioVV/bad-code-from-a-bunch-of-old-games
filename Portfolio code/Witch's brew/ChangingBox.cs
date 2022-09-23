using UnityEngine;
using DG.Tweening;

public class ChangingBox : MonoBehaviour
{
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask wallLayer;
    [SerializeField] public LayerMask cubeLayer;

    public GameObject horizontalArrow;
    public GameObject verticalArrow;

    public AudioSource audioS;
    public AudioSource audioS2;
    public AudioSource audioS3;

    public AudioSource changingS;

    public BoxCollider2D bxR;
    public BoxCollider2D bxU;
    public BoxCollider2D bxD;
    public BoxCollider2D bxL;

    public bool horizontal;

    float movingTime = 0;
    float movingTimeR = 0;

    public float moveSpeed;

    public int chosenSound;

    public bool moving;

    void Update()
    {
        PushCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ArrowChange")
        {
            horizontal = !horizontal;
            changingS.Play();
        }
    }

    public void PushCheck()
    {
        float moveKeysX = Input.GetAxisRaw("Horizontal");
        float moveKeysY = Input.GetAxisRaw("Vertical");


        if(horizontal == true)
        {
            horizontalArrow.SetActive(true);
            verticalArrow.SetActive(false);

            if (!bxL.IsTouchingLayers(cubeLayer) && !bxL.IsTouchingLayers(wallLayer) && bxR.IsTouchingLayers(playerLayer) && moveKeysX == -1 && Input.GetKeyDown(KeyCode.Space) && moving == false)
            {
                transform.DOMoveX(transform.position.x - 1, moveSpeed);
                moving = true;
                DOTween.To(() => movingTime, x => movingTime = x, 1, moveSpeed);
                randomSound();
            }
            if (!bxR.IsTouchingLayers(cubeLayer) && !bxR.IsTouchingLayers(wallLayer) && bxL.IsTouchingLayers(playerLayer) && moveKeysX == 1 && Input.GetKeyDown(KeyCode.Space) && moving == false)
            {
                transform.DOMoveX(transform.position.x + 1, moveSpeed);
                moving = true;
                DOTween.To(() => movingTime, x => movingTime = x, 1, moveSpeed);
                randomSound();
            }
        }

        if (horizontal == false)
        {
            horizontalArrow.SetActive(false);
            verticalArrow.SetActive(true);


            if (!bxD.IsTouchingLayers(cubeLayer) && !bxD.IsTouchingLayers(wallLayer) && bxU.IsTouchingLayers(playerLayer) && moveKeysY == -1 && Input.GetKeyDown(KeyCode.Space) && moving == false)
            {
                transform.DOMoveY(transform.position.y - 1, moveSpeed);
                moving = true;
                DOTween.To(() => movingTime, x => movingTime = x, 1, moveSpeed);
                randomSound();
            }
            if (!bxU.IsTouchingLayers(cubeLayer) && !bxU.IsTouchingLayers(wallLayer) && bxD.IsTouchingLayers(playerLayer) && moveKeysY == 1 && Input.GetKeyDown(KeyCode.Space) && moving == false)
            {
                transform.DOMoveY(transform.position.y + 1, moveSpeed);
                moving = true;
                DOTween.To(() => movingTime, x => movingTime = x, 1, moveSpeed);
                randomSound();
            }
        }

        if (movingTime == 1)
        {
            moving = false;
            movingTime = movingTimeR;
        }
    }
    void randomSound()
    {
        chosenSound = Random.Range(1, 4);
        if (chosenSound == 1)
        {
            audioS.Play();
        }
        if (chosenSound == 2)
        {
            audioS2.Play();
        }
        if (chosenSound == 3)
        {
            audioS3.Play();
        }
    }
}
