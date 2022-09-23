using UnityEngine;

public class Parallax : MonoBehaviour
{
    Rigidbody2D rb;

    Vector3 endPosition;
    Vector3 startPosition;

    public Transform startPositionTransform;
    public Transform endPositionTransfrom;

    [SerializeField] float parallaxEffect;
    float speed;

    Spawner spawner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = startPositionTransform.position;
        endPosition = new Vector2(endPositionTransfrom.position.x, transform.position.y);
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        if(spawner != null)
        {
            speed = spawner.Speed / parallaxEffect * Time.deltaTime;
        }
        else
        {
            speed = parallaxEffect * Time.deltaTime;
        }

        if (transform.position.x <= endPosition.x)
        {
            transform.position += startPosition - endPosition;
        }

        transform.Translate(-Vector2.right * speed, Space.World);
    }
}
