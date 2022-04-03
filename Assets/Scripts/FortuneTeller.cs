using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneTeller : MonoBehaviour
{
    DialogueCanvasController dCanv;
    public GameObject[] tarotCards;
    public Dialogue[] dialogue;
    GameController cont;

    private void Awake()
    {
        cont = FindObjectOfType<GameController>();
        dCanv = FindObjectOfType<DialogueCanvasController>();
        FTDialogue();
    }

    public void ShowCard()
    {
        tarotCards[cont.deaths].SetActive(true);
    }

    public void FTDialogue()
    {
        dCanv.EndDialogue();
        dCanv.ShowCard();
        dCanv.StartDialogue(dialogue[cont.deaths]);
    }
}
