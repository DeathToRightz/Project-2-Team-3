using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PressurePlateManager : MonoBehaviour
{
    [SerializeField] List<GameObject> plates = new List<GameObject>(); //Plates that want to be affected

    private List<bool> plateDownChecker = new List<bool>(); //List of bool to keep the values of the plates being down

    [SerializeField] UnityEvent<List<bool>> checkPlateEvent = new UnityEvent<List<bool>>();  //Event that will take the bool above to send to the specific door

    private void Start()
    {  
        for (int i = 0; i <= plates.Count-1; i++)
        {
            plateDownChecker.Add(plates[i].GetComponent<PressurePlates>().plateIsDown);
           
            
        }
       checkPlateEvent.Invoke(plateDownChecker);
    }

    

    public void SendBoolCheckToDoor()
    {
        for (int i = 0; i <= plates.Count - 1; i++)
        {
            plateDownChecker[i] = plates[i].GetComponent<PressurePlates>().plateIsDown;


        }
        checkPlateEvent.Invoke(plateDownChecker);
    }
}
