using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject pre;
    float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "characters")
        {
            spawnTimer += Time.deltaTime;

            if(spawnTimer >= 10)
            {
                Vector2 h = new Vector2(collision.gameObject.transform.position.x + Random.Range(-15, 15), collision.gameObject.transform.position.y + Random.Range(-15, 15));
                Instantiate(pre, h, new Quaternion());
                spawnTimer = 0;
            }
        }
    }
}
