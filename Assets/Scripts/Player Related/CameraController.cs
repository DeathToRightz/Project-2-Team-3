using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private Transform _player;
    private Transform _targetPos;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _targetPos = FindFirstObjectByType<CameraTargetPos>().gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPos.transform.position, Time.deltaTime * 10);
        transform.LookAt(_player.transform);
    }
}
