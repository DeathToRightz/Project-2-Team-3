using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(PlayerMovement))]
public class Player_PushPullBox : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private Collider _collider;
    private FixedJoint _joint;
    private bool _isAttached;
    private Ray _ray;
    public bool IsAttached => _isAttached;
    private Animator _animator;

    private bool isGrabbing;

    private float _anyInputRef;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerMovement>().PlayerInput;
        _playerInput.PlayerActionMap.Grab.started += _ => JointAttachDetach();
        
       
        
    }
    private void Update()
    {
        if (isGrabbing)
        {           
            _animator.SetFloat("MoveDirection", CheckMovementDirection());
        }
         
    }
    private void JointAttachDetach()
    {
        if (_joint == null) return;
        if (!GameObjectRaycastCheck()) return;
        
        if (!_isAttached)
        {
            Debug.Log("Attach");
            _animator.SetBool("isGrabbing", true);
            isGrabbing = true;
            _isAttached = true;
            _joint.gameObject.GetComponent<SphereCollider>().enabled = false;
            _joint.connectedBody = _rb;           
            FreezePositionOnAttachment();
        }
        else
        {
            Debug.Log("Detach");
           _animator.SetBool("isGrabbing", false);
            isGrabbing=false;
            _isAttached = false;
            _joint.gameObject.GetComponent<SphereCollider>().enabled = true;
            _joint.connectedBody = null;
            _joint = null;
            _rb.constraints = ~RigidbodyConstraints.FreezePosition;
        }
    }

    /// <summary>
    /// Checks if player is looking at pushable object
    /// </summary>
    /// <returns></returns>
    private bool GameObjectRaycastCheck()
    {
        _ray = new Ray(transform.position, transform.forward);
        return Physics.Raycast(_ray, _collider.bounds.extents.z + .2f, LayerMask.GetMask("Pushable"), QueryTriggerInteraction.Ignore);
    }
        
    private void FreezePositionOnAttachment() 
    {
        _rb.angularVelocity = Vector3.zero;

        if (transform.eulerAngles.y is > 45 and < 135 or > 225 and < 315)
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionX;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pushable"))
        {
            _joint = other.GetComponent<FixedJoint>();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pushable"))
        {
            _joint = null;
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(_collider == null) return;
        Debug.DrawRay(_ray.origin, _ray.direction * (_collider.bounds.extents.z + .2f), Color.red);
    }
        
#endif


    private float CheckMovementDirection()
    {

        Vector3 playerVelocity = _rb.velocity.normalized;

        Vector3 position = transform.position;
        Vector3 objectPosition = _joint.transform.position;

        Vector3 direction = objectPosition - position;

        float animationDirection = Vector3.Dot(direction.normalized, playerVelocity);

      return  Remap(animationDirection, -1, 1, 0, 1);
    }


    public float Remap(float v, float minOld, float maxOld, float minNew, float maxNew)
    {
        return minNew + (v - minOld) * (maxNew - minNew) / (maxOld - minOld);
    }
}
