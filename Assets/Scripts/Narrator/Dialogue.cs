﻿using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string DialogueTitle = string.Empty;
    [TextArea(0, 25)]
    public string Sentence = string.Empty;
    [Header("Time for message to stay.")]
    [Space(10)]
    public float Delay = 0;
    public bool ClearScreen = false;
    [Header("Leave empty if you wont use it.")]
    public string SceneToLoad = string.Empty;
}
