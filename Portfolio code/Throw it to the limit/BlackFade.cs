using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlackFade : MonoBehaviour
{
    [SerializeField] Image fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage.DOFade(0, 1);
    }
}
