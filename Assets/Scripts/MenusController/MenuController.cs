using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [SerializeField] FadeTransition fadeTransitionRef;

    private void Start()
    {
        fadeTransitionRef.FadeOut();
    }
    public void OnClickPlay()
    {
        fadeTransitionRef.FadeIn();
        SceneManager.LoadScene("Level1");
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnClickHelp()
    {
        fadeTransitionRef.FadeIn();
        SceneManager.LoadScene("HelpMenu");
    }
    public void OnClickCredits()
    {
        fadeTransitionRef.FadeIn();
        SceneManager.LoadScene("CreditsMenu");
    }
    public void OnClickBack()
    {
        fadeTransitionRef.FadeIn();
        SceneManager.LoadScene("Main Menu");
    }
}
