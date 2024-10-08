using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class OnTriggers : MonoBehaviour
{
    [SerializeField, Tooltip("Should the collider start with entering or leaving")] bool triggerOnEnter;

    [SerializeField, Tooltip("Should the associated collider destroy itself after triggering animation/event")] bool destroyAfterUse;
   
    [SerializeField] List<Animator> animations;
    
    [SerializeField] UnityEvent animationEvent = new UnityEvent();

    [SerializeField] string triggerByTag;
    private void Awake()
    {
        
        if (animations == null)
        {
            Debug.LogError("Need animator controller for reference");
        }
        if(triggerByTag == "")
        {
            Debug.LogError("Specify on what is going to trigger the collider on " + transform.name);
        }
    }


    public void StartAnimations()
    {
       for(int i = 0; i <= animations.Count-1; i++)
        {
            animations[i].SetTrigger("Activate Animation");
        }    
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!triggerOnEnter)
        {
            return;
        }
        if (other.tag == triggerByTag)
        {
            animationEvent.Invoke();
            
        }
        if (destroyAfterUse)
        {
            Destroy(gameObject);
        }
        Debug.Log("Set to Start");


    }

    private void OnTriggerExit(Collider other)
    {
        
            if (triggerOnEnter)
            {
                return;
            }
            if (other.tag == triggerByTag)
            {
                animationEvent.Invoke();
                if(destroyAfterUse)
                {
                    Destroy(gameObject);
                }
                Debug.Log("Set to Exit");
            }
        
        
    }



    
}
