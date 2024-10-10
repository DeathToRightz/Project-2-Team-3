using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class PressurePlates : MonoBehaviour
{
   
    private Animator animator;

   [SerializeField,Tooltip("Put the door that is going to be affected by the plate here")] UnityEvent<bool> checkPlatesEvent = new UnityEvent<bool>();

    [SerializeField] UnityEvent checkAllPlatesEvent = new UnityEvent();

    
    public bool plateIsDown;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        plateIsDown = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Pushable")
        {
          
            return;
        }
        if(plateIsDown == true)
        {
            return;
        }
        animator.SetBool("Something on plate", true);
        plateIsDown = true;
        checkAllPlatesEvent.Invoke();
        //checkPlatesEvent.Invoke(true);
        
       
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Pushable")
        {
            return;  
        }
        if (plateIsDown == false)
        {
            return;
        }
        plateIsDown = false;
        checkAllPlatesEvent.Invoke();
        animator.SetBool("Something on plate", false);
       // checkPlatesEvent.Invoke(false);
        
    }
}




