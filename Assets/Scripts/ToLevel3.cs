using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel3 : MonoBehaviour
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
            StartCoroutine(OnCollisionWithPlayer());
            Debug.LogWarning("Remember change to Level 3 scene");
        }
    }
    
    private IEnumerator OnCollisionWithPlayer()
    {
        _fadeTransition.FadeIn(1);
        yield return new WaitForSeconds(1);
        _fadeTransition.LoadScene(SceneManager.GetSceneByName("Level2").buildIndex);
        _fadeTransition.FadeOut(1);
    }
}
