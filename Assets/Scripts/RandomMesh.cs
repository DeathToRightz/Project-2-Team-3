using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMesh : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshes; 
    
    // Start is called before the first frame update
    void Awake()
    {
        GameObject newMesh = _meshes[Random.Range(0, _meshes.Length)];
        GetComponent<MeshFilter>().sharedMesh = newMesh.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<Renderer>().sharedMaterials = newMesh.GetComponent<MeshRenderer>().sharedMaterials;
        //Instantiate(newMesh, transform.position, transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
