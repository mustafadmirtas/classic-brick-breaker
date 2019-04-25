using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteAndPause : MonoBehaviour
{
    public Button button_mute, button_pause;
    public Sprite mute, no_mute, pause, play;
    bool pauser = false, muter = false;
    // Start is called before the first frame update
    void Start()
    {
        button_mute.onClick.AddListener(mute_sound);
        button_pause.onClick.AddListener(pause_play);
    }

    void pause_play()
    {
        if(pauser == false)
        {
            
            button_pause.image.sprite = play;
            pauser = true;
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;
            button_pause.image.sprite = pause;
            pauser = false;
            AudioListener.pause = false;
        }
    }

    void mute_sound()
    {

        if (muter == false)
        {
            button_mute.image.sprite = no_mute;
            muter = true;
            AudioListener.pause = true;
        }
        else
        { 
            button_mute.image.sprite = mute;
            muter = false;
            AudioListener.pause = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
