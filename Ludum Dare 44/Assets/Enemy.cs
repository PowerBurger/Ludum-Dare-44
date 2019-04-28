using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float health;
    SpriteRenderer[] sp;
    public GameObject bullet;
    public bool canShoot;
    float bulletTimer;
    bool activated;
    void Start()
    {
        player = GameObject.Find("Adam").transform;
        
    }

    public void Activate()
    {
        activated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            try
            {
                var players = GameObject.FindGameObjectsWithTag("characters");

                foreach (GameObject p in players)
                {
                    if (p.GetComponent<Player>().following == null)
                    {
                        player = p.transform;
                        print("found " + p.name);
                    }
                }
            }
            catch
            {
                print("found noone");
            }
        }
        //float distance = Vector3.Distance(transform.position, player.position);
        if (activated)
        {
            float step = 2 * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, player.position, step);

            
        }
        if (canShoot)
        {
            if (activated)
            {

                bulletTimer -= Time.deltaTime;

                if (bulletTimer <= 0)
                {
                    Shoot();
                    bulletTimer = 2;
                }
            }
        }
        if (health <= 0)
        {
            if(gameObject.name == "Boss")
            {
                GameObject.Find("Boulder").GetComponent<Boulder>().enabled = true;
            }
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        GameObject b = Instantiate(bullet, transform.position, new Quaternion());
        Vector3 diff = player.position - b.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Sword")
        {
            health -= 1;
            FindObjectOfType<AudioManager>().Play("enemy");
            StartCoroutine(Flash());
        }

        if (collision.gameObject.name == "hair")
        {
            health -= 1;
            FindObjectOfType<AudioManager>().Play("enemy");
            StartCoroutine(Flash());
        }
        if (collision.gameObject.tag == "magic")
        {
            health -= 0.5f;
            FindObjectOfType<AudioManager>().Play("enemy");
            Destroy(collision.gameObject);
            StartCoroutine(Flash());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "characters")
        {
            if (collision.gameObject.GetComponent<Player>().following == null)
            {
                Health.health -= 1;
                FindObjectOfType<AudioManager>().Play("hurt");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ring")
        {
            health -= Time.deltaTime * 3;
            FindObjectOfType<AudioManager>().Play("enemy");
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        sp = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in sp)
        {
            s.color = Color.red;
            print(s.name);
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer s in sp)
        {
            s.color = Color.white;
        }
    }
}
