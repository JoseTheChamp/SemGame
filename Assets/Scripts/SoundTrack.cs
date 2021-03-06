using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour { //zajišťuje nepřerušení muziky mezi scénami.
    private static bool playing = false;
    private void Start()
    {
        if(!playing){
            playing = true;
            this.GetComponent<AudioSource>().Play();
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
