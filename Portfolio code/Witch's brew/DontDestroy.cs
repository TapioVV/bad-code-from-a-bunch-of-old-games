using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public Scene sc;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        sc = SceneManager.GetActiveScene();
        if (sc.buildIndex == 0)
        {
            Debug.Log(" Destroyed");
            Destroy(gameObject);
        }
    }
}
