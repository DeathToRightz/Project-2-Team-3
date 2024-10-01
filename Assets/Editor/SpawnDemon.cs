using System;
using UnityEditor;
using UnityEngine;

public class SpawnDemon : EditorWindow
{

    string objectName = string.Empty;
    GameObject prefabRef;
    bool shouldPatrol;
    bool shouldTargetPlayer;
    bool spawnDemonAtCoordinates;
    Vector3 spawnLocation;
    
    [MenuItem("Tools/Demon Spawner")]

    public static void ShowWindow()
    {
        GetWindow(typeof(SpawnDemon));
      
    }

    
    
    private void OnGUI()
    {
        GUILayout.Label("Spawn New Demon", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Name of Demon", objectName);
        prefabRef = EditorGUILayout.ObjectField("Prefab of Demon", prefabRef, typeof(GameObject),false) as GameObject;
        shouldPatrol = EditorGUILayout.Toggle("Should Demon Patrol?", shouldPatrol);
        shouldTargetPlayer = EditorGUILayout.Toggle("Should Target Player?", shouldTargetPlayer);

        EditorGUIUtility.labelWidth = 280;
        spawnDemonAtCoordinates = EditorGUILayout.Toggle("Should Demon Spawn at specified coordinates", spawnDemonAtCoordinates);

        if(spawnDemonAtCoordinates )
        {
           spawnLocation = EditorGUILayout.Vector3Field("Input coordinates to spawn demon",spawnLocation);
        }
        
       


        if(GUILayout.Button("Spawn Demon"))
        {
            CreateDemon(objectName,prefabRef,shouldPatrol,shouldTargetPlayer,spawnDemonAtCoordinates);
   
        }

       
    }

    private void CreateDemon(string nameOfDemon, GameObject incomingPrefab, bool shouldPatrol,bool shouldTargetPlayer,bool spawnAtCoordinates)
    {
        GameObject newDemon = null;
      

        if(spawnAtCoordinates)
        {
           
            newDemon = Instantiate(incomingPrefab,spawnLocation, Quaternion.identity);
        }
        else
        {
            newDemon = Instantiate(incomingPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
        newDemon.name = nameOfDemon;
        if(newDemon.GetComponent<DemonPatrolling>() != null)
        {
            if (shouldTargetPlayer)
            {
                newDemon.GetComponent<DemonPatrolling>().aggroTowardPlayer = true;
            }
            if (shouldPatrol)
            {
                newDemon.GetComponent<DemonPatrolling>().allowedToPatrol = true;
            }
        }
        else
        {
            Debug.LogWarning("Spawned Demon doesn't have the DemonPatrolling script attached to it");
        }
    }
}
