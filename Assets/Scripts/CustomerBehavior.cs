using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;

public class CustomerBehavior : MonoBehaviour
{
    [SerializeField] private float criminalChance;
    [SerializeField] private bool criminal = false;
    [SerializeField] private bool carry = false;
    [SerializeField] float speed = 20;
    [SerializeField] float distance = 0; //use????

    [SerializeField] Transform target;
    SpriteRenderer skin;
    Vector2 lastPosition;

    NavMeshAgent agent;

    public void Animate() 
    {
        Vector2 currentPosition = transform.position;
        Vector2 movement = currentPosition - lastPosition;
        float totalMovement = movement.y + movement.x;
        if(movement.x < -0.1f) 
        { 
            skin.flipX = true; //going left
        } 
        else if (movement.x > 0.1f)
        {
            skin.flipX = false; //going right
        } 
        else 
        {
            return;
        }
        // DISCARDED MOVEMENT RECOGNITION. WAS SUPPOSED TO HANDLE WALKING ANIMATIONS. IT DOESN'T WORK. 
        //Debug.Log(totalMovement);

        //if(movement.x < -0.01f | movement.x > 0.01f | movement.y < -0.01f | movement.y > 0.01f) 
        //{
        //    Debug.Log("WALK WALK WALK WALK WALK");
        
        //} else 
        //{
            Debug.Log("STOP STOP STOP STOP STOP");
        //}

        lastPosition = currentPosition;  
    }
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
        agent = GetComponent<NavMeshAgent>();
        skin = GetComponent<SpriteRenderer>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        lastPosition = transform.position;
    }

    void PathfindTEST() 
    {
        agent.SetDestination(target.position);

    }

    void TargetSpawningTEST() 
    { 
        
    
    }
    void CustomerAI() 
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        PathfindTEST();
        Animate();
    }
}
