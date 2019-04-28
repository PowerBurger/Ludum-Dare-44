using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    bool b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (b == true)
            {
                SceneManager.LoadScene("Game");
            }
            if (b == false)
            {
                GameObject.Find("Main").GetComponent<Animator>().Play("main");
                b = true;
            }
            
        }
    }
}
