using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music_handler : MonoBehaviour
{
    [Header("Meta")]
    public AudioClip[] playlist;
    public AudioSource myAudio;
    
    void Start(){
        //we want only one music player. I could have putted it in the title screen and make it play song accordingly to the scene
        //but I got too lazy and didnt had enought time to do it, so I made that simpler thing instead
        GameObject[] otherMusicPlayer = GameObject.FindGameObjectsWithTag("MusicPlayer");
        if(otherMusicPlayer.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        myAudio.clip = playlist[SceneManager.GetActiveScene().buildIndex];
        myAudio.Play();
    }
}
