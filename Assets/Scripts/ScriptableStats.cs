using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableStats : ScriptableObject
{
    [Header("MOVEMENT")]
    [Tooltip("The top horizontal movement speed")]
    public float maxSpeed = 14;
    
    [Tooltip("The top horizontal movement speed while attached with a pushable object")]
    public float maxSpeedWithPushable = 8;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float acceleration = 80;

    [Tooltip("The pace at which the player comes to a stop")]
    public float groundDeceleration = 60;

    [Tooltip("speed of rotation to look towards direction of the movement")]
    public float rotationSpeed = 90f;
}
