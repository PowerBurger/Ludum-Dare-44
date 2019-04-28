using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    //public float health;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        float step = 3 * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }
}
