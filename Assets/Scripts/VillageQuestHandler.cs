using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;
using TMPro;





public class VillageQuestHandler: MonoBehaviour{
    

    public Quest[] quests;
    public TriggeredEvent[] tevents;
    

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI opt1Text;
    public TextMeshProUGUI opt2Text;
    public TextMeshProUGUI opt3Text;
    public GameObject questholder;

    [HideInInspector]
    Quest quest1;
    [HideInInspector]
    Quest quest2;
    [HideInInspector]
    Quest quest3;
    [HideInInspector]
    Quest activeQuest;
    [HideInInspector]
    public bool playerReady = true;
    Manager manager;
    SpriteSpawner spriteSpawner;
    EventManager evmanager;
    bool choosingQuest;

    GameObject player;
    public GameObject start;

    
    PlayerStats stats;
    string name;

    

    void Start()
    {   
        manager = GetComponent<Manager>();
        evmanager = GetComponent<EventManager>();
        spriteSpawner = GetComponent<SpriteSpawner>();
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<PlayerStats>();
        name = stats.playername;
        foreach (var ev in tevents)
        {
            ev.description = ev.description.Replace("{name}", stats.playername); 
        }
        foreach (var quest in quests)
        {
            quest.ev.description = quest.ev.description.Replace("{name}", stats.playername); 
        }


    }

    void Update(){

        
        if(playerReady && Vector2.Distance(start.transform.position, player.transform.position) < 0.05){
            //Stop time
            stats.hp += 10;
            playerReady = false;
            manager.running = false;
            choosingQuest = true;
            spawnQuests();
            spawnEvents();
            //give random attributes
            int r = Random.Range(0,4);
            if(r == 0) stats.strength++;
            else if(r == 1) stats.intelligence++;
            else if(r == 2) stats.dexterity++;
            else if(r ==3) stats.charisma++;
        }
    }

    void spawnQuests(){
        
        foreach (var i in quests)
        {
            i.ev.finished = false;
        }
        

        quest1 = quests[0];
        quest2 = quests[1];
        quest3 = quests[2];

        questholder.SetActive(true);
        titleText.text = "Neue Heldentaten";
        int r = Random.Range(0,4);
        if(r == 0) descriptionText.text = "Nach seiner Rueckkehr ins Dorf brach " + name + " schon bald wieder auf um...";
        if(r == 1) descriptionText.text = "Nach einer kurzen Pause brach machte" + name + " sich bereit um ...";
        if(r == 2) descriptionText.text = "Kaum im Dorf angekommen brach " + name + " wieder auf um ...";
        if(r == 3) descriptionText.text = "Das Dorfleben war nichts für " + name + ". Schon bald brach er auf um..";
        opt1Text.text = "" + quest1.description;
        opt2Text.text = "" + quest2.description;
        opt3Text.text = "" + quest3.description;

    }

  

    void spawnEvents(){
        int index1 = Random.Range(0, tevents.Length);
        int index2 = -1;
        int index3 = -1;

        while (index2 == -1)
        {
            int temp = Random.Range(0, tevents.Length);
            if (temp != index1) index2 = temp;
        }  

        while (index3 == -1) 
        {
            int temp = Random.Range(0, tevents.Length);
            if (temp != index1 && temp != index2) index3 = temp;
        }

        foreach (var ev in tevents)
        {
            ev.finished = false;
        }
        

        TriggeredEvent e1 = tevents[index1];
        TriggeredEvent e2 = tevents[index2];
        TriggeredEvent e3 = tevents[index3];
        spriteSpawner.setLocations(e1, e2, e3);
        evmanager.activeEvents[0] = e1;
        evmanager.activeEvents[1] = e2;
        evmanager.activeEvents[2] = e3;
    }


    public void chooseQuest(int number){
        if(choosingQuest){

            if(number == 1) activeQuest = quest1;
            else if(number == 2) activeQuest = quest2;
            else activeQuest = quest3;


            spriteSpawner.setQuestLocation(activeQuest.ev);
            evmanager.activequest = activeQuest;
            
            questholder.SetActive(false);
            manager.running = true;
            choosingQuest = false;
      }
        
    }
}
