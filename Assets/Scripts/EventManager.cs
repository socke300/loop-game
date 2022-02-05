using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour
{
    [HideInInspector]
    public TriggeredEvent[] activeEvents;
    [HideInInspector]
    public TriggeredEvent activeEvent;
    
    [HideInInspector]
    public Quest activequest;
    string outcome;
    GameObject player;
    Manager manager;
    SpriteSpawner spriteSpawner;
    VillageQuestHandler questHandler;

    public GameObject shopLocation;
    public GameObject shopUi;
    public bool wasInShop = false; 

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI opt1Text;
    public TextMeshProUGUI opt2Text;
    public TextMeshProUGUI opt3Text;
    public TextMeshProUGUI outcomeText;
    public GameObject questholder;
    public GameObject textHolder;
    public GameObject outcomeHolder;
    bool choosingEvent;
    AudioManager audio;
    StroyGenerator story;
    PlayerStats stats;
    string name;

    float dist1;
    float dist2;
    float dist3;
    float dist4;
    float dist5;




    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
       manager = GetComponent<Manager>();
       spriteSpawner = GetComponent<SpriteSpawner>();
       questHandler = GetComponent<VillageQuestHandler>();
       audio = GetComponent<AudioManager>();
       story = GetComponent<StroyGenerator>();
       stats = GetComponent<PlayerStats>();
       name = stats.playername;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.running){
            dist1 = Vector2.Distance(activeEvents[0].location.transform.position, player.transform.position);
            dist2 = Vector2.Distance(activeEvents[1].location.transform.position, player.transform.position);
            dist3 = Vector2.Distance(activeEvents[2].location.transform.position, player.transform.position);
            dist4 = Vector2.Distance(activequest.ev.location.transform.position, player.transform.position);
            dist5 = Vector2.Distance(shopLocation.transform.position, player.transform.position);

            if(dist1 <= 0.05f && !activeEvents[0].finished) triggerEvent(activeEvents[0]);
            if(dist2 <= 0.05f && !activeEvents[1].finished) triggerEvent(activeEvents[1]);
            if(dist3 <= 0.05f && !activeEvents[2].finished) triggerEvent(activeEvents[2]);
            if(dist4 <= 0.05f && !activequest.ev.finished) triggerEvent(activequest.ev);
            if(dist5 <= 0.05f && !wasInShop) triggerShop();
        }
        

    }

     public void closeShop(){
        manager.running = true;
        shopUi.SetActive(false);
    }

    void triggerShop(){
        audio.playAudio(2, 1f);
        manager.running = false;
        wasInShop = true;
        shopUi.SetActive(true);
    }

    void triggerEvent(TriggeredEvent ev){
        wasInShop = false;
        questHandler.playerReady = true;
        audio.playAudio(2, 1f);
        activeEvent = ev;
        manager.running = false;
        choosingEvent = true;
        questholder.SetActive(true);
        titleText.text = ev.title;
        descriptionText.text = ev.description;
        opt1Text.text = ev.options[0];
        opt2Text.text = ev.options[1];
        opt3Text.text = ev.options[2];
    }

    public void chooseEvent(int number){
        
        if(choosingEvent){
            if(number == 1) outcome1(activeEvent.id);
            if(number == 2) outcome2(activeEvent.id);
            if(number == 3) outcome3(activeEvent.id);
            choosingEvent = false;
            outcomeText.text = outcome;
            textHolder.SetActive(false);
            outcomeHolder.SetActive(true);
            audio.playAudio(0, 1.5f);            
        }
    }

    public void closeEvent(){
        textHolder.SetActive(true);
        outcomeHolder.SetActive(false);
        questholder.SetActive(false); 
        manager.running = true; 
        activeEvent.finished = true;
        spriteSpawner.deactivate(activeEvent.location);

    }





    void outcome1(int id){
        int r = Random.Range(0,101);
        switch(id){
            case 1:
                if(stats.gold >= 10){
                    stats.gold -= 10;
                    stats.ehre++;
                    stats.charisma++;
                    outcome = "Der Bettler bedankte sich vielmals. " + name + " unterhielt sich noch einige Zeit mit dem Mann, bis er sich wieder auf den Weg ins Abendteuer machte.";
                    story.add(" zeigte er seine Grosszuegigkeit und teilte er sein Gold mit einem Bettler");
                }
                else{
                    outcome = "Der Bettler konnte ueber " + name + "s Witz nicht lachen. Natuerlich wusste " + name + ", dass er das Geld nicht hatte. Allerdings bereitete es ihm Freude den armen Bettler auszutricksen.";
                    story.add(" legte er einen Bettler rein."); 
                }
            break;
            case 2:
                if(r > 20){
                    if(stats.damage > 6){
                        stats.gold += 50;
                        stats.mutig++;
                        stats.strength++;
                        outcome = name + " zueckte seine Waffe und rannte los. Nachdem sein erster Hieb einen der Banditen schwer verletzte, ergriffen diese sofort die Flucht. Der Haendler bedankte sich und reichte " + name + " 50 Gold als Belohnung";
                        story.add(" rettete er einen Haendler vor Banditen.");
                    }
                    else{
                        stats.gold = 0;
                        stats.hp -= 20;
                        stats.riskolustig++;
                        outcome = "Bei " + name + "s Anblick begannen die Banditen zu lachen. Schnell ueberwaeltigten sie ihn und stahlen sein Gold.";
                        story.add(" wurde er ausgeraubt.");
                    }
                }
                else{
                    outcome = name + " rannte mit gezueckter Waffe los. Doch als er bei den Banditen ankam blickte er in die erstaunten Gesichter einer Hochtzeitsgemeinschaft. 'Vielleicht sollte ich mir eine neue Brille zulegen', dachte sich " + name + ".";
                }
            break;
            case 3:
                if(r > 50){
                    stats.hp += 10;
                    stats.strength++;
                    stats.riskolustig++;
                    outcome = name + " genoss ein staerkendes Pilzrisotto. " + name + " verstand sowieso nicht warum einige Leute so viel Angst vor unbekannten Pilzen hatten.";
                }
                else{
                    stats.hp -= 10;
                    stats.strength--;
                    stats.riskolustig++;
                    outcome = "Wie auch zu erwarten, vergiftete sich " + name + " mit den unbekannten Pilzen";
                    story.add(" erlitt er eine Lebensmittelvergiftung");
                }
            break;
            case 4:
                if(r > 70){
                    stats.riskolustig++;
                    outcome = "Wie sollte so ein wenig Regen auch einen wahren Helden aufhalten?";
                    story.add(" trotzte er allen Unwettern");
                }
                else{
                    stats.hp -= 10;
                    stats.riskolustig++;
                    outcome = "Der Marsch durch den stroemenden Regen hinterliess ihn mit einer ordentlichen Erkaeltung.";
                    story.add(" zog er sich eine Erkaeltung zu");
                }
            break;
            case 5:
                if(stats.gold > 10){
                    stats.intelligence++;
                    stats.vernünftig++;
                    outcome = "Auch wenn es nicht billig war, konnte der Heiler " + name + " helfen. Nebenbei erlernte " + name + " ein paar neue Tricks.";
                    story.add(" war er für ein paar Tage krank.");
                }
                else{
                    if(stats.charisma>5){
                        stats.vernünftig++;
                        stats.charisma++;
                        outcome = "Obwohl " + name + " nicht genug Geld hatte, half der Heiler ihm.";
                        story.add(" war er für ein paar Tage krank.");
                    }
                    else{
                        stats.vernünftig++;
                        stats.hp -= 10;
                        outcome = "Leider verlangte der Heiler für seine Dienste mehr, als " + name + " bezahlen konnte.";
                        story.add(" war er für ein paar Tage krank.");
                    }
                }
            break;
            case 6:
                if(stats.charisma>2 && r>50){
                    stats.charisma++;
                    outcome = name + " konnte die Dame leicht um den Finger wickeln. Sie verbrachten einen wunderschoenen Abend. Am naechsten Tag musste " + name + " leider aufbrechen, er konnte es sich nicht leisten weiter Zeit zu verlieren.";
                    story.add(" hatte er eine unvergessliche Nacht.");
                }
                else if(stats.charisma>2 && r>50){
                    stats.charisma++;
                    stats.hp -= 8;
                    outcome = name + " konnte die Dame leicht um den Finger wickeln. Sie verbrachten einen wunderschoenen Abend. Leider kam am naechsten Morgen der Ehemann der Dame aus der Kneipe wieder. Nach einem kurzen Fauskampf um das Herz der Dame, beschloss " + name + " liebr weiter zu ziehen.";
                    story.add(" hatte er eine unvergessliche Nacht, gefolgt von einem angespannten Morgen.");
                }
                else{
                    stats.charisma++;
                    outcome = "Leider war " + name + "s Charm etwas eingerostet. Naja, Uebung macht den Meister.";
                }
            break;
            case 7:
                stats.vernünftig++;
                outcome = "Wer nicht spielt, kann nicht verlieren.";
                story.add(" widerstand er der Versuchung zu spielen.");
            break;
            case 8:
                if(stats.gold > 15){
                    stats.gold -= 15;
                    if(r >25){
                        stats.dexterity++;
                        outcome = "Ein wirklich schoenes Paar Schuhe.";
                    }
                    else{
                        outcome = "Kaum war der Haendler aus der Sichtweite verschwunden, loeste sich schon die Sohle von den Schuhen.";
                        story.add(" wurde er von einem Haendler ueber den Tisch gezogen.");
                    }
                }
                else{
                    stats.intelligence --;
                    outcome = "Rechnen war wirklich nicht " + name + "s Staerke. Der Haendler musste ihm erklaeren, dass" + name + "zu wenig Gold hatte.";
                }
            break;
            case 9:
                stats.vernünftig ++;
                if(r > 50){
                    stats.gold += 10;
                    outcome = name + " griff in seine Tasche und fand ein paar Muenzen die er schon laengst vergessen hatte.";
                    story.add( " fand er ein wenig Gold.");
                }
                else{
                    stats.gold -= 10;
                    outcome = name + " bemerkte, dass seine Geldboerse ein Loch hatte. Einiges von dem Inhalt fehlte.";
                    story.add( " verlor er ein wenig Gold.");
                }
            break;
            case 100:
                if(r > 30){
                    stats.riskolustig++;
                    if(stats.armour>12){
                        stats.strength++;
                        outcome = "Ohne einen Kratzer zu erleiden schlug " + name + " die Kobolde in die Flucht.";
                    }
                    else{
                        stats.strength++;
                        stats.hp -= 10;
                        outcome = "Mutig stellte " + name + " sich den Kobolden. Er war siegreich, doch wurde im Kampf verletzt.";
                    }
                    stats.gold += 10;
                    story.add(" besiegte er eine Horde wilder Kobolde.");
                }
                else{
                    if(stats.armour>12){
                        stats.hp -= 10;
                        outcome = "Die Kobolde umrringten " + name + ". Auf Grund seiner Verletzungen musste er den Rueckzug antretten.";
                    }
                    else{
                        stats.hp -= 20;
                        outcome = "Der Plan ging nicht auf. " + name + " musste schwer verwundet den Rueckzug antretten.";
                    }
                    story.add(" wurde er von einer Hand voll Kobolde besiegt.");    
                }

                break;
            case 101:
                if(stats.damage>8){
                    stats.mutig++;
                    if(r>50){
                        outcome = "Beim Anblick von " + name + "s Schwert began der Verbrecher zu zittern. Er ergab sich auf der stelle.";
                    }
                    else{
                        stats.hp -= 10;
                        outcome = name + "schaffte es den Verbrecher zu ueberwaeltigen, doch wurde dabei verletzt.";
                    }
                    stats.gold += 10;
                    story.add(" fing er einen Gesuchten Verbrecher.");
                }
                else{
                    stats.riskolustig++;
                    if(stats.armour>10){
                        stats.hp -= 5;
                        outcome = name + " geling es den Verbrecher zu fangen, auch wenn er dabei leicht verletzt wurde.";
                        story.add(" fing er einen Gesuchten Verbrecher.");
                    }
                    else{
                        stats.hp -= 20;
                        outcome = "Der Plan ging nicht auf. " + name + " musste schwer verwundet den Rueckzug antretten.";
                        story.add(" wurde er von einem Krimminellen verwundet.");
                    }
                        
                }
            break;
            case 102:
                stats.mutig++;
                if(r > 80 || stats.damage > 14){
                    if(stats.armour > 15){
                        stats.hp -= 10;
                        outcome = "Mit der Staerke eines Riesen erschlug " + name + " den Troll. Ein wahres Bild fuer die Goetter.";
                        
                    }
                    else{
                        stats.hp -= 20;
                        outcome = name + " erschlug den Troll, allerdings nicht ohne ein paar heftige Hiebe zu kassieren.";
                    }
                    stats.gold += 40;
                    story.add(" erschlug er einen Troll.");
                }else{
                    if(stats.armour > 10){
                        stats.hp -= 30;
                        outcome = "Der Troll war zu maechtig, " + name + " musste fliehen.";
                        story.add(" schaffte er es nicht einen Troll zu besiegen.");
                    }
                    else{
                        stats.hp = 0;
                        outcome = name + " Wer haette gedacht, dass " + name + " einen Troll nicht alleine toeten konnte.";
                        story.add(" wurde er von einem Troll erschlagen.");
                    }
                }
            break;
            case 103:
            break;
            case 104:
            break;
            case 105:
            break;
            case 106:
            break;
            default:
            break;
        }
    }

    void outcome2(int id){
        int r = Random.Range(0,101);
        switch(id){
            case 1:
                if(r > 50){
                    stats.gold += 5;
                    stats.ehre -= 2;
                    outcome = name + " holte aus und schlug den Bettler zu boden. Was fiel dem dreisten Kerl auch ein ihn anzusprechen. Als Entschaedigung bediente er sich am Goldbeutel des Bettlers.";
                    story.add(" gewann er einen Kampf mit einem Bettler");
                }
                else{
                    stats.hp -= 6;
                    stats.ehre--;
                    outcome = name + " holte aus, doch der Bettler war schneller. Er zog eine Klinge und traf " + name + " ins Bein. Danach rannte der Bettler so schnell er konnte.";
                    story.add(" verlor er einen Kampf mit einem Bettler");
                }
            break;
            case 2:
                if(stats.dexterity > 5 || r > 50){
                    stats.dexterity++;
                    stats.ängstlich++;
                    stats.vernünftig++;
                    outcome = name + " schlich sich um die Banditen herrum. Die Hilferufe des Haendlers ignorierte er gekonnt. Zum Glueck hatte ihn niemand bermerkt.";
                    story.add(" meidete er heldenmutig einen Kampf mit Banditen.");
                }
                else{
                    stats.ängstlich++;
                    outcome = "Leider war " + name + " nicht so geschickt im Schleichen wie er dachte. Die Banditen bermerkten ihn und lachten ihn aus.";
                }
            break;
            case 3:
                stats.intelligence++;
                stats.vernünftig++;
                outcome = "Wahrscheinlich eine weise Entscheidung, dennoch fragte sich " + name + " noch jahrelang, was wohl haette passieren koennen.";
            break;
            case 4:
                if(r < 60){
                    stats.intelligence++;
                    stats.vernünftig++;
                    stats.hp += 2;
                    outcome = name + "Fand Unterschlupf auf einem Bauernhof. Der freundlich Bauer bot ihm sogar eine waermende Mahlzeit an. 'In Zeiten wie diesen muessen wir alle zusammenhalten.' ";
                    story.add(" lernte er einen netten Bauern kennen.");
                }
                else{
                    if(stats.strength > 5){
                        stats.strength++;
                        stats.vernünftig++;
                        outcome = name + "Fand Unterschlupf in einer Hoehle. Allerdings war er nicht der einzige, der dort Unterschlupf suchte. Jedoch hielt " + name + "s einschuechternde Gestalt den Baeren davon ab ihn anzugreifen.";
                        story.add(" teilte er sich eine Hoehle mit einem Baeren");
                    }
                    else{
                        stats.vernünftig++;
                        stats.hp -= 9;
                        outcome = name + "Fand Unterschlupf in einer Hoehle. Allerdings war er nicht der einzige, der dort Unterschlupf suchte. Ein Baer griff ihn an und verletzte ihn.";
                        story.add(" teilte er sich eine Hoehle mit einem Baeren");
                    }
                }
            break;
            case 5:
                if(stats.strength>5){
                    stats.strength++;
                    stats.riskolustig++;
                    outcome = "Nach ein paar Tagen war die Krankheit schon verflogen.";
                    story.add(" war er fuer ein paar Tage krank.");
                }
                else{
                    stats.hp -= 12;
                    stats.riskolustig++;
                    outcome = "Wie zu erwarten, wurde die Krankheit schlimmer. Nach einer Woche starken Fieber, war " + name + " zwar geschwaecht, aber gesund genug um weiter zu gehen.";
                    story.add(" war er fuer ein paar Tage schwer krank.");
                }
            break;
            case 6:
                stats.ängstlich++;
                outcome = "Wer nichts wagt, der nicht gewinnt.";
            break;
            case 7:
                if(stats.gold > 0){
                    if(r>50){
                        stats.gold = stats.gold*2;
                        stats.riskolustig++;
                        outcome = name + " staunte nicht schlecht. Bereits nach ein paar Runden hatte er seinen Einsatz verdoppelt. Als die Stimmung in der Runde zu kippen begann, beschloss " + name + " lieber mit dem Gewinn zu verschwinden.";
                        story.add(" gewann er "+ stats.gold/2 +" beim Glueckspiel");
                    }
                    else{
                        stats.gold = 0;
                        stats.riskolustig++;
                        outcome = "Als " + name + " bereits all sein Gold verspielt hatte, wurde ihm klar, dass die Maenner mit gezinkten Wuerfel spielen. Als er sein Geld zurueckforderte, machten ihm die gezogenen Waffen der Maenner klar, dass er zwar kein Gold mehr zu verlieren, ihm sein Leben aber noch bleibt. Geknickt, aber eine Lektion reicher ging er seines Weges.";
                        story.add(" verlor er alles beim Spiel mit gezinkten Wuerfeln.");
                    }
                }
                else{
                    outcome = name + " wollte zwar mitspielen, doch ohne Einsatz liessen die Maenner ihn nicht an ihren Tisch.";
                }
            break;
            case 8:
                if(stats.gold > 15){
                    stats.gold -= 15;
                    if(r >25){
                        stats.intelligence++;
                        outcome = "Ein wirklich gutes Buch.";
                    }
                    else{
                        outcome = "Leider hatte " + name + " vergessen, dass er nicht lesen konnte. Das Buch konnte er trotzdem nicht mehr zurueck geben." ;
                    }
                }
                else{
                    stats.intelligence --;
                    outcome = "Rechnen war wirklich nicht " + name + "s Staerke. Der Haendler musste ihm erklaeren, dass" + name + "zu wenig Gold hatte.";
                }
            break;
            case 9:
                stats.mutig ++;
                if(r > 50){
                    stats.damage += 2;
                    outcome = "Am Strassenrand fand " + name + " ein neues Schwert.";
                    story.add( " fand er ein neues Schwert.");
                }
                else{
                    stats.damage -= 2;
                    outcome = "Ploetzlich fiel " + name + " eine grosse Schramme in der Klinge seines Schwertes auf.";
                }
            break;
            case 100:
                if(r > 70 || stats.dexterity > 3){
                    stats.vernünftig++;
                    stats.dexterity++;
                    stats.ehre--;
                    if(stats.dexterity > 5){
                        outcome = "In der Nacht schlich sich " + name + " an das Lager herran. Still und heimlich schlitzte er einem nach dem anderen die Gurgel auf." ;
                    }
                    else{
                        stats.hp -= 7;
                        outcome = name + " schlich sich bei Nacht an das Lager herran. Die meisten Kobolde erledigte er im Schlaf, gegen eine Hand voll musste er kaempfen.";
                    }
                    stats.gold += 10;
                    story.add(" meuchelte er Kobolde im Schlaf.");
                }
                else{
                    if(stats.damage>9){
                        stats.hp -= 8;
                        outcome = "Kaum war " + name + " im Lager, schon wurde er bemerkt. Zum Glueck war " + name + " besser mit dem Schwert als bei der Heimlichkeit.";
                        stats.gold += 10;
                        story.add(" Besiegte er haarscharf ein paar Kobolde."); 
                    }
                    else{
                        stats.hp -= 20;
                        outcome = " Die Kobolde entdeckten " + name + " noch bevor er das Lager erreichte. Schwer verletzt musste er fliehen.";
                        story.add(" wurde er von einer Hand voll Kobolde besiegt.");    
                    }
                    
                }
            break;
            case 101:
                if(r > 70 || stats.charisma > 3){
                    stats.vernünftig++;
                    stats.charisma++;
                    if(stats.charisma > 6){
                        outcome = "Der Verbrecher konnte " + name + "s Worten nichts entgegensetzen. Er war von " + name + "s Charm so gefangen, dass er ihm sogar seine Beute ueberliess." ;
                        stats.gold += 20;
                    }
                    else{
                        outcome =  "Nach langem hin und her konnte " + name + " den Verbrecher ueberzeugen sich zu ergeben.";
                    }
                    stats.gold += 10;
                    story.add(" nutzte er Worte als Waffe um einen Verbrecher zu fangen.");
                }
                else{
                    stats.hp -= 25;
                    outcome =  "Scheinbar war " + name + " nicht so ueberzeugend wie er dachte. Das einzige was seine Rede erntete, war einen Dolch in die Magengrube.";
                    story.add(" wurde er beim Redenschwingen erstochen.");
                }
            break;
            case 102:               
                stats.vernünftig++;
                if(stats.charisma > 4){
                    stats.charisma++;
                    stats.gold += 40;
                    outcome = "Die Jaeger liessen sich von " + name + " ueberzeugen. Zusammen erschlugen sie den Troll.";
                    story.add(" besiegte er mit einiger Hilfe einen Troll.");
                }
                else if(stats.gold > 25){
                    stats.charisma++;
                    stats.gold += 10;
                    outcome = "Gegen eine ordentliche Summe Gold waren die Jaeger bereit " + name + " zu helfen. Zusammen erschlugen sie den Troll.";
                    story.add(" besiegte er mit einiger Hilfe einen Troll.");
                }else{
                    outcome = "Die Jaeger hatten keine Lust fuer einen Fremden zu sterben.";
                }
            break;
            case 103:
            break;
            case 104:
            break;
            case 105:
            break;
            case 106:
            break;
            default:
            break;
        }
    }

    void outcome3(int id){
        int r = Random.Range(0,101);
        switch(id){
            case 1:
                outcome = "Der Bettler bedankte sich, doch irgendwie hatte " + name + " nicht das Gefuehl, dass er es ernst meinte";
            break;
            case 2:
                if(r > 20){
                    stats.ehre--;
                    stats.damage += 5;
                    outcome = name + "Das Warten lohnte sich. " + name + " konnte eine schoene neue Waffe und etwas Gold ergattern.";
                    story.add(" raubte er einen Haendler aus.");
                }
                else{
                    stats.ehre--;
                    outcome = "Leider gab es nichts mehr zu holen.";
                    story.add(" brachten Banditen ihn um seine wohlverdiente Beute.");
                }
            break;
            case 3:
                if(r> 40){
                    stats.hp = 0;
                    outcome = "Was hatte er auch erwartet?";
                    story.add( " starb er, weil die Versuchung Pilze zu rauchen gross war.");
                }
                else{
                    stats.intelligence += 5;
                    stats.riskolustig += 3;
                    outcome = "Endlich erschloss sich ihm der Sinn des Lebens.";
                    story.add( " rauchte er Pilze, die ihm so einiges klar machten.");
                }
            break;
            case 4:
              stats.hp -= 25;
              outcome = "Unter dem Baum zu stehen hielt ihn zunaechst halbwegs trocken, jedoch wurde er vom Blitz getroffen.";
              story.add( " wurde er vom Blitz getroffen");
            break;
            case 5:
                if(stats.intelligence>3){
                    stats.intelligence++;
                    outcome = "Zum Glueck fand " + name + " bald die richtigen Kraeuter";
                    story.add(" war er fuer ein paar Tage krank.");
                }
                else{
                    stats.hp -= 15;
                    outcome = "Leider kannte " + name + " sich nicht besonders gut mit Kraeutern aus und waehlte ausversehen eine giftige Pflanze";
                    story.add(" vergiftete er sich.");
                }
            break;
            case 6:
                outcome = "Die Dame schaute " + name + " noch eine Weile hinterher und ging dann ihres Weges.";
            break;
            case 7:
                if((stats.dexterity > 4 && r > 40) || r > 85){
                    stats.dexterity++;
                    stats.gold = stats.gold*3;
                    stats.riskolustig++;
                    outcome = "Der Plan ging auf." + name + "verliess den Spieltisch mit mehr Geld, als er je zuvor gesehen hatte.";
                    story.add(" gewonn er beim Spiel mit gezinkten Wuerfeln.");
                }
                else{
                    stats.hp -= 10;
                    stats.riskolustig++;
                    outcome = name + " flog schneller auf, als er seine Gewinne haette zaehlen koennen. Nach einer ordentlichen Tracht Pruegel liessen die Maenner ihn ziehen.";
                    story.add(" wurde er beim betruegen erwischt.");
                }
            break;
            case 8:
                outcome = name + " ging unbeirrt weiter.";
            break;
            case 9:
                stats.charisma++;
                outcome = "Trotz der Trauer wurde " + name + " ploetzlich klar, dass es Menschen gibt, denen er was bedeutet.";
                story.add( " hatte er eine Erleuchtung."); 
            break;
            case 100:
                stats.ängstlich++;
                outcome = name + " zog es vor Schlachten in seinem Kopf und nicht auf dem Feld auszutragen.";
                story.add(" floh er vor ein paar Kobolden.");
            break;
            case 101:
                stats.ängstlich++;
                outcome = name + " zog es vor Schlachten in seinem Kopf und nicht auf dem Feld auszutragen.";
                story.add(" floh er vor einem Verbrecher.");
            break;
            case 102:
                stats.ängstlich++;
                outcome = name + " zog es vor Schlachten in seinem Kopf und nicht auf dem Feld auszutragen.";
                story.add(" floh er vor einem Troll.");
            break;
            case 103:
            break;
            case 104:
            break;
            case 105:
            break;
            case 106:
            break;
            default:
            break;
        }  
    }

}
