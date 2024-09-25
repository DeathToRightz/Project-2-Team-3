using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DemonPatrolling : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    private NavMeshAgent agent;

    [SerializeField]
    List<Transform> patrolLocation = new List<Transform>();

    [SerializeField]
    //  bool startPatrolling;
    int patrolIndex = 0;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        if(agent.pathPending)
        {
            Debug.LogWarning($"{gameObject.name}, path is pending ");
        }
        if (patrolLocation.Count >= 1)
        {
            Debug.LogWarning($"{gameObject.name}, needs at least 2 patrol points");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0)) 
        {
           Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //agent.SetDestination(hit.point); //Smooth move to click point
                
                Debug.Log(hit.point);
            }
        }*/ ///When person click on screen and hits something where the NavMeshAgent can go it will go there

        StartPatrolling();    
    }


    void StartPatrolling()
    {
       
        if(!agent.pathPending && patrolLocation.Count >= 1)
        {
            while(agent.remainingDistance <.05f)
            {
                agent.SetDestination(patrolLocation[patrolIndex].transform.position);
            }
            patrolIndex = (patrolIndex + 1) % patrolLocation.Count;
        }
    }
    
}
