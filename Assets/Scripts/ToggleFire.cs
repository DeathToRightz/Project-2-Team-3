using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ToggleFire : MonoBehaviour
{
   
    private float fireMax = 10f;
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ToggleFlames());


    }

   
    IEnumerator ToggleFlames()
    {
        


        fireMax -= Time.deltaTime;
        fireMax = Mathf.Clamp(fireMax, 0, 10f);
        GetComponent<VisualEffect>().SetFloat("FlamesSize", fireMax);
        yield return null;

    }
}
