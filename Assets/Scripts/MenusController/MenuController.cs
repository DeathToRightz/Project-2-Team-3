using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    
    public FadeTransition fadeTransitionRef;
    private AudioSource audioSource;  
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>() != null? audioSource = GetComponent<AudioSource>() : audioSource = gameObject.AddComponent<AudioSource>(); //If SoundManager object doesn't have a AudioSource                                                                                                                                                             //Creates one
        fadeTransitionRef = FindObjectOfType<FadeTransition>();
        Debug.Log(fadeTransitionRef);
    }
    private void Start()
    {
       
        fadeTransitionRef.FadeOut(3);
        SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Main Theme"));
    }
    private void Update()
    {
       
    }

    public void OnClickPlay()
    {   
        SoundManager.instance.PlaySound(transform.position,SoundManager.instance.FindSoundInfoByName("Start Game"));
        StartCoroutine(DelaySceneTransitions(3, 4, "Level1"));
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnClickHelp()
    {
        RandomButtonSFX();
        StartCoroutine(DelaySceneTransitions(3, 4, "HelpMenu"));
    }
    public void OnClickCredits()
    {
        RandomButtonSFX();
        StartCoroutine(DelaySceneTransitions(3, 4, "CreditsMenu"));      
    }
    public void OnClickBack()
    {
        RandomButtonSFX();
        StartCoroutine(DelaySceneTransitions(3, 4, "Main Menu"));
    }

    private IEnumerator DelaySceneTransitions(float FadeDelay,float loadingDelay, string nameOfScene)
    {
        fadeTransitionRef.FadeIn(FadeDelay);
        yield return new WaitForSeconds(loadingDelay);
        
        SceneManager.LoadScene(nameOfScene);
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
