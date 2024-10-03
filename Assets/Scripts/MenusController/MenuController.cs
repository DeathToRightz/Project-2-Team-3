using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [SerializeField] FadeTransition fadeTransitionRef;
    
    private void Awake()
    {
       
    }
    private void Start()
    {
        fadeTransitionRef.FadeOut(3);
       
    }

    
    public void OnClickPlay()
    {
       
        
        
        StartCoroutine(DelaySceneTransitions(3, 4, "Level1"));
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnClickHelp()
    {

        StartCoroutine(DelaySceneTransitions(3, 4, "HelpMenu"));
       
    }
    public void OnClickCredits()
    {
        StartCoroutine(DelaySceneTransitions(3, 4, "CreditsMenu"));
        
    }
    public void OnClickBack()
    {
        StartCoroutine(DelaySceneTransitions(3, 4, "Main Menu"));
        
    }

    private IEnumerator DelaySceneTransitions(float FadeDelay,float loadingDelay, string nameOfScene)
    {
        fadeTransitionRef.FadeIn(FadeDelay);
        yield return new WaitForSeconds(loadingDelay);
        
        SceneManager.LoadScene(nameOfScene);
    }
}
