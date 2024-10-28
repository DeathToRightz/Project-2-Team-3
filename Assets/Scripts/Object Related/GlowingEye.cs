using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingEye : MonoBehaviour
{
    private Renderer _objectRenderer;
    private float glowIntensity, darkIntensity;
    private Color _finalColor;    
    public GameObject eyeCameraView;
    private float timeToGlow,timeToDarken;
    private bool darken;
    private void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();           
    }

    private void Update()
    {
        if (darken)
        {
            ChangeColorBack();
        }
    }
    public void ChangeColorOverTimeEvent(float delayStart)
    {     
        darken = false;
        
        while (timeToGlow <= delayStart)
        {
            timeToGlow += Time.deltaTime;
            return;
        }
       
        darkIntensity = 0;
        timeToDarken = 0;

        glowIntensity = Mathf.MoveTowards(glowIntensity, 1, Time.deltaTime / 3);

        _finalColor = Color.white * glowIntensity;

        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);

    }
    private void ChangeColorBack()
    {
        if (darkIntensity >= 1) { return; }

        while (timeToDarken <= 3) { timeToDarken += Time.deltaTime; return; }

        glowIntensity = 0;
        timeToGlow = 0;

        darkIntensity = Mathf.MoveTowards(darkIntensity, 1, Time.deltaTime / 3);

        _finalColor = Color.black * darkIntensity;

        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);

    }

    public void ChangeCameraViewEvent(float delay)
    {
        StartCoroutine(ChangeCameraView(delay));
    }


    IEnumerator ChangeCameraView(float delay)
    {
        eyeCameraView.SetActive(true);
         yield return new WaitForSeconds(delay);
        eyeCameraView.SetActive(false);
      

    }
   
    public void ChangeColorBackEvent()
    {
        darken = true;
    }
}
