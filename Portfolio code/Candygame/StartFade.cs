using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{
    public Image fadeImage;
    void Start()
    {
        fadeImage.DOFade(0, 1);
    }
}
