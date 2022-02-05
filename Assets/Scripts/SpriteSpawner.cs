using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteSpawner : MonoBehaviour
{
    public GameObject[] locations;
    public GameObject[] questLocations;
    public Sprite eventImg;
    public Sprite questImg;
 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].GetComponent<SpriteRenderer>().sprite = null;
        }

        for (int i = 0; i < questLocations.Length; i++)
        {
            questLocations[i].GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setLocations(TriggeredEvent e1, TriggeredEvent e2, TriggeredEvent e3){
        int index1 = Random.Range(0, locations.Length);
        int index2 = -1;
        int index3 = -1;

        while (index2 == -1)
        {
            int temp = Random.Range(0, locations.Length);
            if (temp != index1) index2 = temp;
        }  

        while (index3 == -1) 
        {
            int temp = Random.Range(0, locations.Length);
            if (temp != index1 && temp != index2) index3 = temp;
        }

        locations[index1].GetComponent<SpriteRenderer>().sprite = eventImg;
        locations[index2].GetComponent<SpriteRenderer>().sprite = eventImg;
        locations[index3].GetComponent<SpriteRenderer>().sprite = eventImg;

        e1.location = locations[index1];
        e2.location = locations[index2];
        e3.location = locations[index3]; 
    }

    public void setQuestLocation(TriggeredEvent ev){
        ev.location = questLocations[Random.Range(0, questLocations.Length)];
        ev.location.GetComponent<SpriteRenderer>().sprite = questImg;
    }

    public void deactivate(GameObject location){
        
        for (int i = 0; i < locations.Length; i++)
        {
            if(location == locations[i]){
                locations[i].GetComponent<SpriteRenderer>().sprite = null;
                break;
            }
        }

        for (int i = 0; i < questLocations.Length; i++)
        {
            if(location == questLocations[i]){
                questLocations[i].GetComponent<SpriteRenderer>().sprite = null;
                break;
            }
        }
    }
}
