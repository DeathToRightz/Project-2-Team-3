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

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private IEnumerator SetAlpha(float alphaValue, float setTimer)
    {
        alphaValue = Mathf.Clamp(alphaValue ,0, 1);
        Color newColor = _fadeImage.color;

        while (Math.Abs(newColor.a - alphaValue) > .0001f)
        {
            newColor.a = Mathf.MoveTowards(newColor.a, alphaValue, Time.deltaTime / setTimer);
            _fadeImage.color = newColor;
            yield return new WaitForEndOfFrame();
        }
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
