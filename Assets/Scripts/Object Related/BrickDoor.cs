using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDoor : MonoBehaviour
{

    private Animator animator;

    [SerializeField] int plateCount; //Counts the amount of plates that are down
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
    }
  
    public void  CheckIfAllPlatesAreDown(List<bool> incomingList) //Gets the bool list from the event in the
                                                                  //manager, uses the int member variable to compare if
                                                                  //all plates are down

    {
        plateCount = 0;
        for(int i = 0;i <= incomingList.Count-1;i++)
        {
            if (incomingList[i])
            {               
                plateCount++;
            }
            
        }
        if(plateCount == incomingList.Count)
        {
            animator.SetBool("All Plates Down", true);
            return;
        }
        animator.SetBool("All Plates Down", false);
       

    }
}
