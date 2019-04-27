using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Order[] renderers = FindObjectsOfType<Order>();

        foreach(Order renderer in renderers)
        {
            var sprs = renderer.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer s in sprs)
            {
                s.sortingOrder = (int)(renderer.transform.position.y * -100);
            }
        }
    }
}
