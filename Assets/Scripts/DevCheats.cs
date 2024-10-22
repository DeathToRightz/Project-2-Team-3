using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevCheats : MonoBehaviour
{
    private static DevCheats _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)) ToNextScene();
        if(Input.GetKeyDown(KeyCode.P)) ToPreviousScene();
    }

    private void ToNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= 5) nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    private void ToPreviousScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        
        if(nextSceneIndex < 0) return;
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}
