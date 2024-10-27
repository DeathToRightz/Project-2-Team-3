using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEmmision : MonoBehaviour
{
    public Material putThingHere;
    private Color colorFromMat;
    private Color newColor;
    public GameObject putCubeHere;
    private Renderer cubeRenderer;
    void Start()
    {
        cubeRenderer = putCubeHere.GetComponent<Renderer>();
        colorFromMat = cubeRenderer.material.color;
       // colorFromMat = putCubeHere.GetComponent<MeshRenderer>().material.color;
        newColor = colorFromMat;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Press");
           // cubeRenderer.material.SetColor("_EmissionColor",  *= 2);
            //cubeRenderer.material.SetColor("_BaseColor", colorFromMat *= 2);
            
        }
    }
}
