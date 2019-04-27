using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDialogueManager : MonoBehaviour {

    public GameObject DialogueCanvasPrefab;
    public GameObject TalkPromptPrefab;
    public GameObject dcp;
    public GameObject prevSpeaker;
    public bool isTalking;


    //public void CreateDialogueBox()
    //{
    //    dcp = Instantiate(DialogueCanvasPrefab);
    //}

    private void Update()
    {
        if (dcp != null && prevSpeaker != null)
        {
            if(prevSpeaker.GetComponent<NPC>() != null)
            dcp.transform.position = new Vector3(prevSpeaker.transform.position.x, prevSpeaker.transform.position.y + prevSpeaker.GetComponent<NPC>().bubbleHigher, prevSpeaker.transform.position.z);
        }
    }

    public void StartDialogue(SentConv Sentences, GameObject speaker)
    {
        isTalking = true;
        var diaboxes = GameObject.FindGameObjectsWithTag("DialogueCanvas");
        if (diaboxes.Length == 0)
        {
            float higher = speaker.GetComponent<NPC>().bubbleHigher;
            Vector3 bubblePos = new Vector3(speaker.transform.position.x, speaker.transform.position.y + higher, speaker.transform.position.z);
            dcp = Instantiate(DialogueCanvasPrefab, bubblePos, speaker.transform.rotation);
            dcp.GetComponent<DialogueManager>().StartDialogue(Sentences, speaker);
        }
    }

    public void ChangeCharacter(GameObject speaker)
    {
        prevSpeaker = speaker;
        float higher = speaker.GetComponent<NPC>().bubbleHigher;
        Vector3 bubblePos = new Vector3(speaker.transform.position.x, speaker.transform.position.y + higher, speaker.transform.position.z);
        //dcp = Instantiate(DialogueCanvasPrefab, bubblePos, speaker.transform.rotation);
        dcp.transform.position = bubblePos;
        //dcp.GetComponent<DialogueManager>().StartDialogue(Sentences, speaker);
    }
}
