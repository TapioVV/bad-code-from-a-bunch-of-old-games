using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpriteChange : MonoBehaviour
{
    [SerializeField] Sprite s1;
    [SerializeField] Sprite s2;
    [SerializeField] Sprite s3;
    [SerializeField] Sprite s4;
    [SerializeField] Sprite s5;
    [SerializeField] Sprite s6;

    Sprite[] sprites = new Sprite[6];

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sprites = new Sprite[] { s1, s2, s3, s4, s5, s6 };

        int randomSprite = Random.Range(0, 6);
        Debug.Log(randomSprite);

        sr.sprite = sprites[randomSprite];
    }
}
