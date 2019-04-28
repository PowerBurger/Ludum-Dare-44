using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifetime;
    //Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        //var players = GameObject.FindGameObjectsWithTag("characters");

        //foreach (GameObject player in players)
        //{
        //    if (player.GetComponent<Player>().following == null)
        //    {
        //        //target.position = player.transform.position;
        //        target = player.transform;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;

        if(lifetime >= 5)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.up * Time.deltaTime * 20);
    }
}
