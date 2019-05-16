using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MuteAndPause : MonoBehaviour
{
    public static MuteAndPause instance;
    public GameObject levelComp;
    public Button button_mute, button_pause,button_exit,button_mute2;
    public Sprite mute, no_mute, pause, play;
    bool pauser = false, muter = false;
    public Text brick_count;
    // Start is called before the first frame update
    void Start()
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
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            gameObject.SetActive(true);

        }
        button_mute.onClick.AddListener(MuteSound);
        button_mute2.onClick.AddListener(MuteSound);
        button_pause.onClick.AddListener(PausePlay);
        button_exit.onClick.AddListener(exit);
        if(PlayerPrefs.GetInt("Sound", 1) == 0)
        {
            MuteSound();
        }
    }

    void PausePlay()
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

    public void MuteSound()
    {

        if (muter == false)
        {
            button_mute.image.sprite = no_mute;
            muter = true;
            AudioListener.pause = true;
            PlayerPrefs.SetInt("Sound", 0);
            if (PlayerPrefs.GetInt("Lang", 1) == 1){
                if (button_mute2 != null)
                {
                    button_mute2.GetComponentInChildren<Text>().text = "Kapalı";
                }
            }
            else
            {
                if (button_mute2 != null)
                {
                    button_mute2.GetComponentInChildren<Text>().text = "Off";
                }
            }
            
        }
        else
        { 
            button_mute.image.sprite = mute;
            muter = false;
            AudioListener.pause = false;
            PlayerPrefs.SetInt("Sound", 1);
            if (PlayerPrefs.GetInt("Lang", 1) == 1)
            {
                if (button_mute2 != null) { 
                button_mute2.GetComponentInChildren<Text>().text = "Açık";
                }
            }
            else
            {
                if(button_mute2 != null) {
                    button_mute2.GetComponentInChildren<Text>().text = "On";
                }
            }
        }
    }
    void exit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
        Destroy(Game.instance.gameObject);
        GameOver.instance.health = 2;
        Play.instance.ResetBall();
        Play.instance.ResetStick();
        Game.instance.ResetSpeed();
        Lang.instance.gameObject.SetActive(true);
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
