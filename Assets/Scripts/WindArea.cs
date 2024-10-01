using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    private PlayerMovement _player;
    [SerializeField, Range(-1,1)] private float _forwardDirection = 1;
    [SerializeField, Range(-1,1)] private float _sideDirection;
    [SerializeField] private float _windForce = 500;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != _player.gameObject) return;
        _player.ApplyWindForce(new Vector3(_forwardDirection,0, _sideDirection) * _windForce);
    }

    private void OnTriggerExit(Collider other)
    {
        _player.StopWindForce();
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, new Vector3(_forwardDirection,0, _sideDirection) * _windForce, Color.green);
    }

#endif
}
