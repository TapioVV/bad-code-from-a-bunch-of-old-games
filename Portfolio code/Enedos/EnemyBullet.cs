using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float shotSpeed;

    Planet planet;

    Rigidbody2D rb;
    void Start()
    {
        planet = FindObjectOfType<Planet>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.zero - transform.position * shotSpeed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Planet")
        {
            planet.TakeDamage();
            Destroy(gameObject);
        }
    }
}
