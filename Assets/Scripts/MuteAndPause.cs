﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MuteAndPause : MonoBehaviour
{
    public static MuteAndPause instance;
    public GameObject levelComp;
    public Button button_mute, button_pause,button_exit;
    public Sprite mute, no_mute, pause, play;
    bool pauser = false, muter = false;
    public Text brick_count;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            gameObject.SetActive(true);

        }
        button_mute.onClick.AddListener(mute_sound);
        button_pause.onClick.AddListener(pause_play);
        button_exit.onClick.AddListener(exit);
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
    void exit()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
