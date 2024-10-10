using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDoor : MonoBehaviour
{
 
    [SerializeField] List<bool> neededPlatesUp = new List<bool>(); //List that gets filled by Pressure Plat Manager
    
       
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
    }

    private void Update()
    {
        StartDoorAnimation(CountHowManyPlatesDown(neededPlatesUp)); //Calls the function below to update the values of everything
    }
    private void StartDoorAnimation(int incomingPlateCount) //Gets the number of plates down for animation of door
    {        
        animator.SetInteger("PlatesDown",incomingPlateCount); //Sets the animation named PlatesDown that takes a int from the paramater
    }


    public void GetIncomingBoolList(List<bool> incomingList) //Function that gets bool list from event
                                                            // Clears every time is called to update list
    {
        neededPlatesUp.Clear();       
       neededPlatesUp.AddRange(incomingList);
    }

    private int CountHowManyPlatesDown(List<bool> incomingList) //Counts how many plates are down
    {
        int platesDown = 0;
        for(int i = 0; i <= incomingList.Count-1; i++)
        {
            if (incomingList[i])
            {
                platesDown++;
            }
        }
        return platesDown;
    }
}
