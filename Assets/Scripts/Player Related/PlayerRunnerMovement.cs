using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerRunnerMovement : MonoBehaviour
{
    [SerializeField,Tooltip("This determines how much to slow down the walking animation when hit")] float reducedAnimationSpeed = .5f;
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private float _targetXPosition;
    private float _forwardSpeed;
    private bool _canBeDamaged = true;
    private Animator _animator;
    [SerializeField] private float _initialSpeed, _sidewaysSpeed;
    [SerializeField] private Transform _laneCenter, _laneLeft, _laneRight;
    [SerializeField] private SkinnedMeshRenderer _mesh;

    private enum PlayerRunnerLanes
    {
        Left,
        Center,
        Right,
    }
    private PlayerRunnerLanes _currentRunnerLane;

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("isMoving", true);
        _rb = GetComponent<Rigidbody>();
        _playerInput = new PlayerInput();
        _playerInput.PlayerRunerActionMap.Enable();
        _playerInput.PlayerRunerActionMap.ChangeToLaneLeft.performed += _ => ChangeToLaneLeft();
        _playerInput.PlayerRunerActionMap.ChangeToLaneRight.performed += _ => ChangeToLaneLRight();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _currentRunnerLane = PlayerRunnerLanes.Center;
        SetPositionToLane(_currentRunnerLane);
        _forwardSpeed = _initialSpeed;

        if (SoundManager.instance)
        {
            SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Rain"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector3(0, 0, _forwardSpeed);
        Vector3 targetVector3 = new Vector3(_targetXPosition, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetVector3, Time.deltaTime * _sidewaysSpeed);
    }

    private void ChangeToLaneLeft()
    {
        if (_currentRunnerLane <= PlayerRunnerLanes.Left) return;
        _currentRunnerLane--;
        SetPositionToLane(_currentRunnerLane);
    }
    
    private void ChangeToLaneLRight()
    {
        if (_currentRunnerLane >= PlayerRunnerLanes.Right) return;
        _currentRunnerLane++;
        SetPositionToLane(_currentRunnerLane);
    }
    
    private void SetPositionToLane(PlayerRunnerLanes runnerLane)
    {
        switch (runnerLane)
        {
            case PlayerRunnerLanes.Center:
                _targetXPosition = _laneCenter.transform.position.x;
                break;
            case PlayerRunnerLanes.Left:
                _targetXPosition = _laneLeft.transform.position.x;
                break;
            case PlayerRunnerLanes.Right:
                _targetXPosition = _laneRight.transform.position.x;
                break;
            default:
                Debug.Log("Lane does not exist");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RunnerObstacle"))
        {
            if (!_canBeDamaged) return;
            StartCoroutine(ReduceSpeedForSeconds(2, 2));
        }
    }
    
    private IEnumerator ReduceSpeedForSeconds(float reductionAmount, float reductionTime)
    {
        _canBeDamaged = false;
        _forwardSpeed = Mathf.Clamp(_forwardSpeed - reductionAmount, 1, _forwardSpeed);
        _animator.speed = reducedAnimationSpeed;
        //do other feedback (animation, etc)
        StartCoroutine(FlashMesh(reductionTime, .1f));
        yield return new WaitForSeconds(reductionTime);
        _forwardSpeed = _initialSpeed;
        _animator.speed = 1;
        _canBeDamaged = true;
    }

    /// <summary>
    /// Turns off and on the characters mesh during the specified time.
    /// </summary>
    /// <param name="duration">length of flashing in seconds</param>
    /// <param name="interval">time in between each flash</param>
    /// <returns></returns>
    private IEnumerator FlashMesh(float duration, float interval)
    {
        float elapsedTime = 0;
        int index = 0;
        
        while (elapsedTime < duration)
        {
            _mesh.enabled = index % 2 == 0;
            elapsedTime += interval;
            index++;
            yield return new WaitForSeconds(interval);
        }

        _mesh.enabled = true;
    }
}
