using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableStats : ScriptableObject
{
    [Header("INPUT")] 
    [Tooltip("Makes all Input snap to an integer. Prevents gamepads from walking slowly.")]
    public bool snapInput = true;
    
    [Tooltip("Minimum input required before input is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
    public float deadZoneThreshold = 0.1f;

    [Header("MOVEMENT")]
    [Tooltip("The top horizontal movement speed")]
    public float maxSpeed = 14;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float acceleration = 80;

    [Tooltip("The pace at which the player comes to a stop")]
    public float groundDeceleration = 60;

    [Tooltip("speed of rotation to look towards direction of the movement")]
    public float rotationSpeed = 90f;
}
