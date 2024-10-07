using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class OnTriggers : MonoBehaviour
{
    [SerializeField, Tooltip("Should the collider start with entering or leaving")] bool triggerOnEnter;

    [SerializeField] Animator _animatorController;

    [SerializeField] Animator bobController;
   
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

    private void OnTriggerEnter(Collider other)
    {
        if(bobController != null)
        {
            if (!triggerOnEnter)
            {
                return;
            }
            if (other.name == triggerBy)
            {
                Debug.Log("Start Animation");
                _animatorController.SetTrigger("Activate Animation");
            }
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
                Debug.Log("Set to Exit");
            }
        }
        
    }
}
