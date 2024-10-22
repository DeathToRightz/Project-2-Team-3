using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuMusic : MonoBehaviour
{
    private MainmenuMusic instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {

            instance = this;
            


        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "New Level1")
        {
            Destroy(this.gameObject);
        }
    }
}
