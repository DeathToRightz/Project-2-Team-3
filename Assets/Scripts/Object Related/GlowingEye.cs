using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingEye : MonoBehaviour
{
    private Renderer _objectRenderer;
    private float glowIntensity, darkIntensity;
    public Material _glowingMat;
    public Material _defaultMat;
    private Color _finalColor;
    private Color _defaultColor;
   
    Color currentColor;
    private void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();
        _defaultColor = _defaultMat.color;
       // _finalColor = _glowingMat.color;
       
    }

    private void Update()
    {
        
    }
    public void ChangeColorOverTime(float timeDelay)
    {
        darkIntensity = 0;
        Debug.Log(glowIntensity);
        glowIntensity = Mathf.MoveTowards(glowIntensity, 1, Time.deltaTime / timeDelay);

         _finalColor = Color.white * glowIntensity;
          Debug.Log("Going down");
        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);
    }

    public void ChangeColorBack(float timeDelay)
    {
        glowIntensity = 0;
        Debug.Log(darkIntensity);
        darkIntensity = Mathf.MoveTowards(darkIntensity, 1, Time.deltaTime / timeDelay);

        _finalColor = Color.black * glowIntensity;
        Debug.Log("Going down");
        _objectRenderer.material.SetColor("_EmissionColor", _finalColor);

    }

    //intensity = Mathf.MoveTowards(intensity, 1, Time.deltaTime / time);

           // _finalColor = Color.white * intensity;
      //      Debug.Log("Going down");
//_objectRenderer.material.SetColor("_EmissionColor", _finalColor);
}
