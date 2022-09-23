using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public bool shootRight;
    public ParticleSystem ps;

    CircleCollider2D cc;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float bulletSpeed;

    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();

        int randomNumber = Random.Range(1, 4);
       
        if(randomNumber == 1)
        {
            sr.color = Color.yellow;
            ps.startColor = Color.yellow;
        }
        if (randomNumber == 2)
        {
            sr.color = new Color(0, 0.5f, 0, 1);
            ps.startColor = new Color(0, 0.5f, 0, 1);
        }
        if (randomNumber == 3)
        {
            sr.color = Color.red;
            ps.startColor = Color.red;
        }

        if (shootRight == true)
        {
            rb.velocity = Vector2.right * bulletSpeed;
            if (transform != null)
            {
                transform.DORotate(new Vector3(0, 0, 180), Random.Range(0.2f, 0.5f), RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
        }
        if(shootRight == false)
        {
            rb.velocity = Vector2.left * bulletSpeed;
            if(transform != null)
            {
                transform.DORotate(new Vector3(0, 0, 180), Random.Range(0.2f, 0.5f), RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
        }
        Destroy(gameObject, 20);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ps.transform.parent = null;
            ps.Play();
            sr.enabled = false;
            cc.enabled = false;
            rb.velocity = Vector2.zero;
            Destroy(ps.gameObject, 1);
            Destroy(gameObject, 1.1f);
            
        }
    }
}
