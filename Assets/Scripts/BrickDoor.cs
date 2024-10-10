using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDoor : MonoBehaviour
{

    [SerializeField] bool plateAStatus,plateBStatus;
    [SerializeField] int neededPlatesDown;
    [SerializeField] List<bool> neededPlatesUp = new List<bool>();
    public int platesDown;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
    }

    private void Update()
    {
        StartDoorAnimation(CountHowManyPlatesDown(neededPlatesUp));
    }
   /* public void CheckPlateA(bool incomingBool)
    {
       
        plateAStatus = incomingBool;
        
       
    }
    public void CheckPlateB(bool incomingBool)
    {
        plateBStatus = incomingBool;
        
    }*/

 

    private void StartDoorAnimation(int incomingPlateCount)
    {
        

        animator.SetInteger("PlatesDown",incomingPlateCount);
    }


    public void GetIncomingBoolList(List<bool> incomingList)
    {
        neededPlatesUp.Clear();       
       neededPlatesUp.AddRange(incomingList);
    }

    private int CountHowManyPlatesDown(List<bool> incomingList)
    {
         platesDown = 0;
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
