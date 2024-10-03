using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MySoundManager : MonoBehaviour
{
    [SerializeField] List<SoundInfo> soundInfo = new List<SoundInfo>();

    [SerializeField] List<SoundInfo> sounds { get{ return soundInfo; } }


  
    
    [System.Serializable]
    public class SoundInfo
    {
        public string Name;
        public AudioClip AudioClip;
        public bool Mute;
        public bool PlayOnAwake;
        public bool loop;

        [SerializeField, Range(0, 1)]
        public float Volume;

        [SerializeField, Range(0, 1)]
        public float Pitch;

        public SoundInfo(string name)
        {

        }
          
    }
}
