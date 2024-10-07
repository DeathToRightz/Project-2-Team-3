using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _initialAmountToPool;

    // Start is called before the first frame update
    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _initialAmountToPool; i++)
        {
            AddObjectToPool();
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject obj = (GameObject)Instantiate(_objectToPool, this.transform, true);
        obj.SetActive(false);
        _pooledObjects.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in _pooledObjects)
        {
            // if the pooled objects is NOT active, return that object 
            if (!obj.activeInHierarchy) return obj;
        }

        // otherwise, add new object 
        return AddObjectToPool();
    }
}
