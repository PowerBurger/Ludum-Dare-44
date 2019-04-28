using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        if (player == null /*|| player != null && player.gameObject.GetComponent<Player>().following != null*/)
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
    }
    void Update()
    {
        float step = 40 * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }
}
