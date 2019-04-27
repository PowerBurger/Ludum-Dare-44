using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public int health = 5;
    public GameObject following;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (following == null)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f)
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

            if (Input.GetButtonDown("Attack"))
            {
                animator.Play("hit");
            }
        }

        if(following != null)
        {
            float distance = Vector3.Distance(transform.position, following.transform.position);
            if (distance > 1.5f)
            {
                float step = speed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, following.transform.position, step);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DialogueTrigger")
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

        if (collision.gameObject.name == "SoulCollector")
        {
            collision.GetComponent<SoulCollector>().OpenBars();
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Stone Of Mana")
        {
            PlayerPrefs.SetInt("Stone Of Mana", 1);
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Medal Of Life")
        {
            PlayerPrefs.SetInt("Medal Of Life", 1);
            Destroy(gameObject);
        }
    }
}
