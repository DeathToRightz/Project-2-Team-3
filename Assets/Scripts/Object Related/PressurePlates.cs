using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class PressurePlates : MonoBehaviour
{  
    private Animator animator;

    [SerializeField, Tooltip ("Attach the Pressue Plate Manager that is holding this plate and select the SendBoolCheckToDoor")] UnityEvent checkAllPlatesEvent = new UnityEvent();
    [SerializeField] UnityEvent<float> glowEye = new UnityEvent<float>();
    [SerializeField] UnityEvent<float> darkEye = new UnityEvent<float>();
    [SerializeField] GameObject eyeObject;
    public bool plateIsDown;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();  
    }
    private void Update()
    {
      
    }
    private void OnTriggerStay(Collider other) 
    {
        if (other.tag != "Pushable" && plateIsDown == true) //While the object is not tagged with pushable and the plate is already down   
                                                           // don't continue 
        {
                      
            return;
        }
                                                         //If everything above is true then start animation down, set plateIsDown to true
                                                        //And invoke the event for the Pressure Plate Manager
        animator.SetBool("Something on plate", true);    
        plateIsDown = true;
        glowEye.Invoke(3);
        checkAllPlatesEvent.Invoke();  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Pushable" && plateIsDown == false) //While the object is not tagged with pushable and the plate is not down
                                                            //don't continue
        {
            return;  
        }
                                                           //If everything above is true then start animation down, set plateIsDown to true
                                                           //And invoke the event for the Pressure Plate Manager
        animator.SetBool("Something on plate", false);
        plateIsDown = false;
        darkEye.Invoke(3);
        checkAllPlatesEvent.Invoke();         
    }

    /*void ChangeColorOverTime(float time)
    {
        intensity = Mathf.MoveTowards(intensity, 100,Time.deltaTime/5);
              
        Color finalColor = Color.white * intensity;

        eyeRenderer.material.SetColor("_EmissionColor", finalColor);
       

    }*/

   /* void ChangeColorofEye(float intensity)
    {
        Color finalColor = Color.white * intensity;
        //Color finalColor = Color.white;
        
        //eyeRenderer.material.SetColor("_EmissionColor", finalColor * 2 );
        eyeRenderer.material.SetColor("_EmissionColor", finalColor);
    }*/
}




