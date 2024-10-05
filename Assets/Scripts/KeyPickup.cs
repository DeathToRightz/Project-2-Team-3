using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField, Tooltip("Door gameObject that this key opens. Door opens when key is collected.")]
    private GameObject _doorToOpen;
    private Animation _doorAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        _doorAnimation = _doorToOpen.GetComponent<Animation>();
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
            _doorAnimation.Play();
            Destroy(gameObject);
        }
    }
}
