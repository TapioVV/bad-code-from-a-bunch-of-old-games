using UnityEngine;
using DG.Tweening;

public class WallBreakerMove : MonoBehaviour
{
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask wallLayer;
    [SerializeField] public LayerMask cubeLayer;

    public AudioSource audioS;
    public AudioSource audioS2;
    public AudioSource audioS3;

    public BoxCollider2D bxR;
    public BoxCollider2D bxU;
    public BoxCollider2D bxD;
    public BoxCollider2D bxL;

    float movingTime = 0;
    float movingTimeR = 0;

    public float moveSpeed;

    public int chosenSound;

    public bool moving;

    void Update()
    {
        PushCheck();
    }

    public void PushCheck()
    {
        float moveKeysX = Input.GetAxisRaw("Horizontal");
        float moveKeysY = Input.GetAxisRaw("Vertical");

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
