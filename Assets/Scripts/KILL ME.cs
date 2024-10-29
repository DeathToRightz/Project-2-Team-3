using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KILLME : MonoBehaviour
{
    private void Start()
    {
        KillGameObject();
    }
    public void KillGameObject()
    {
        Destroy(gameObject,3);
    }
}
