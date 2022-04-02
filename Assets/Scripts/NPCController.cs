using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : LivingThing
{
    public enum npcstates { idle, follow, panic }
    [Header("Basics")]
    public npcstates curState = npcstates.idle;
    public bool inRange = false;
    Rigidbody2D bod;
    //PlayerController player;
    Transform target;
    public float maxDisLow = 1.25f;
    public float maxDisHigh = 3f;
    public float maxFollowDistance;
    float dis;

    [Space]
    [Header("Dialogue")]
    public Dialogue dialogue;
    public Queue<string> sentences;
    DialogueCanvasController dCanv;
    public float timeBetweenTalks = 0.1f;
    float talkCools;
    public float timeBetweenChars = 0.01f;
    public bool mainDialogue = false;
    public GameObject speechBubbleParent;
    public Text speechBubbleText;
    public float dialogueTime = 7.5f;

    private void Awake()
    {
        dCanv = FindObjectOfType<DialogueCanvasController>();
        sentences = new Queue<string>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bod = GetComponent<Rigidbody2D>();
    }

    public override void OnEnable()
    {
        maxFollowDistance = Random.Range(maxDisLow, maxDisHigh);
        base.OnEnable();
    }

    public void ChangeState(npcstates newState)
    {
        curState = newState;
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
    }

    public override void Die()
    {
        base.Die();
    }

    void Idle()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inRange)
        {
            ChangeState(npcstates.follow);
        }
    }

    void Follow()
    {
        dis = Vector2.Distance(transform.position, target.position);

        if (target != null && dis > maxFollowDistance)
        {
            Vector2 dir = target.position - transform.position;
            bod.AddForce(dir * spd * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.X) && inRange)
        {
            ChangeState(npcstates.idle);
        }

        if (!inRange) ChangeState(npcstates.idle);
    }

    void Panic()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inRange)
        {
            ChangeState(npcstates.follow);
        }
    }

    private void Update()
    {
        switch(curState)
        {
            case (npcstates.idle):
                Idle();
                break;
            case (npcstates.follow):
                Follow();
                break;
            case (npcstates.panic):
                Panic();
                break;
        }

        if (inRange && talkCools <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //if (Input.GetKeyDown(KeyCode.E) && !dCanv.isTalkingAlready)
                //Do Dialogue
                if (mainDialogue)
                {
                    dCanv.EndDialogue();
                    dCanv.StartDialogue(dialogue);
                }
                else
                {
                    if (!speechBubbleParent.activeInHierarchy)
                    {
                        EndDialogue();
                        StartDialogue(dialogue);
                    }
                }
            }
        }

        if (talkCools > 0) talkCools -= Time.deltaTime;
    }

    public virtual void StartDialogue(Dialogue d)
    {
        sentences.Clear();

        foreach (string s in d.sentences)
        {
            sentences.Enqueue(s);
        }

        //Add continue button function to UI button
        //continueButton => DisplayNextSentence();
        speechBubbleParent.SetActive(true);
        Invoke("DisplayNextSentence", 0f);
    }

    public virtual void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            Invoke("EndDialogue", dialogueTime);
            return;
        }

        string sen = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sen));
        Invoke("DisplayNextSentence", dialogueTime);
        //dCanv.dialogueText.text = sen;
    }

    IEnumerator TypeSentence(string sentence)
    {
        speechBubbleText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            speechBubbleText.text += letter;
            yield return timeBetweenChars;
        }
    }

    public virtual void EndDialogue()
    {
        speechBubbleText.text = "";
        speechBubbleParent.SetActive(false);
    }
}
