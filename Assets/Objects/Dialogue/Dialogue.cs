using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string Name;
    public Sprite NPCSprite;

    [TextArea(3,10)]
    public string[] Sentences;
}
