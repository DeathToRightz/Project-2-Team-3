using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetPos : MonoBehaviour
{
    private PlayerMovement _player;
    private PlayerInput _playerInput;
    private float _mouseInput;
    [SerializeField] private Vector3 _offset;
    [SerializeField, Range(0,1)] private float _mouseSensitivity = .3f;
   
    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>();
        _playerInput = _player.PlayerInput;
        transform.position = _player.transform.position + _offset;
    }
    
    private void Update()
    {
        _mouseInput = _playerInput.PlayerActionMap.MouseX.ReadValue<float>();
        
        _offset = Quaternion.AngleAxis (_mouseInput * _mouseSensitivity, Vector3.up) * _offset;
        transform.position = _player.transform.position + _offset;
    }
}
