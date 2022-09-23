using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for the objects that are thrown
public class Throwable : MonoBehaviour
{
    ThrowingAndStates throwingAndStates;

    float highestPointGotten;

    void Start()
    {
        throwingAndStates = FindObjectOfType<ThrowingAndStates>();
    }

    void Update()
    {
        // Gives the highest point this object has traveled to
        if(transform.position.y > highestPointGotten)
        {
            highestPointGotten = transform.position.y;
        }
    }

    // Check if the player threw the object inside the lines
    void CheckForPoints()
    {
        if(highestPointGotten > throwingAndStates.limitLine.transform.position.y)
        {
            throwingAndStates.gameOver = true;
        }
        else if (highestPointGotten > throwingAndStates.goalLine.transform.position.y && highestPointGotten < throwingAndStates.limitLine.transform.position.y)
        {
            throwingAndStates.score += 1;
            throwingAndStates.PlayCheerSound();
        }
        else if (highestPointGotten < throwingAndStates.goalLine.transform.position.y)
        {
            throwingAndStates.PlayTooLowSound();
        }
    }
    // When this object hits the floor it calls a couple of methods and goes to sleep
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            CheckForPoints();
            gameObject.GetComponent<Rigidbody2D>().Sleep();
            throwingAndStates.ThrowableHitFloor();
        }
    }
}
