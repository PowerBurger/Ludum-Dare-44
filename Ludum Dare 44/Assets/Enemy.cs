using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public int health;
    SpriteRenderer sp;
    void Start()
    {
        player = GameObject.Find("Player1").transform;
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 15)
        {
            float step = 2 * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
        }
        if(health <= 0)
        {
            print("die!");
            Destroy(gameObject);
        }
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
    }

    IEnumerator Flash()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
    }
}
