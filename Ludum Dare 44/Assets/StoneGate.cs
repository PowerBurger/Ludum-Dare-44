using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGate : MonoBehaviour
{
    public GameObject mana;
    public GameObject life;
    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mana.activeSelf == true && life.activeSelf == true && blood.activeSelf == true)
        {
            Destroy(gameObject);
        }
    }

    public void Activate(string gem)
    {
        if(gem == "mana")
        {
            mana.SetActive(true);
        }
        if (gem == "life")
        {
            life.SetActive(true);
        }
        if (gem == "blood")
        {
            blood.SetActive(true);
        }
    }
}
