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
    [SerializeField] UnityEvent glowEye = new UnityEvent();
    [SerializeField] UnityEvent ChangeCameraView = new UnityEvent();
    [SerializeField] UnityEvent darkEye = new UnityEvent();
    private bool alreadyOn;
   
    public bool plateIsDown;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();  
    }
    private void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag != "Pushable" && plateIsDown == true || alreadyOn) || other.tag == "Player") //While the object is not tagged with pushable and the plate is already down   
                                                            // don't continue 
        {

            return;
        }
       
        ChangeCameraView.Invoke();
    }
    private void OnTriggerStay(Collider other) 
    {
        if ((other.tag != "Pushable" && plateIsDown == true) || other.tag == "Player") //While the object is not tagged with pushable and the plate is already down   
        {
                      
            return;
        }
        alreadyOn = true;
        //If everything above is true then start animation down, set plateIsDown to true
        //And invoke the event for the Pressure Plate Manager
        animator.SetBool("Something on plate", true);    
        plateIsDown = true;
       
        glowEye.Invoke();
        checkAllPlatesEvent.Invoke();  
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.tag != "Pushable" && plateIsDown == false) || other.tag == "Player") //While the object is not tagged with pushable and the plate is not down
                                                            //don't continue
        {
            return;  
        }
                                                           //If everything above is true then start animation down, set plateIsDown to true
        alreadyOn = false;                                                  //And invoke the event for the Pressure Plate Manager
        animator.SetBool("Something on plate", false);
        plateIsDown = false;
        ChangeCameraView.Invoke();
        darkEye.Invoke();
        checkAllPlatesEvent.Invoke();         
    }

}




