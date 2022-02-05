using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TriggeredEvent
{
    public int id;
    [Header("Text")]
    public String title;
    [TextArea(3,10)]
    public String description;

    
    [Header("Icon")]
    public Sprite icon;

    [Header("Buttons")]
    public String[] options;

    [HideInInspector]
    public GameObject location; 
    [HideInInspector]
    public bool finished;
    

}
