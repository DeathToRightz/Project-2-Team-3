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

    public void FadeIn()
    {
        StartCoroutine(SetAlpha(1));
    }
    
    public void FadeOut()
    {
        StartCoroutine(SetAlpha(0));
    }

    private IEnumerator SetAlpha(float alphaValue)
    {
        alphaValue = Mathf.Clamp(alphaValue ,0, 1);
        Color newColor = _image.color;

        while (Math.Abs(newColor.a - alphaValue) > .0001f)
        {
            newColor.a = Mathf.MoveTowards(newColor.a, alphaValue, Time.deltaTime);
            _image.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }
}
