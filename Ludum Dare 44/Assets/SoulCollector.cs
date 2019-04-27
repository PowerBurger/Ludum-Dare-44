using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollector : MonoBehaviour
{
    public GameObject bars;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBars()
    {
        bars.GetComponent<Animator>().Play("slide");
        Destroy(gameObject);
    }
}
