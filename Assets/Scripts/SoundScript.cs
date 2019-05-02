using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundScript : MonoBehaviour
{
    public static SoundScript instance;
    public AudioClip[] sounds;
    public AudioSource source;


    private void Start()
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
    }
    public void PlaySound(int i)
    {
        source.PlayOneShot(sounds[i]);
    }

}
