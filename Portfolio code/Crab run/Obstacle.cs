using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Spawner spawner;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        float speed = spawner.Speed * Time.deltaTime;
        transform.Translate(-Vector2.right * speed, Space.World);
    }
}
