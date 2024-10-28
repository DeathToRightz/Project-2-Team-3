using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PressurePlateManager : MonoBehaviour
{
    [SerializeField] List<GameObject> plates = new List<GameObject>(); //Plates that want to be affected

    private List<bool> plateDownChecker = new List<bool>(); //List of bool to keep track if plates are down

    //Event that will take the bool above to send to the specific door

    [SerializeField,Tooltip("Attach this to desired door with the CheckIfAllPlatesAreDown, make sure it's DYNAMIC")] UnityEvent<List<bool>> checkPlateEvent = new UnityEvent<List<bool>>();
    private void Start()
    {  
        for (int i = 0; i <= plates.Count-1; i++) // Start of game will add the desired plates from above to the list on the assigned door
        {
            plateDownChecker.Add(plates[i].GetComponent<PressurePlates>().plateIsDown);
           
            
        }
       checkPlateEvent.Invoke(plateDownChecker);
    }

    

    public void SendBoolCheckToDoor() //Gets the plateIsDown member variable from List of GameObjects
                                      //above to count how many plates needed to keep track of for door
    {
        for (int i = 0; i <= plates.Count - 1; i++)
        {
            plateDownChecker[i] = plates[i].GetComponent<PressurePlates>().plateIsDown;
        }
        checkPlateEvent.Invoke(plateDownChecker);
    }


    
}
