using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerCollisions))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private Transform _mainCam;
    [SerializeField] private bool _showDebug;
    private Rigidbody _rb;
    private Player_PushPullBox _playerPushScript;
    private PlayerCollisions _collisions;

    //Inputs
    private PlayerInput _playerInput;
    private float _horizontalInput;
    private float _verticalInput;

    //Movement
    private float _currentVelocity;
    private Vector3 _moveDirection;
    private bool _grounded;
    private bool _isAffectedByWind;

    public PlayerInput PlayerInput => _playerInput;

    private Animator _animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerPushScript = GetComponent<Player_PushPullBox>();
        _collisions = GetComponent<PlayerCollisions>();
        _playerInput = new PlayerInput();
        _playerInput.PlayerActionMap.Enable();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Debug.Log(_animator);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(SoundManager.instance != null)
        {
            SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("War Drums"));
        }
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
        _grounded = _collisions.GroundCollisionCheck();
    }

    private void FixedUpdate()
    {
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

        
        _currentVelocity = Mathf.MoveTowards(_currentVelocity, _stats.maxSpeed, _stats.acceleration * Time.fixedDeltaTime);
        CheckMaxSpeedModifier();
        _moveDirection = (forward * _verticalInput + right * _horizontalInput) * _currentVelocity; 
    }

    void ApplyMovement()
    {
       
        _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.z);

        if (_moveDirection == Vector3.zero || _playerPushScript.IsAttached) { _animator.SetBool("isMoving", false); return; }
        _animator.SetBool("isMoving", true);
        

        //rotate player towards direction of movement
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _stats.rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    ///Modifies and clamps maximum speed based on conditions.
    ///</summary>
    private void CheckMaxSpeedModifier() 
    {
        if (_playerPushScript.IsAttached)
        {
            _currentVelocity = Mathf.Clamp(_currentVelocity, 0, _stats.maxSpeedWithPushable);

          
        }
        else if (_isAffectedByWind)
        {
            _currentVelocity = Mathf.Clamp(_currentVelocity, 0, _stats.maxSpeedOnWindArea);
        }
    }
    
    #region Wind Functions

    /// <summary>
    /// Use to start applying wind force.
    /// </summary>
    /// <param name="windForce">direction.</param>
    public void ApplyWindForce(Vector3 windForce)
    {
        if (!_isAffectedByWind) _isAffectedByWind = true;
        _rb.AddForce(windForce, ForceMode.Force);
    }

    /// <summary>
    /// Use to stop applying wind force. Use if you are using ApplyWindForce().
    /// </summary>
    public void StopWindForce()
    {
        _isAffectedByWind = false;
    }

    #endregion

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
        string velocityLabel = $"Velocity: {_rb.velocity}";

        GUI.Label(label1Rect, isGroundedLabel);
        GUI.Label(label2Rect, velocityLabel);
    }
#endif
}