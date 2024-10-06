using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "New Animation", menuName = "Animations")]
public class Animation_SO : ScriptableObject
{
    [SerializeField] public string nameOfAnimation;    
    [SerializeField] public Animator m_animator;
    [SerializeField] public Animation m_animation;
    [SerializeField] public bool triggerOnEnter;
}

   

