using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [SerializeField] FadeTransition fadeTransitionRef;
    [SerializeField] AudioClip[] buttonSounds;
    [SerializeField] AudioClip enterGame;
    private AudioSource audioSource;  
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>() != null? audioSource = GetComponent<AudioSource>() : audioSource = gameObject.AddComponent<AudioSource>(); //If SoundManager object doesn't have a AudioSource                                                                                                                                                           //Creates one
    }
    private void Start()
    {
        fadeTransitionRef.FadeOut(3);
       
    }

    
    public void OnClickPlay()
    {   
        audioSource.PlayOneShot(enterGame);
        StartCoroutine(DelaySceneTransitions(3, 4, "Level1"));
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnClickHelp()
    {
        SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Button1"));
        // audioSource.PlayOneShot(buttonSounds[Random.Range(0, buttonSounds.Length-1)]);
        StartCoroutine(DelaySceneTransitions(3, 4, "HelpMenu"));      
    }
    public void OnClickCredits()
    {
        audioSource.PlayOneShot(buttonSounds[Random.Range(0, buttonSounds.Length - 1)]);
        StartCoroutine(DelaySceneTransitions(3, 4, "CreditsMenu"));      
    }
    public void OnClickBack()
    {
        audioSource.PlayOneShot(buttonSounds[Random.Range(0, buttonSounds.Length - 1)]);
        StartCoroutine(DelaySceneTransitions(3, 4, "Main Menu"));
    }

    private IEnumerator DelaySceneTransitions(float FadeDelay,float loadingDelay, string nameOfScene)
    {
        fadeTransitionRef.FadeIn(FadeDelay);
        yield return new WaitForSeconds(loadingDelay);
        
        SceneManager.LoadScene(nameOfScene);
    }
}
