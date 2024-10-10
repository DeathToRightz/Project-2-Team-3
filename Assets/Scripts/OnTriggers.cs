using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class OnTriggers : MonoBehaviour
{
    //[SerializeField, Tooltip("Should the collider start with entering or leaving")] bool triggerOnEnter;

    [SerializeField, Tooltip("Should the associated collider destroy itself after triggering animation/event")] bool destroyAfterUse;
   
    [SerializeField] List<Animator> animations;
    
    [SerializeField] UnityEvent TriggerAnimationEvent = new UnityEvent();
    [SerializeField] UnityEvent<bool[]> BoolAnimationEvent = new UnityEvent<bool[]>();


    [SerializeField] string triggerByTag;
  
    [SerializeField] string animationTriggerName;
    [SerializeField] string animationBoolName;

    public bool plateDown = false;
    [SerializeField] enum BoxColliderState
    {
        triggerEnter,
        triggerExit,
        triggerStay
    }

    [SerializeField] BoxColliderState boxColliderState;
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


    public void TriggerAnimations()
    {
       for(int i = 0; i <= animations.Count-1; i++)
        {
            if (animationTriggerName != null)
            {
                animations[i].SetTrigger("Activate Animation");
            }
            
        }    
    }

    public void SetBoolForAnimations(bool incomingBool)
    {
        for (int i = 0; i <= animations.Count - 1; i++)
        {
            if (animationBoolName != null)
            {
                animations[i].SetBool(animationBoolName,incomingBool);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (boxColliderState != BoxColliderState.triggerEnter)
        {
            return;
        }
        if (other.tag == triggerByTag)
        {
           // BoolAnimationEvent.Invoke(true);        
        }
        if (destroyAfterUse)
        {
            Destroy(gameObject);
        }
      
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == triggerByTag)
        {
            plateDown = false;
          //  BoolAnimationEvent.Invoke(plateDown);
            if (destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (boxColliderState != BoxColliderState.triggerStay)
        {
            return;
        }
        if (other.tag == triggerByTag)
        {
            plateDown = true;
           // BoolAnimationEvent.Invoke(plateDown);
        }        
        if (destroyAfterUse)
        {
            Destroy(gameObject);
        }
        
    }


}
