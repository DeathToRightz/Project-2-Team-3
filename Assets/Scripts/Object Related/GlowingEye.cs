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
    [SerializeField] List<GameObject> numberOfBirds = new List<GameObject>();
    [SerializeField] List<GameObject> windAreas = new List<GameObject>();
    private PlayerMovement _playerRef;
    private void Awake()
    {
        _objectRenderer = GetComponent<Renderer>();
        _playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       
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
        
        _playerRef.PlayerInput.Disable();
        ToggleBirdsAndWind(numberOfBirds, windAreas, false);
        eyeCameraView.SetActive(true);
         yield return new WaitForSeconds(delay);
        ToggleBirdsAndWind(numberOfBirds, windAreas, true);
        eyeCameraView.SetActive(false);        
        _playerRef.PlayerInput.Enable();
    }
   
    public void ChangeColorBackEvent()
    {
        darken = true;
    }



    private void ToggleBirdsAndWind(List<GameObject> birds, List<GameObject> windAreas, bool turnOn)
    {
        if (!turnOn)
        {
            for (int i = 0; i <= birds.Count - 1; i++)
            {
                birds[i].GetComponent<DemonFOV>().enabled = false;
                birds[i].GetComponent<DemonPatrolling>().enabled = false;
            }
            if(windAreas != null)
            {
                for (int i = 0; i <= windAreas.Count - 1; i++)
                {
                    windAreas[i].GetComponent<WindArea>().enabled = false;
                }
            }

        }
        else
        {
            for (int i = 0; i <= birds.Count - 1; i++)
            {
                birds[i].GetComponent<DemonFOV>().enabled = true;
                birds[i].GetComponent<DemonPatrolling>().enabled = true;
            }
            if (windAreas != null)
            {
                for (int i = 0; i <= windAreas.Count - 1; i++)
                {
                    windAreas[i].GetComponent<WindArea>().enabled = true;
                }
            }
        }
        
    }
}
