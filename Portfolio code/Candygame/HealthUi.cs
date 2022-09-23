using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    public GameObject healthImage1;
    public GameObject healthImage2;
    public GameObject healthImage3;

    GameObject player;
    int healthNumber;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        healthNumber = player.GetComponent<Player>().health;
        if(healthNumber <= 2)
        {
            healthImage3.SetActive(false);
        }
        if (healthNumber <= 1)
        {
            healthImage2.SetActive(false);
        }
        if (healthNumber <= 0)
        {
            healthImage1.SetActive(false);
        }
    }
}
