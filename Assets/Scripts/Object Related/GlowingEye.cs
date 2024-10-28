using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingEye : MonoBehaviour
{
    private Renderer _objectRenderer;
    private float glowIntensity, darkIntensity;
    private Color _finalColor;
    private void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();    
    }

    private void Update()
    {
       
       
    }
    public void ChangeColorOverTime(float timeDelay)
    {
        
        darkIntensity = 0;
     
        glowIntensity = Mathf.MoveTowards(glowIntensity, 1, Time.deltaTime / timeDelay);

         _finalColor = Color.white * glowIntensity;
         
        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);
    }

    public void ChangeColorBack(float timeDelay)
    {
        glowIntensity = 0;
       
        darkIntensity = Mathf.MoveTowards(darkIntensity, 1, Time.deltaTime / timeDelay);

        _finalColor = Color.black * darkIntensity;
       
        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);

    }
     
}
