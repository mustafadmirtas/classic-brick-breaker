using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundScript : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource source;


    private void Start()
    {
     
    }
    public void PlaySound(int i)
    {
        source.PlayOneShot(sounds[i]);
    }

}
