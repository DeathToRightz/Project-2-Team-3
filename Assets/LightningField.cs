using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningField : MonoBehaviour
{
    [SerializeField] GameObject lightningVFX;
    private float _meshSizeX,_meshSizeY,_meshSizeZ;
    // Update is called once per frame
    private void Awake()
    {
        _meshSizeX = GetComponent<MeshFilter>().mesh.bounds.extents.x * gameObject.transform.localScale.x;
       // _meshSizeX = ((GetComponent<MeshFilter>().mesh.bounds.extents.x * gameObject.transform.localScale.x) + gameObject.transform.position.x) ;
        _meshSizeY = ((GetComponent<MeshFilter>().mesh.bounds.extents.y * gameObject.transform.localScale.y) + gameObject.transform.position.y) ;
        _meshSizeZ = ((GetComponent<MeshFilter>().mesh.bounds.extents.z * gameObject.transform.localScale.z) + gameObject.transform.position.z) ;
      
    }
    void Start  ()
    {
        Debug.Log(transform.position.x);
        Debug.Log(transform.localScale.x);
        StartCoroutine(LightningFieldFunc(lightningVFX));       
    }

    IEnumerator LightningFieldFunc(GameObject objVFX)
    {
        while (true)
        {
            Instantiate(objVFX,new Vector3(Random.Range(transform.position.x,_meshSizeX),transform.position.y,Random.Range(-_meshSizeZ,_meshSizeZ + 1)),Quaternion.identity);
           // Instantiate(objVFX,new Vector3(Random.Range(-_meshSizeX,_meshSizeX + 1), Random.Range(-_meshSizeY, _meshSizeY + 1), Random.Range(-_meshSizeZ, _meshSizeZ + 1)),Quaternion.identity);
            yield return new WaitForSeconds(4f);
        }
    }
}
