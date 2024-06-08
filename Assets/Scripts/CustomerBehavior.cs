using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    [SerializeField] private float criminalChance;
    [SerializeField] private bool criminal = false;
    [SerializeField] private bool carry = false;
    [SerializeField] float speed = 20;
    [SerializeField] float distance = 0; //use????

    public enum custState
    {
        Idle,
        Moving,
        Picking,
        Dead,
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PathfindTEST() 
    { 
        
    
    }
    void CustomerAI() 
    { 
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
