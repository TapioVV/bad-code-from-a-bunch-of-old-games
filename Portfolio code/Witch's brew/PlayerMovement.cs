using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    float xInput;
    float yInput;
    public float movePower;

    Animator anim;

    bool moving;
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        GetInputs();
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void GetInputs()

    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", xInput);
        anim.SetFloat("Vertical", yInput);
    }
    void Movement()
    {
        Vector2 MovementVectorX = new Vector2(xInput * movePower, 0);
        rb.velocity = new Vector2(xInput * movePower, rb.velocity.y);

        Vector2 MovementVectorY = new Vector2(0, yInput * movePower);
        rb.velocity = new Vector2(rb.velocity.x, yInput * movePower);
    }
}