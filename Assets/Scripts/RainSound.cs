using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RainSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.instance)
        {
            SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Rain"));
        }
    }
}
