using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    
    [SerializeField, Tooltip("Door gameObject that this key opens. Door opens when key is collected.")] 
    private GameObject _doorToOpen;
    [SerializeField, Tooltip("Camera to activate during animation")] 
    private GameObject _cinematicCamera;
    
    private Animation _doorAnimation;
    private PlayerMovement _player;
    [SerializeField] GameObject _demonBird;
    // Start is called before the first frame update
    void Start()
    {
        _doorAnimation = _doorToOpen.GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>()) //turn object invisible
            {
                if(meshRenderer == null) return;
                meshRenderer.enabled = false;
            }
            if (_demonBird) { _demonBird.GetComponent<DemonFOV>().enabled = false; }
            _player = other.GetComponent<PlayerMovement>();
            _player.PlayerInput.Disable();
            StartCoroutine(StartAnimation(1));
        }
    }

    private IEnumerator StartAnimation(float delay)
    {
        _cinematicCamera.SetActive(true);
        yield return new WaitForSeconds(delay);
        _doorAnimation.Play();
        StartCoroutine(AnimationEnd(_doorAnimation.clip.length));
    }
    
    private IEnumerator AnimationEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        _cinematicCamera.SetActive(false);
        _player.PlayerInput.Enable();
        Destroy(gameObject);
    }
}
