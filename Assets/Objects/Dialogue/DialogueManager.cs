using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text DialogueName;
    public Text DialogueText;
    [Range(0, 0.2f)]public float TypeSpeed;
    public Image DialogueImage;
    public Animator Animation;
    public PlayerController Player;

    void Start()
    {
        _conversations = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(!Player.Busy)
        {
            Player.Busy = true;

            Animation.SetBool("IsOpen", true);
            DialogueName.text = dialogue.Name;
            DialogueImage.sprite = dialogue.NPCSprite;

            _conversations.Clear();

            foreach (string sentence in dialogue.Sentences)
                _conversations.Enqueue(sentence);

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence ()
    {
        if(_prevSentence != null && !DialogueText.text.Equals(_prevSentence))
        {
            StopAllCoroutines();
            DialogueText.text = _prevSentence;
            return;
        }

        if(_conversations.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = _conversations.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) 
    {
        DialogueText.text = "";
        _prevSentence = sentence;

        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(TypeSpeed);
        }
    }

    void EndDialogue()
    {
        Animation.SetBool("IsOpen", false);
        Player.Busy = false;
    }

    private Queue<string> _conversations;
    private string _prevSentence;
}
