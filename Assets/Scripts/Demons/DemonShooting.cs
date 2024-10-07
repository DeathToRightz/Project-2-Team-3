using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class DemonShooting : MonoBehaviour
{
    private GameObject _player;
    private ObjectPooler _bulletPooler;
    
    // Start is called before the first frame update
    void Start()
    {
        _bulletPooler = GetComponent<ObjectPooler>(); 
        _player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) ShootBullet(_player);
    }

    private void ShootBullet(GameObject target)
    {
        GameObject newBullet = _bulletPooler.GetPooledObject();
        newBullet.transform.position = transform.position;
        newBullet.transform.LookAt(target.transform);
        newBullet.SetActive(true);
    }
}
