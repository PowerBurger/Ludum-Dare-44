using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NPC : MonoBehaviour {

    GlobalDialogueManager gdm;
    GameObject tp;
    private bool promptActive;
    public bool isTalking;

    [HideInInspector]
    //public bool insideTalkZone;
    GameObject playerTalking;
    //[HideInInspector]
    public float moveToX;

    [Header("NPC Info")]
    public string npcName;
    public Color nameColor;
    public int voiceCount;
    public float bubbleHigher;
    public string dialogue;

    //[Header("Dialogues")]
    //public LinesArea[] linesArea;

    [HideInInspector]
    public bool speakerFacingMe;

    //[Header("Voice")]
    //public AudioClip sound;
    //public float pitchRange;

    // Use this for initialization
    void Start ()
    {
        //gdm = GameObject.Find("GlobalDialogueManager").GetComponent<GlobalDialogueManager>();

        //if (gameObject.name == "Speaker")
        //{
        //    isTalking = true;
        //    SentConv s = Lang.GetConv("SpeakerIntro");
        //    FindObjectOfType<GlobalDialogueManager>().StartDialogue(s, gameObject);
        //}
    }

	void Update ()
    {

    }

    //public void TalkPrompt()
    //{
    //    if (!promptActive && !isTalking)
    //    {
    //        Vector3 thepos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z);
    //        tp = Instantiate(gdm.TalkPromptPrefab, thepos, gameObject.transform.rotation);
    //        promptActive = true;
    //    }
    //}

    //public void TalkPromptDeactivate()
    //{
    //    if (promptActive)
    //    {
    //        tp.GetComponentInChildren<Animator>().Play("disappear");
    //        Destroy(tp, 1);
    //        promptActive = false;
    //    }
    //}

    //public void Talk(GameObject pt)
    //{
    //    playerTalking = pt;

    //    try
    //    {
    //        TalkPromptDeactivate();
    //    }
    //    catch { }
    //    if (isTalking == false)
    //    {
    //        isTalking = true;





    //        //GetComponent<DialogueTrigger>().TriggerDialogue(gameObject);
    //        SentConv s = Lang.GetConv(dialogue);
    //        FindObjectOfType<GlobalDialogueManager>().StartDialogue(s, gameObject);
    //    }
    //}

    public void EndOfTalk()
    {
        isTalking = false;
        //if (playerTalking != null)
        //{
        //    playerTalking.GetComponent<PlayerMovement>().isTalking = false;
        //}
        //TalkPrompt();
    }
}
