using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] clips;
    public AudioClip[] musicClips;
    public AudioSource music;
    int i=0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAudio(int number, float time){
        audio.clip = clips[number];
        audio.Play();
        Invoke("StopAudio", time);
    }

    private void StopAudio()
    {
        audio.Stop();
    }

    void StartMusic()
    {   
        music.clip = musicClips[i];
        music.Play();
        i++;
        if(i>=musicClips.Length)
        i=0;
        Invoke("StartMusic", music.clip.length+0.5f);    
    }
}
