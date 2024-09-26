using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonTest : MonoBehaviour
{

    public bool chasePlayer;

    private void Awake()
    {
       
    }

    private void Start()
    {
        Debug.Log(chasePlayer + gameObject.name);
        
    }
}
