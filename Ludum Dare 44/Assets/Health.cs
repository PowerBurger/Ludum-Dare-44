using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static float health = 2;
    public static Transform lastCheckpoint;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0)
        {
            var players = GameObject.FindGameObjectsWithTag("characters");

            foreach (GameObject p in players)
            {
                p.transform.position = lastCheckpoint.position;
                health = 2;
            }
        }
        if (health < 2)
        {
            health += Time.deltaTime * 0.05f;
        }
        if (health > 2)
        {
            health = 2;
        }
    }
}
