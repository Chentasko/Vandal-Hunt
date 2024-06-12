using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class CustomerBehavior : MonoBehaviour
{
    bool isOnWaitPenalty = false;

    [SerializeField] private float criminalChance;
    [SerializeField] private bool criminalWillBe;
    [SerializeField] private bool criminal = false;
    [SerializeField] private bool carry = false;
    [SerializeField] float speed = 20;
    [SerializeField] float distance = 0; //use????

    public int diceRollDouble;
    public int diceRollTriple;

    [SerializeField] Transform target;
    SpriteRenderer skin;
    Vector2 lastPosition;
    bool touching = false;
    bool touch = false;
    bool left = true;
    bool init = false;

    float waitMin = 3.0f;
    float waitMax = 7.0f;
    float waitTime;

    NavMeshAgent agent;
    public State customerState;
    public AnimationState customerAnimationState;
    public string Targets;

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
        //Debug.Log("STOP STOP STOP STOP STOP");
        //}

        lastPosition = currentPosition;  
    }
    public enum State
    {
        Idle,
        Moving,
        Picking,
        Dead,
    }

    public enum AnimationState 
    { 
        Standby,
        Walking,
        HandsMoving,
        Death,   
    }
    void OnTriggerEnter2D(Collider2D other)
    {     
        Debug.Log("Touched target!");

        if (init!) 
        {
            Debug.Log("Touched target!");
            touch = true;
        } else 
        {
            touch = false;
        }
        //customerState = State.Idle; //If things don't work, Uncomment this and commentize the same code that is in TriggerStay2D

    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (touch)
        {
            init = true;
            Debug.Log("With the target!");
        } else 
        {
            touching = false;
        }
        customerState = State.Idle;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("It is gone!");
        if (touch) 
        {
            Debug.Log("It is gone!");
            touch = false;
            customerState = State.Moving;
        } else 
        {
            touch = true;
        }
    }

    IEnumerator RandomWaitInterval() 
    {
        //float waitTime = Random.Range(waitMin, waitMax);
        

        isOnWaitPenalty = true;
        //Debug.Log("RANDOM WAITING " + isOnWaitPenalty);
        waitTime = UnityEngine.Random.Range(waitMin, waitMax);
        yield return new WaitForSeconds(waitTime);
        isOnWaitPenalty = false;
        //Debug.Log("WAIT DONE " + isOnWaitPenalty);
    }
    IEnumerator MainAI()
    {
        
        waitTime = UnityEngine.Random.Range(waitMin, waitMax); //waiting before doing ANYTHING wheter coming from a state or just starting. Everything comes back to this (hopefully)
        yield return new WaitForSeconds(waitTime);
        carry = true;
        isOnWaitPenalty = false;
        diceRollTriple = UnityEngine.Random.Range(1, 4);
        

        //THIS SECTION HANDLES RANDOM BEHAVIOR SELECTION. DON'T FORGET TO MAKE SURE YOU HAVE IMPLEMENTED RANDOM WAIT INTERVALS.

        if (diceRollTriple == 1) //Idle state. Move somewhere freely (or maybe just stand still and wait? decide on that)
        {
            customerState = State.Idle;
            //customerAnimationState = AnimationState.Standby; Uncomment this if Idle will mean stand still
            //customerAnimationState = AnimationState.Walking; Uncomment this if Idle will mean moving randomly somewhere

        }
        else if (diceRollTriple == 2) //carry checking code. if they carry anything and if criminal, will either escape or back to the loop (seek flowchart for more info)
        {
            diceRollDouble = UnityEngine.Random.Range(1, 3);
            if (carry && diceRollDouble == 1) 
            { 
                //The innocent will go to checkout here. The criminal will leave.
                if (criminalWillBe) 
                {
                    criminal = true; //CAN SHOOT NOW
                    customerState = State.Moving; //it is moving but to the exit with the items they stole.
                    //set.target = to the exit?? something like that
                } 
                else if (!criminalWillBe) //not criminal
                {
                    while (!init)
                    {
                        agent.SetDestination(target.position); //going to checkout? dont forget to set it up. just like them all
                    }

                    //checking out is the usual animation. code that too
                    //lastly it will leave the store. BEING INNOCENT.
                }

                

            }

        } 
        else if (diceRollTriple == 3) //Pick up items!! 
        {
            customerState = State.Moving;
            while (!init && customerState == State.Moving) 
            {
                agent.SetDestination(target.position); //MOVING TO GET 
            }

            customerState = State.Picking;
            customerAnimationState = AnimationState.HandsMoving;
            
            //PICKUP ANIMATIONS, PICKUP SOUNDS. this already happens after the player has moved to the target shelves. Might execute these independently since AnimationState now exists
            //(after the while is breaks of course duh).
            
            isOnWaitPenalty = true;
            //Debug.Log("RANDOM WAITING " + isOnWaitPenalty);
            waitTime = UnityEngine.Random.Range(waitMin, waitMax);
            yield return new WaitForSeconds(waitTime);
            isOnWaitPenalty = false;
            carry = true;

            customerState = State.Idle;
            customerAnimationState = AnimationState.Standby;
        }

        //while (customerState == State.Moving && !isOnWaitPenalty) 
        //{
            //agent.SetDestination(target.position);
            //play walking animation or something
           
        //} 

        //if (init) 
        //{
            //customerState = State.Picking;


            //isOnWaitPenalty = true;
            //picking animation code
            //picking sound code 
            //Debug.Log("RANDOM WAITING " + isOnWaitPenalty);
            
            //customerState = State.Moving;
            //Debug.Log("WAIT DONE " + isOnWaitPenalty);

            
        //}


        //start first by moving to the aile
        //if (touch) 
        //{
            

        //}
        
        
        
        yield return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        skin = GetComponent<SpriteRenderer>();     
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lastPosition = transform.position;

        customerState = State.Moving;
        customerAnimationState = AnimationState.Standby;

        float criminalChanceUsing = UnityEngine.Random.Range(1, 101); //Determines if the customer will steal or not at start
        if (criminalChanceUsing > 50) 
        {
            criminalWillBe = false; //will be innocent    
        } else if (criminalChanceUsing <= 50) 
        {
            criminalWillBe = true; //will steal
        }
    }

    void PathfindTEST() 
    {
        //agent.SetDestination(target.position);

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
        StartCoroutine(MainAI());
        StartCoroutine(RandomWaitInterval()); //DO NOT FORGET TO COMMENT THIS OUT
    }
}
