using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, 7f);
    }
}
