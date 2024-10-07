using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBubbleBullet : MonoBehaviour
{
    [SerializeField] private int _lifetime, _speed;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Deactivate(_lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }

    private IEnumerator Deactivate(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
