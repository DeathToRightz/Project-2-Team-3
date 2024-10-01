using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Vector3 _offsetToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offsetToPlayer;
        transform.LookAt(_player.transform);
    }
}
