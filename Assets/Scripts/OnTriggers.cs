using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class OnTriggers : MonoBehaviour
{
    [SerializeField,Tooltip("Should the collider start with entering or leaving")] bool triggerOnEnter;
   
    private Animator _animator;
   
    [SerializeField] string triggerBy;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null && transform.parent != null)
        {
            Debug.LogError("Need to add animator to " + transform.parent.name);
        }
        else if(_animator == null && transform.parent == null)
        {
            Debug.LogError("Need to add animator to " + name);
        }
        if(triggerBy == "")
        {
            Debug.LogError("Specify on what is going to trigger the collider");
        }
    }

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(!triggerOnEnter)
        {
            return;
        }
        if(other.name == triggerBy)
        {
            Debug.Log("Set to enter");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(triggerOnEnter)
        {
            return;
        }
        if (other.name == triggerBy)
        {
            Debug.Log("Set to Exit");
        }
    }
}
