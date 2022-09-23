using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
