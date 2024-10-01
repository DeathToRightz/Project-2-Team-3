using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DemonFOV : MonoBehaviour
{
    [SerializeField] float angle = 100;
    [SerializeField] float radius = 10;

    [SerializeField] LayerMask obstructionLayers, targetLayer;

    public GameObject playerRef;

    public bool canSeePlayer;

    [SerializeField] public Vector3 directionToTarget;

    private float distanceToTarget;

    // Start is called before the first frame update
    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player") != null? playerRef = GameObject.FindGameObjectWithTag("Player"): null;
    }

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if(canSeePlayer)
        {
            VisualizeFOV(distanceToTarget, directionToTarget);
        }
       
    }
    private IEnumerator FOVRoutine()
    {
      while (true)
        {
            yield return new WaitForSeconds(0.2f);
            
            FieldOFViewCheck();
        }
    }

    private void FieldOFViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetLayer);

        if (rangeChecks.Length != 0)
        {
            Transform targetTransform = rangeChecks[0].transform;
          
            directionToTarget = (targetTransform.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                 distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                if (!Physics.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionLayers))
                {
                    canSeePlayer = true;
                   
                   
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }

        
    }


    private void VisualizeFOV(float distance,Vector3 direction)
    {
        Color color = Color.blue;
        Debug.DrawRay(transform.position,direction * distance, color);
    }
}
