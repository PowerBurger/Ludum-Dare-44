using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI lengthChecker;
    public GameObject upperBox;
    public Animator animator;
    private float clickTimer;
    private string oldsays;
    GameObject previousSpeaker;
    AudioManager audioManager;
    public bool command;
    public string commandtext = "";
    public bool tag;
    public string tagtext = "";
    //NPC npc;

    //private Queue<string> sentences;
    private Queue<sent> sentences;

    // Use this for initialization
    void Awake () {
        //sentences = new Queue<string>();
        sentences = new Queue<sent>();

        audioManager = FindObjectOfType<AudioManager>();
	}

    private void Update()
    {
        if(clickTimer > 0)
        {
            clickTimer -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Talk") && clickTimer <= 0)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (SentConv SentList, GameObject speaker)
	{

        previousSpeaker = speaker;
        //npc = previousSpeaker.GetComponent<NPC>();
		animator.SetBool("IsOpen", true);

		nameText.text = speaker.GetComponent<NPC>().npcName;
        nameText.color = speaker.GetComponent<NPC>().nameColor;

		sentences.Clear();

		foreach (sent s in SentList)
		{
			//sentences.Enqueue(s.whatsays);
            sentences.Enqueue(s);
        }

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
        previousSpeaker.GetComponent<Animator>().SetBool("isTalking", false);
        clickTimer = 0.3f;
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		//string sentence = sentences.Dequeue();
        sent sentence = sentences.Dequeue();

        if(sentence.whosays != oldsays)
        {
            GameObject whospeaks = GameObject.Find(sentence.whosays);
            nameText.color = whospeaks.GetComponent<NPC>().nameColor;
            previousSpeaker = whospeaks;
            //previousSpeaker.GetComponent<Animator>().SetBool("isTalking", true);

            FindObjectOfType<GlobalDialogueManager>().ChangeCharacter(whospeaks);
        }

        


        //Vector3 bubblePos = new Vector3(speaker.transform.position.x, speaker.transform.position.y + 6.5f, speaker.transform.position.z);
        //GameObject dcp = Instantiate(DialogueCanvasPrefab, bubblePos, speaker.transform.rotation);
        //dcp.GetComponent<DialogueManager>().StartDialogue(Sentences, speaker);

        oldsays = sentence.whosays;
        StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentence));
        StartCoroutine(TypeSentence(sentence));
	}

    //IEnumerator TypeSentence (string sentence)
    IEnumerator TypeSentence (sent sentence)
	{
        //previousSpeaker.GetComponent<Animator>().SetBool("isTalking", true);
        nameText.text = sentence.whosays;
        dialogueText.text = "";
        int count = previousSpeaker.GetComponent<NPC>().voiceCount - 1;
        //foreach (char letter in sentence.ToCharArray())

        bool precommand = false;
        bool startc = false;
        foreach (char letter in sentence.whatsays)
        {
            startc = false;
            if(letter == '{')
            {
                precommand = true;
            }
            if (letter == '@')
            {
                if (precommand == false)
                {
                    precommand = true;
                    startc = true;
                }
            }
            if (!precommand)
            {
                dialogueText.text += letter;
            }
            if (letter == '@')
            {
                if (precommand == true && !startc)
                {
                    precommand = false;
                }
            }
            if (letter == '}')
            {
                precommand = false;
            }
        }
        int totalVisibleCharacters = dialogueText.textInfo.characterCount;
        int visibleCount = 0;
        dialogueText.maxVisibleCharacters = 0;
        char[] letterArr = dialogueText.text.ToCharArray();

        //Checking how many lines
        dialogueText.ForceMeshUpdate();
        float lineCount = dialogueText.textInfo.lineCount - 3;
        if(lineCount < 0)
        {
            lineCount = 0;
        }

        //Resetting
        dialogueText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(dialogueText.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 5.699997f);
        upperBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(upperBox.GetComponent<RectTransform>().anchoredPosition.x, 39.1f);
        nameText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(nameText.gameObject.GetComponent<RectTransform>().anchoredPosition.x, 110.2f);

        //Resizing
        dialogueText.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 57.3f * lineCount);
        upperBox.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 57.3f * lineCount);
        nameText.gameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 57.3f * lineCount);

        foreach (char letter in sentence.whatsays) 
		{

            if (letter == '+')
            {
                previousSpeaker.GetComponent<Animator>().SetBool("isTalking", false);
                DisplayNextSentence();
                break;
            }

            if (letter == '*')
            {
                command = !command;
                if(command == false)
                {
                    previousSpeaker.GetComponent<Animator>().Play(commandtext);
                    commandtext = "";
                }
            }

            if (letter == '{')
            {
                command = true;
            }

            if (letter == '}')
            {
                command = false;
                if (commandtext == "shake")
                {
                    //FindObjectOfType<CameraShake>().ShakeDefault();
                }
                if (commandtext.Length > 3)
                { 
                    if (commandtext.Substring(0, 2) == "tp")
                    {
                        string coords = commandtext.Substring(3);
                        print(coords);
                        string x = "";
                        string y = "";
                        bool yb = false;
                        foreach (char c in coords)
                        {
                            if (c == ',')
                            {
                                yb = true;
                            }
                            else
                            {
                                if (!yb)
                                {
                                    x += c;
                                }
                                if (yb)
                                {
                                    y += c;
                                }
                            }
                        }
                        previousSpeaker.transform.position = new Vector2(float.Parse(x), float.Parse(y));
                    }
                }

                if (commandtext.Length > 6)
                {
                    if (commandtext.Substring(0, 4) == "anim")
                    {
                        previousSpeaker.GetComponent<Animator>().Play(commandtext.Substring(5));
                        
                    }
                }

                commandtext = "";
            }

            if (letter == '<')
            {
                tag = true;
            }

            if (letter == '@')
            {
                command = !command;
                if (command == false)
                {
                    yield return new WaitForSeconds(float.Parse(commandtext));
                    commandtext = "";
                }
            }

            if (letter == '#')
            {
                command = !command;
                if (command == false)
                {
                    previousSpeaker.GetComponent<NPC>().moveToX = float.Parse(commandtext);
                    previousSpeaker.GetComponent<Animator>().SetFloat("speed", 1);
                    commandtext = "";
                }
            }

            if (command == false)
            {
                if (letter != '*' && letter != '#' && letter != '@' && letter != '[' && letter != ']' && letter != '{' && letter != '}')
                {
                    if (!tag)
                    {
                        //if (tagtext != "")
                        //{
                        //    //dialogueText.text += tagtext;
                        //    tagtext = "";
                        //}

                        //New system thingy
                        visibleCount += 1;

                        dialogueText.maxVisibleCharacters = visibleCount;

                        count += 1;


                        //dialogueText.text += letter;
                        previousSpeaker.GetComponent<Animator>().SetBool("isTalking", true);
                        FindObjectOfType<GlobalDialogueManager>().dcp.GetComponentInChildren<Animator>().Play("DialogueBox_Open");
                    }
                }
            }
            if(command == true)
            {
                if (letter != '*' && letter != '#' && letter != '@' && letter != '{' && letter != '}')
                {
                    commandtext += letter;
                }
            }
            if(tag == true)
            {
                //tagtext += letter;
            }

            
            if (count == previousSpeaker.GetComponent<NPC>().voiceCount)
            {
                if (!command)
                {
                    try
                    {
                        audioManager.Play(previousSpeaker.name);
                    }
                    catch { }
                }
                count = 0;
            }

            if (!command && !tag)
            {
                yield return new WaitForSeconds(0.03f);
                //wait more if punctuation
                if(letter == '.' || letter == '!')
                {
                    yield return new WaitForSeconds(0.4f);
                }
            }
            if (letter == '>')
            {
                tag = false;
            }
        }
        previousSpeaker.GetComponent<Animator>().SetBool("isTalking", false);

        
    }

	void EndDialogue()
	{
        StartCoroutine(EndDialogueCo());
		
    }

    IEnumerator EndDialogueCo()
    {
        FindObjectOfType<GlobalDialogueManager>().isTalking = false;
        previousSpeaker.GetComponent<Animator>().SetBool("isTalking", false);
        animator.SetBool("IsOpen", false);
        previousSpeaker.GetComponent<NPC>().EndOfTalk();
        Destroy(gameObject, 1);
        Destroy(GetComponent<DialogueManager>());
        yield return new WaitForSeconds(0.03f);
        //previousDt.GetComponent<DialogueTrigger>().TriggerNext();
    }
}
