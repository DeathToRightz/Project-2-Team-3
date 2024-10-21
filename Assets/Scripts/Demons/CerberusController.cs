using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (SoundManager.instance)
        {
            SoundManager.instance.PlaySound(transform.position, SoundManager.instance.FindSoundInfoByName("Cerburus"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector3(0, 0, _speed);
    }
}
