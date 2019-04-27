using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float health;
    SpriteRenderer sp;
    public GameObject bullet;
    public bool canShoot;
    float bulletTimer;
    void Start()
    {
        player = GameObject.Find("Adam").transform;
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            var players = GameObject.FindGameObjectsWithTag("characters");

            foreach (GameObject p in players)
            {
                if (p.GetComponent<Player>().following == null)
                {
                    player = p.transform;
                }
            }
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 15)
        {
            float step = 2 * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, player.position, step);

            
        }
        if (canShoot)
        {
            bulletTimer -= Time.deltaTime;

            if (bulletTimer <= 0)
            {
                Shoot();
                bulletTimer = 2;
            }
        }
        if (health <= 0)
        {
            print("die!");
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
            StartCoroutine(Flash());
        }

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().health -= 1;
        }

        if (collision.gameObject.tag == "magic")
        {
            health -= 1;
            Destroy(collision.gameObject);
            StartCoroutine(Flash());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ring")
        {
            health -= Time.deltaTime * 3;
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
    }
}
