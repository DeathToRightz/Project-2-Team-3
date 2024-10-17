using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    private static FadeTransition _instance;
    [SerializeField, Tooltip("Image to be fade in and out.")] private Image _fadeImage;
    private float _currentImageAlpha;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            
            DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        //FadeOut(1);
        SceneManager.activeSceneChanged += CheckForDifferentScene;
    }

    public void FadeIn(float incomingTimer) //When calling the FadeIn/FadeOut it will ask for a timer for prolong the transition
    {
        StartCoroutine(SetAlpha(1,incomingTimer));
    }
    
    public void FadeOut(float incomingTimer)
    {
        StartCoroutine(SetAlpha(0,incomingTimer));
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithFade(float fadeDelay, string nameOfScene)
    {
        StartCoroutine(DelaySceneTransitions(fadeDelay, nameOfScene));
    }
    
    private IEnumerator DelaySceneTransitions(float fadeDelay, string nameOfScene)
    {
        FadeIn(fadeDelay);
        yield return new WaitUntil((() => Math.Abs(_currentImageAlpha - 1) < .0001));
        SceneManager.LoadScene(nameOfScene);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(nameOfScene).isLoaded && Math.Abs(_currentImageAlpha - 1) < .0001);
        FadeOut(fadeDelay);
    }

    private IEnumerator SetAlpha(float alphaValue, float setTimer)
    {
        alphaValue = Mathf.Clamp(alphaValue ,0, 1);
        Color newColor = _fadeImage.color;

        while (Math.Abs(newColor.a - alphaValue) > .0001f)
        {
            newColor.a = Mathf.MoveTowards(newColor.a, alphaValue, Time.deltaTime / setTimer);
            _fadeImage.color = newColor;
            _currentImageAlpha = newColor.a;
            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckForDifferentScene(Scene currentScene, Scene newScene)
    {
        FadeOut(3);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_fadeImage == null)
        {
            Debug.LogWarning("Fade Image has not yet been assigned", this);
        }
    }

#endif
}
