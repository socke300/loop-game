using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Quest{
    
    [Header("Text")]
    public string title;
    public String description;

    
    public int difficulty;

    
    public int rewardScore;
    public int rewardGold;

    public TriggeredEvent ev;

}
