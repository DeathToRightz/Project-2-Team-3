using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    private Image _image;

    // Start is called before the first frame update
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void FadeIn(float incomingTimer) //When calling the FadeIn/FadeOut it will ask for a timer for prolong the transition
    {
        StartCoroutine(SetAlpha(1,incomingTimer));
    }
    
    public void FadeOut(float incomingTimer)
    {
        StartCoroutine(SetAlpha(0,incomingTimer));
    }

    private IEnumerator SetAlpha(float alphaValue, float setTimer)
    {
        alphaValue = Mathf.Clamp(alphaValue ,0, 1);
        Color newColor = _image.color;

        while (Math.Abs(newColor.a - alphaValue) > .0001f)
        {
          
            newColor.a = Mathf.MoveTowards(newColor.a, alphaValue, Time.deltaTime / setTimer);
            _image.color = newColor;
            yield return new WaitForEndOfFrame();
          
        }
    }
}
