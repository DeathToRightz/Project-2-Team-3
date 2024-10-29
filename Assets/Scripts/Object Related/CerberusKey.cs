using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CerberusKey : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    
    [SerializeField, Tooltip("Door gameObject that this key opens. Door opens when key is collected.")] 
    private GameObject _doorToOpen;
    [SerializeField, Tooltip("Camera to activate during animation")] 
    private GameObject _cinematicCamera;
    [SerializeField] private Animator _cerberusAnimator;
    
    [Header("Runner Variables")]
    [SerializeField] private PlayerRunnerMovement _playerRunner;
    [SerializeField] private GameObject _runnerCamera;
    [SerializeField] private GameObject _runnerCerberus;
    [SerializeField] private PlayerLanesController _lanesController;
    
    private Animation _doorAnimation;
    private PlayerMovement _thirdPersonPlayer;
    
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
           
            _thirdPersonPlayer = other.GetComponent<PlayerMovement>();
            _thirdPersonPlayer.PlayerInput.Disable();
            StartCoroutine(StartAnimation(1));
        }
    }

    private IEnumerator StartAnimation(float delay)
    {
        _cerberusAnimator.Play("CerberusNoticePlayerCutsceen");
        yield return new WaitForSeconds(2.5f);
        _cinematicCamera.SetActive(true);
        yield return new WaitForSeconds(delay);
        _doorAnimation.Play();
        StartCoroutine(AnimationEnd(_doorAnimation.clip.length));
    }
    
    private IEnumerator AnimationEnd(float delay)
    {
       
        yield return new WaitForSeconds(delay);
        _thirdPersonPlayer.gameObject.SetActive(false);
        _playerRunner.gameObject.SetActive(true);
        _lanesController.gameObject.SetActive(true);
        _runnerCerberus.SetActive(true);
        _runnerCamera.SetActive(true);
        _cinematicCamera.SetActive(false);
        Destroy(gameObject);
    }
}
