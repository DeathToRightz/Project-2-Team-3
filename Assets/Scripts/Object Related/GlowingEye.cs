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
    private float timeHolder = 0;
    private void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();           
    }


    public void ChangeColorOverTimeEvent(float delayStart)
    {

        while (timeHolder <= delayStart)
        {
            timeHolder += Time.deltaTime;
            return;
        }
       
        darkIntensity = 0;

        glowIntensity = Mathf.MoveTowards(glowIntensity, 1, Time.deltaTime / 3);

        _finalColor = Color.white * glowIntensity;

        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);
    }

    public void ChangeColorBackEvent(float delayStart)
    {
        while (timeHolder <= delayStart)
        {
            timeHolder += Time.deltaTime;
            return;
        }
        glowIntensity = 0;
       
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
   
}
