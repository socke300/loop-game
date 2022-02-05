using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public bool running = false; // if the player is running or the game is paused by an event

    public float dayTime;
    float time = 0f;
    public static int day = 18;
    public static int year = 1;


    //camera
    public Color dayColor;
    public Color nightColor;
    public float durationDay = 60.0F;
    public GameObject cameraA;
    public Camera cam;
    public GameObject mainLight;

    //Light
    public Light2D sun;
    public Color dayLightColor;
    public Color nightLightColor;
    public float dayLightIntensity;
    public float nightLightIntensity;

    PlayerStats stats;
    StroyGenerator story;

    public GameObject endScreen;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI summaryText;
    


    void Start()
    {

        cam = cameraA.GetComponent<Camera>();
        sun = mainLight.GetComponent<Light2D>();
        stats = GetComponent<PlayerStats>();
        story = GetComponent<StroyGenerator>();
    }

    // Update is called once per frame
    public void Update()
    {

        if(stats.dead && running){
            //finish the game
            stats.score += stats.age;
            running = false;
            endScreen.SetActive(true);
            deathText.text = "Starb in einem Alter von " + stats.age + " Jahren.";
            nameText.text = story.nameGenerator() + " (Score: " + stats.score + ")";
            summaryText.text = story.writeStory();
        }

        //time handling
        if(running){
            dayTime += Time.deltaTime;
            time += Time.deltaTime;

            if(dayTime >= durationDay/2){
                day ++;
                stats.age += 2;
                dayTime = 0f;
            }
        
            float t = Mathf.PingPong(time, durationDay) / durationDay;
            cam.backgroundColor = Color.Lerp(dayColor, nightColor, t);
            sun.intensity = Mathf.Lerp(dayLightIntensity, nightLightIntensity, t);
            sun.color = Color.Lerp(dayLightColor, nightLightColor, t);
        }
    }

    public void exitToMenu(){
        SceneManager.LoadScene(0);
    }
}
