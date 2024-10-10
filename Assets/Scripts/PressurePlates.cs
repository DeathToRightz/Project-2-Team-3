using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class PressurePlates : MonoBehaviour
{  
    private Animator animator;

    [SerializeField] UnityEvent checkAllPlatesEvent = new UnityEvent();

    
    public bool plateIsDown;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
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
        checkAllPlatesEvent.Invoke();
        
              
    }
}




