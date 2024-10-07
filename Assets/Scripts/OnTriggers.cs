using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
public class OnTriggers : MonoBehaviour
{
    [SerializeField, Tooltip("Should the collider start with entering or leaving")] bool triggerOnEnter;

    [SerializeField] Animator _animatorController;
    
    [SerializeField] Animator bobController;

    [SerializeField] UnityEvent bobVictimEvent = new UnityEvent();

    [SerializeField] string triggerBy;
    private void Awake()
    {
        
        if (_animatorController == null)
        {
            Debug.LogError("Need animator controller for reference");
        }
        if(triggerBy == "")
        {
            Debug.LogError("Specify on what is going to trigger the collider");
        }
    }


    public void BobVictimScene()
    {
        _animatorController.SetTrigger("Activate Animation");
        bobController.SetTrigger("Activate Animation");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
            if (!triggerOnEnter)
            {
                return;
            }
            if (other.name == triggerBy)
            {
                
            bobVictimEvent.Invoke();
            Debug.Log("Start Animation");                       
            }
        
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (bobController != null)
        {
            if (triggerOnEnter)
            {
                return;
            }
            if (other.name == triggerBy)
            {
                bobVictimEvent.Invoke();
                Debug.Log("Set to Exit");
            }
        }
        
    }



    
}
