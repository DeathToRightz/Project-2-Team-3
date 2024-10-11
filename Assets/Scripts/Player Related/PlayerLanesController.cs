using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanesController : MonoBehaviour
{
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindFirstObjectByType<PlayerRunnerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);
    }
}