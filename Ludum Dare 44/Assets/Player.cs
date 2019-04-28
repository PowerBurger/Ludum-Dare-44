using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public int health = 5;
    public GameObject following;
    public GameObject magic;
    public Transform magicpos;
    public GameObject ring;
    public GameObject hair;

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
                animator.SetBool("moving", true);
            }
            if (Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
                animator.SetBool("moving", true);
            }
            if (Input.GetAxisRaw("Vertical") > 0.5f)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
                animator.SetBool("moving", true);
            }
            if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
                animator.SetBool("moving", true);
            }
            if (Input.GetAxisRaw("Vertical") > -0.5f && Input.GetAxisRaw("Vertical") < 0.5f && 
                Input.GetAxisRaw("Horizontal") > -0.5f && Input.GetAxisRaw("Horizontal") < 0.5f)
            {
                animator.SetBool("moving", false);
            }

            if (FindObjectOfType<GlobalDialogueManager>().isTalking == false)
            {
                if (Input.GetButtonDown("Attack") && gameObject.name == "Adam")
                {
                    animator.Play("hit");
                    FindObjectOfType<AudioManager>().Play("swoosh");
                }

                if (Input.GetButtonDown("Attack") && gameObject.name == "Benny")
                {
                    Magic();
                    FindObjectOfType<AudioManager>().Play("magic");
                }

                if (Input.GetButtonDown("Attack") && gameObject.name == "Mel")
                {
                    ring.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("ringup");
                }
                if (Input.GetButtonUp("Attack") && gameObject.name == "Mel")
                {
                    FindObjectOfType<AudioManager>().Play("ringdown");
                }
            }
        }
        if (Input.GetButtonUp("Attack") && gameObject.name == "Mel")
        {
            ring.SetActive(false);
        }

        if (following != null)
        {
            float distance = Vector3.Distance(transform.position, following.transform.position);
            if (distance > 1.5f)
            {
                animator.SetBool("moving", true);
                float step = speed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, following.transform.position, step);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }
    }
    void Walk1()
    {
        FindObjectOfType<AudioManager>().Play("walk1");
    }

    void Walk2()
    {
        FindObjectOfType<AudioManager>().Play("walk2");
    }

    void Magic()
    {
        GameObject b = Instantiate(magic, magicpos.position, new Quaternion());
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - b.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Title Screen");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "dungeon")
        {
            if (GameObject.Find("OverworldSound").GetComponent<AudioSource>().isPlaying == false)
            {
                GameObject.Find("OverworldSound").GetComponent<AudioSource>().Play();
            }
            GameObject.Find("DungeonSound").GetComponent<AudioSource>().Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "dungeon" && collision.gameObject.name != "startar")
        {
            if (GameObject.Find("DungeonSound").GetComponent<AudioSource>().isPlaying == false)
            {
                GameObject.Find("DungeonSound").GetComponent<AudioSource>().Play();
            }
            GameObject.Find("OverworldSound").GetComponent<AudioSource>().Stop();
        }

        if (collision.gameObject.tag == "DialogueTrigger")
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
            //PlayerPrefs.SetInt("Stone Of Mana", 1);
            Destroy(collision.gameObject);
            GameObject.Find("Stone Gate").GetComponent<StoneGate>().Activate("mana");
            FindObjectOfType<AudioManager>().Play("gem");
        }

        if (collision.gameObject.name == "Medal Of Life")
        {
            //PlayerPrefs.SetInt("Medal Of Life", 1);
            Destroy(collision.gameObject);
            GameObject.Find("Stone Gate").GetComponent<StoneGate>().Activate("life");
            FindObjectOfType<AudioManager>().Play("gem");
        }

        if (collision.gameObject.name == "Crystal Of Blood")
        {
            //PlayerPrefs.SetInt("Crystal Of Blood", 1);
            Destroy(collision.gameObject);
            GameObject.Find("Stone Gate").GetComponent<StoneGate>().Activate("blood");
            FindObjectOfType<AudioManager>().Play("gem");
        }

        if (collision.gameObject.name == "OpenBarsTrigger")
        {
            GameObject.Find("BarsTriggered").GetComponent<Animator>().Play("slide");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "range")
        {
            collision.gameObject.GetComponentInParent<Enemy>().Activate();
        }

        if (collision.gameObject.name == "Boulder")
        {
            StartCoroutine(Ending());
        }

        if (collision.gameObject.tag == "checkpoint")
        {
            Health.lastCheckpoint = collision.gameObject.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            if (GetComponent<Player>().following == null)
            {
                Health.health -= 1;
                Destroy(collision.gameObject);
                FindObjectOfType<AudioManager>().Play("hurt");
            }
        }
    }
}
