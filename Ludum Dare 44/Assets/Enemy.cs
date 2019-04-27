using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    public int health;
    SpriteRenderer sp;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = 2 * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        if(health <= 0)
        {
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
