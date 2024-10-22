using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MenuController : MonoBehaviour
{
    private FadeTransition fadeTransitionRef;
    private AudioSource audioSource;
    [SerializeField] private GameObject _insideDoorCamera, _mainMenuPanel,_backgroundMusic;
    private GameObject _startGameSound, _endGameSound;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>() != null? audioSource = GetComponent<AudioSource>() : audioSource = gameObject.AddComponent<AudioSource>(); //If SoundManager object doesn't have a AudioSource                                                                                                                                                             //Creates one
        fadeTransitionRef = FindFirstObjectByType<FadeTransition>();
    }
    
    private void Start()
    {
        fadeTransitionRef.FadeOut(3);
        if(SceneManager.GetActiveScene().name == "End Level")
        {
            _endGameSound = SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("End Theme"));
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnClickPlay()
    {
       Destroy(_backgroundMusic);
      _startGameSound =  SoundManager.instance.PlaySound(transform.position,SoundManager.instance.FindSoundInfoByName("Start Game"));
        _insideDoorCamera.SetActive(true);
        _mainMenuPanel.SetActive(false);
        fadeTransitionRef.LoadSceneWithFade(3,"New Level1");
    }
    
    public void OnClickHelp()
    {
        RandomButtonSFX();
        fadeTransitionRef.LoadSceneWithFade(2,"HelpMenu");
    }
    
    public void OnClickCredits()
    {
        RandomButtonSFX();
        fadeTransitionRef.LoadSceneWithFade(2,"CreditsMenu");
    }
    
    public void OnClickBack()
    {
        RandomButtonSFX();
        fadeTransitionRef.LoadSceneWithFade(2,"MainMenu");
    }
    
    public void OnClickQuit()
    {
        Application.Quit();
    }

    private GameObject RandomButtonSFX()
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                return SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Button1"));


            case 1:
                return SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Button2"));


            case 2:
                return SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Button3"));


            case 3:
                return SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Button4"));


            case 4:
                return SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Button5"));

        }
        return null;
    }
}
