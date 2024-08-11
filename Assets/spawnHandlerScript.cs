using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class spawnHandlerScript : MonoBehaviour
{
    [SerializeField] private GameObject Target1point1;

    private GameObject Target1;
    //this is here only for testing purposes.
    //make sure in the next verison this is in the CUSTOMER BEHAVIOR script instead
    
    public Vector2 targetSpawnPosition;
    //this is also here for testing purposes.
    //also should be handled by CUSTOMER BEHAVIOR instead

    // Start is called before the first frame update
    void Start()
    {
        spawningTarget();
    }

    void spawningTarget() 
    {      
        Instantiate(Target1point1, targetSpawnPosition, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
