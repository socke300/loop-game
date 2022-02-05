using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StroyGenerator : MonoBehaviour
{
    public string summary;
    public string finalName;
    public List<string> achievements;
    public List<string> possibleNames;
    PlayerStats stats;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    public void add(string achievement){
        achievements.Add(stats.age.ToString() + ";" + achievement);
    }

    public string nameGenerator(){

        if(stats.age > 100) possibleNames.Add("der Alte");
        if(stats.age > 120) possibleNames.Add("der Unsterbliche");
        if(stats.ehre >= 3) possibleNames.Add("der Edle");
        if(stats.ehre < -1) possibleNames.Add("der Ehrenlose");
        if(stats.ängstlich >= 3) possibleNames.Add("der Feige");
        if(stats.mutig >= 3) possibleNames.Add("der Mutige");
        if(stats.riskolustig >= 3) possibleNames.Add("der Spieler");
        if(stats.vernünftig >= 3) possibleNames.Add("der Weise");
        if(stats.gold > 80) possibleNames.Add("der Reiche");
        if(stats.intelligence >= 5) possibleNames.Add("der Clevere");
        if(stats.strength >= 5) possibleNames.Add("der Bulle");
        if(stats.dexterity >= 5) possibleNames.Add("die Katze");
        if(stats.charisma >= 5) possibleNames.Add("der Charmeur");
        if(stats.charisma < 2) possibleNames.Add("der Unbeliebte");
        
    

        stats.score += possibleNames.Count*5;
        if(possibleNames.Count == 0) possibleNames.Add("der Nichtsnutz");
        int r = Random.Range(0, possibleNames.Count-1);
        return stats.playername + " " + possibleNames[r];
    }

    public string writeStory(){
        foreach (var achievement in achievements)
        {
            int r = Random.Range(0, 5);
            string[] tokens = achievement.Split(';');
            if(r == 0) summary += " Im Alter von " + tokens[0] + " Jahren " + tokens[1]; 
            else if(r == 1) summary += " Mit " + tokens[0] + " Jahren " + tokens[1];
            else if(r == 2) summary += " Als er " + tokens[0] + " war " + tokens[1];
            else if(r == 3) summary += " In seinem " + tokens[0] + ". Lebensjahr " + tokens[1];
            else if(r == 4) summary += " Kurz nach seinem " + tokens[0] + ". Geburtstag " + tokens[1];        
        }
        return summary;
    }

}
