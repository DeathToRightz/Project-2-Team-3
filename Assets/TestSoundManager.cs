using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundManager : MonoBehaviour
{
    GameObject soundBackground;
    void Start()
    {
        
      
        soundBackground = SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Sound Background"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(soundBackground.activeSelf)
            {
                SoundManager.instance.soundPool.Release(soundBackground);
            }
            else
            {
               
                soundBackground = SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Sound Background"));
            }
          
        }

    }
}
