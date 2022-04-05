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
    public string npcName;
    public Dialogue[] dialogue;
    public Queue<string> sentences;
    DialogueCanvasController dCanv;
    public float timeBetweenTalks = 0.1f;
    float talkCools;
    public float timeBetweenChars = 0.01f;
    public bool mainDialogue = false;
    public GameObject speechBubbleParent;
    public Text speechBubbleText;
    public float dialogueTime = 7.5f;
    public float idleRange = 10f;
    public bool canFollow = true;

    GameObject uiParent;
    public GameObject namePrefab;
    public GameObject hpPrefab;
    Text nameText;
    Image hpBar;
    GameObject n;
    GameObject h;

    GameController cont;

    //Animations
    Animator anim;

    public override void Awake()
    {
        anim = GetComponent<Animator>();
        cont = FindObjectOfType<GameController>();

        uiParent = GameObject.FindGameObjectWithTag("uiParent");
        h = Instantiate(hpPrefab);
        n = Instantiate(namePrefab);
        h.transform.SetParent(uiParent.transform);
        n.transform.SetParent(uiParent.transform);
        nameText = n.GetComponent<Text>();
        nameText.text = npcName;
        hpBar = h.transform.GetChild(1).GetComponent<Image>();
        n.SetActive(false);
        h.SetActive(false);

        base.Awake();
        dCanv = FindObjectOfType<DialogueCanvasController>();
        sentences = new Queue<string>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bod = GetComponent<Rigidbody2D>();
    }

    public override void OnEnable()
    {
        maxFollowDistance = Random.Range(maxDisLow, maxDisHigh);
        hpBar.fillAmount = 1;
        base.OnEnable();
    }

    public void ChangeState(npcstates newState)
    {
        curState = newState;
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
        hpBar.fillAmount = (hp / maxHp);
    }

    public override void Die()
    {
        n.SetActive(false);
        h.SetActive(false);
        cont.VillagerDeath();
        ChangeState(npcstates.idle);
        base.Die();
    }

    void Idle()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inRange)
        {
            n.SetActive(true);
            h.SetActive(true);
            ChangeState(npcstates.follow);
        }
    }

    void Follow()
    {
        dis = Vector2.Distance(transform.position, target.position);

        if (dis >= idleRange) ChangeState(npcstates.idle);

        if (target != null && dis > maxFollowDistance)
        {
            Vector2 dir = target.position - transform.position;
            bod.AddForce(dir * spd * Time.deltaTime);
        }

        if (target != null)
        {
            anim.SetFloat("moveX", bod.velocity.x);
            anim.SetFloat("moveY", bod.velocity.y);
            //if (target.position.y > transform.position.y) rend.sprite = sprites[0];
            //if (target.position.x < transform.position.x) rend.sprite = sprites[3];
            //if (target.position.x > transform.position.x) rend.sprite = sprites[2];
            //if (target.position.y < transform.position.y) rend.sprite = sprites[1];

            //rend.flipX = (target.position.x > transform.position.x);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            n.SetActive(false);
            h.SetActive(false);
            ChangeState(npcstates.idle);
        }
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
                    dCanv.StartDialogue(dialogue[0], npcName);
                }
                else
                {
                    if (!speechBubbleParent.activeInHierarchy)
                    {
                        EndDialogue();
                        StartDialogue(dialogue[0]);
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
