using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tonextlevel : MonoBehaviour
{
    private FadeTransition _fadeTransition;
    
    // Start is called before the first frame update
    void Start()
    {
        _fadeTransition = FindFirstObjectByType<FadeTransition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _fadeTransition.LoadSceneWithFade(1, "Level2");
        }
    }
}
