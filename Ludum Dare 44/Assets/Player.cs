using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public int health = 5;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if(Input.GetButtonDown("Attack"))
        {
            animator.Play("hit");
        }
    }
}
