using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Color selected;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deselect()
    {
        GetComponent<Image>().color = Color.white;
    }
    public void Select()
    {
        GetComponent<Image>().color = selected;

        var characters = GameObject.FindGameObjectsWithTag("characters");
        Camera.main.GetComponent<CameraFollow>().target = character.transform;

        foreach(GameObject c in characters)
        {
            if(c != character)
            {
                c.GetComponent<Player>().following = character;
            }
            else
            {
                c.GetComponent<Player>().following = null;
            }
        }

        var slots = GameObject.FindGameObjectsWithTag("slot");

        foreach (GameObject s in slots)
        {
            if (s != gameObject)
            {
                s.GetComponent<Slot>().Deselect();
            }
        }
    }
}
