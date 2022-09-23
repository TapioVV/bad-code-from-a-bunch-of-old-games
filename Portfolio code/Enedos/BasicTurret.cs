using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    [SerializeField] Sprite daySprite;
    [SerializeField] Sprite nightSprite;
    SpriteRenderer sr;

    GameManager gm;

    [SerializeField] BasicBullet bbPrefab;
    BasicBullet bb;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager>();
    }

    public void BasicTurretShoot(float bulletSpeed)
    {
           bb = Instantiate(bbPrefab, transform.position, transform.rotation);
           bb.rb.velocity = transform.up * bulletSpeed;
    }

    private void Update()
    {
        if(gm.nightState == true)
        {
            sr.sprite = nightSprite;
        }
        if (gm.nightState == false)
        {
            sr.sprite = daySprite;
        }
    }
}
