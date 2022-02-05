using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public string playername;
    public int age = 14;
    public int maxhp = 60;
    public int hp = 60;
    public int armour; // 1-60 
    public int damage; // 1-60

    public int intelligence; //1-20
    public int strength;    //1-20
    public int dexterity;   //1-20
    public int charisma;    //1-20


    //stats not shown to the player
    public int ehre; 

    public int riskolustig;
    public int vernünftig;

    public int mutig;
    public int ängstlich;


    public int gold;
    public int score;

    public bool statsOpen = false;
    public GameObject statsBar;
    public TextMeshProUGUI armourText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI intelligenceText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI charismaText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI goldText;

    public bool dead;

    //UI
    public TextMeshProUGUI timeText;

    void Awake()
    {
        playername = Menu.name;
    }

    void Start()
    {
        age = 18;
        maxhp = 55;
        hp = maxhp;
        armour = 5;
        damage = 5;
        gold = 25;
        intelligence = 1;
        strength = 1;
        dexterity = 1;
        charisma = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(gold <= 0) gold = 0;
        timeText.text = "Alter: " + age;

        if(hp > maxhp) hp = maxhp;
        if(age == 18) maxhp = 55;
        if(age == 18) maxhp = 65;
        if(age == 20) maxhp = 70;
        if(age == 22) maxhp = 75;
        if(age == 25) maxhp = 80;
        if(age == 35) maxhp = 75;
        if(age == 40) maxhp = 65;
        if(age == 45) maxhp = 60;
        if(age == 50) maxhp = 50;
        if(age == 55) maxhp = 40;
        if(age == 60) maxhp = 30;
        if(age == 65) maxhp = 20;
        if(age == 70) maxhp = 10;
        if(age == 80) maxhp = 10;
        if(age == 90) maxhp = 10;
        if(age == 100) maxhp = 5;
        if(age == 110) maxhp = 0;

        if(hp <= 0) dead = true;

        armourText.text = "" + armour;
        damageText.text = "" + damage;
        intelligenceText.text = "" + intelligence;
        strengthText.text = "" + strength;
        dexterityText.text = "" + dexterity;
        charismaText.text = "" + charisma;
        hpText.text = hp + "/" + maxhp;
        goldText.text = "" + gold;
    }


    public void showStats(){
        statsOpen = !statsOpen;
        statsBar.SetActive(statsOpen);
    }
 

    

}
