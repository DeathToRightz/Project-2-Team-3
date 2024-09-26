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
            spawnLocation = EditorGUILayout.Vector3Field("Input coordinates", spawnLocation);
        }
       


        if(GUILayout.Button("Spawn Demon"))
        {

            Vector3 spawn = new Vector3(0,0,0);
            
            GameObject newObject = Instantiate(prefabRef,spawn,Quaternion.identity);
            newObject.name = objectName;
           
            if(newObject.GetComponent<DemonTest>() != null && shouldTargetPlayer)
            {
                newObject.GetComponent<DemonTest>().chasePlayer = true;
            }
            
            
           
            
        }
    }

   
}
