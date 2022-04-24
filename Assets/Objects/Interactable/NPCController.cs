using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Interactable
{
    public Dialogue dialogue;

    public override void Interact()
    {
        if(dialogue.Sentences.Length > 0)
            TriggerDialogue();
        else
            Debug.Log("No dialogue found");
            // Other interaction
    }
    

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
