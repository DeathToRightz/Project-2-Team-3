using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private Transform _mainCam;
    [SerializeField] private bool _showDebug;
    private Rigidbody _rb;
    private Player_PushPullBox _playerPushScript;

    //Inputs
    private PlayerInput _playerInput;
    private float _horizontalInput;
    private float _verticalInput;

    //Movement
    LayerMask _groundLayerMask;
    private float _currentVelocity;
    private Vector3 _moveDirection;
    private bool _grounded;

    public PlayerInput PlayerInput => _playerInput;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundLayerMask = LayerMask.GetMask("Ground");
        _playerPushScript = GetComponent<Player_PushPullBox>();
        
        _playerInput = new PlayerInput();
        _playerInput.PlayerActionMap.Enable();
    }

    private void OnDisable()
    {
        _playerInput.PlayerActionMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = _playerInput.PlayerActionMap.Movement.ReadValue<Vector2>().x;
        _verticalInput = _playerInput.PlayerActionMap.Movement.ReadValue<Vector2>().y;
    }

    private void FixedUpdate()
    {
        GroundCollisionCheck();
        HandleHorizontal();
        ApplyMovement();
    }

    private void HandleHorizontal()
    {
        if (_horizontalInput == 0 && _verticalInput == 0)
        {
            _currentVelocity = Mathf.MoveTowards(_currentVelocity, 0, _stats.groundDeceleration * Time.fixedDeltaTime);
            _moveDirection = _moveDirection.normalized * _currentVelocity;
            return;
        }

        //Camera Reference, makes it so that player moves in relation to the camera. DONT place camera directly on top of player.
        Vector3 forward = _mainCam.forward;
        Vector3 right = _mainCam.right;
        forward.y = 0f;
        right.y = 0f;
        forward = forward.normalized;
        right = right.normalized;

        _currentVelocity = Mathf.MoveTowards(_currentVelocity, _playerPushScript.IsAttached? _stats.maxSpeedWithPushable : _stats.maxSpeed, _stats.acceleration * Time.fixedDeltaTime);
        _moveDirection = (forward * _verticalInput + right * _horizontalInput) * _currentVelocity; 
    }

    void ApplyMovement()
    {
        _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.z);

        if (_moveDirection == Vector3.zero || _playerPushScript.IsAttached) return;
        
        //rotate player towards direction of movement
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _stats.rotationSpeed * Time.deltaTime);
    }

    void GroundCollisionCheck()
    {
        float distance = 1.1f;
        _grounded = Physics.Raycast(transform.position, Vector3.down, distance, _groundLayerMask);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_stats == null)
        {
            Debug.LogWarning("Please assign a ScriptableStats asset to PlayerController's Stats slot", this);
        }
    }

    /// <summary>
    /// Draws box for debug purposes
    /// </summary>
    private void OnGUI()
    {
        if(!_showDebug) return;
        
        int spacing = 3;
        float width = 200;
        float height = 100;
        GUI.Box(new Rect(spacing, spacing, width, height), "");

        Rect label1Rect = new Rect(3 + spacing, 0 + spacing, width, 25);
        string isGroundedLabel = $"is Grounded? {_grounded}";
        
        Rect label2Rect = new Rect(3 + spacing, 25 + spacing, width, 25);
        string velocityLabel = $"Velocity: {_moveDirection}";

        GUI.Label(label1Rect, isGroundedLabel);
        GUI.Label(label2Rect, velocityLabel);
    }
#endif
}