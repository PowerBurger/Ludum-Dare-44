using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public string dialogue;

	public void TriggerDialogue ()
	{
        SentConv s = Lang.GetConv(dialogue);
        //if(s.canMove == false)
        //{
        //    var chars = GameObject.FindGameObjectsWithTag("character");

        //    foreach(GameObject c in chars)
        //    {
        //        c.GetComponent<Animator>().SetFloat("speed", 0);
        //        c.GetComponent<Animator>().SetBool("isJumping", false);
        //    }
        //}


        FindObjectOfType<GlobalDialogueManager>().StartDialogue(s, GameObject.Find("Adam"));

        Destroy(gameObject);
    }

}
