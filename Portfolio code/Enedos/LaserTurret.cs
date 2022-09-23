using UnityEngine;
using DG.Tweening;

public class LaserTurret : MonoBehaviour
{
    [SerializeField] SpriteRenderer laserSR;
    [SerializeField] BoxCollider2D bx;

    [SerializeField] Sprite daySprite;
    [SerializeField] Sprite nightSprite;
    SpriteRenderer sr;

    GameManager gm;

    float laserTimeR;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (gm.nightState == true)
        {
            sr.sprite = nightSprite;
        }
        if (gm.nightState == false)
        {
            sr.sprite = daySprite;
        }
    }
    public void LaserTurretShoot(float laserTime)
    {
        laserTimeR = laserTime;
        laserSR.enabled = true;
        bx.enabled = true;
        DOTween.To(() => laserTimeR, x => laserTimeR = x, 0, laserTime).OnComplete(LaserShootEnd);
    }
    void LaserShootEnd()
    {
        laserSR.enabled = false;
        bx.enabled = false;
    }
}
