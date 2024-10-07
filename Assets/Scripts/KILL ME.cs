using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KILLME : MonoBehaviour
{
    public void KillGameObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
