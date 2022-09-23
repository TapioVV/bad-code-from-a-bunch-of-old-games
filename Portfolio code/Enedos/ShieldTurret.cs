using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTurret : MonoBehaviour
{
    [SerializeField] Sprite daySprite;
    [SerializeField] Sprite nightSprite;
    SpriteRenderer sr;

    GameManager gm;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
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
}
