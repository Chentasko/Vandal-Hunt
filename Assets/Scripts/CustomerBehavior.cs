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
    [SerializeField] bool ActivateAI;

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
    bool imleaving = false;

    int waitMin = 3;
    int waitMax = 7;
    int waitTime;
    float currentTime;

    NavMeshAgent agent;
    public State customerState;
    public AnimationState customerAnimationState;
    public string Targets;
    float waitMultiplier = 1; //will be 0 at initialisation
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

    void DeltaWaitingCalculations()
    {
        Debug.Log("I will now start waiting!");
        waitTime = UnityEngine.Random.Range(waitMin, waitMax);
        if (currentTime <= waitTime) 
        {
            while (currentTime <= waitTime)
            {
                currentTime = (currentTime + Time.deltaTime) * waitMultiplier; //.2 //.4 //.6 //.8                                                                     //Debug.Log(currentTime);
            }
        }
        currentTime = 0;
        Debug.Log("I've waited exactly " + waitTime);        
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
    IEnumerator MainAI() //will most likely not be a courotine. switch to a main void function if necessary
    {
        diceRollTriple = UnityEngine.Random.Range(1, 4);
        //diceRollTriple = 1; //comment this out 
        diceRollTriple = 2; //commet this out too

        if (diceRollTriple == 1 && ActivateAI) //Idle state. Move somewhere freely (or maybe just stand still and wait? decide on that)
        {
            customerState = State.Idle;
            //set target following           
            //customerAnimationState = AnimationState.Standby; //Uncomment this if Idle will mean stand still
            customerAnimationState = AnimationState.Walking; //Uncomment this if Idle will mean moving randomly somewhere (IT MOVES DESTINATION)
            while (!init)
            {
                agent.SetDestination(target.position);
            }
            Debug.Log("First behaviour executed");

        }
        else if (diceRollTriple == 2 && ActivateAI) //carry checking code. if they carry anything and if criminal, will either escape or back to the loop (seek flowchart for more info)
        {
            diceRollDouble = UnityEngine.Random.Range(1, 3);
            diceRollDouble = 1; //comment this out
            if (carry && diceRollDouble == 1) //diceRollDouble equaling to 1 means the customer will either try to escape or checkout
            { 
                //The innocent will go to checkout here. The criminal will leave. yeah
                if (criminalWillBe) 
                {
                    criminal = true; //CAN SHOOT NOW
                    //set.target = to the exit?? something like that
                    customerState = State.Moving; //it is moving but to the exit with the items they stole.
                    
                }
                else if (!criminalWillBe) //not criminal
                {
                    while (!init)
                    {
                        agent.SetDestination(target.position); //going to checkout? dont forget to set it up. just like them all
                    }

                    customerState = State.Picking;
                    customerAnimationState = AnimationState.HandsMoving;
                    //checking out is the usual animation. code that too
                    //lastly it will leave the store. BEING INNOCENT.
                }
                imleaving = true;
            } 
            else 
            { 
                //Finish executing this behaviour. Go back to RWI (the waiting at the top)
            }

            Debug.Log("Second behaviour executed");

        } 
        else if (diceRollTriple == 3 && ActivateAI) //Pick up items!! 
        {
            customerState = State.Moving;
            while (!init && customerState == State.Moving) 
            {
                agent.SetDestination(target.position); //MOVING TO GET ITEMS
            }

            customerState = State.Picking;
            customerAnimationState = AnimationState.HandsMoving;
            
            //PICKUP ANIMATIONS, PICKUP SOUNDS. this already happens after the player has moved to the target shelves. Might execute these independently since AnimationState now exists
            //(after the while is breaks of course duh).
            
            isOnWaitPenalty = true;
            //Debug.Log("RANDOM WAITING " + isOnWaitPenalty);
            //waitTime = UnityEngine.Random.Range(waitMin, waitMax);
            //yield return new WaitForSeconds(waitTime);
            isOnWaitPenalty = false;
            carry = true;
            

            customerState = State.Idle;
            customerAnimationState = AnimationState.Standby;
            Debug.Log("Third behaviour executed"); 
        }
        DeltaWaitingCalculations();
        yield return null;
    }
    void MoveToTarget() 
    {
        while (!init)
        {
            agent.SetDestination(target.position);
            customerAnimationState = AnimationState.Walking;
        }
        customerAnimationState = AnimationState.Standby;
    }
    void MainAI2() 
    { 
        if (criminalWillBe) 
        {
            Debug.Log("I am a criminal!");
            //set target store
            MoveToTarget(); //coming to store
            DeltaWaitingCalculations();
            diceRollDouble = UnityEngine.Random.Range(1, 3);
            if (diceRollDouble == 1) //if true, i will pick up items
            {
                Debug.Log("I'm going to pickup items!");
                //set target shelf
                MoveToTarget(); //moving to a shelf
                customerAnimationState = AnimationState.HandsMoving;
                DeltaWaitingCalculations(); 
                carry = true;
                Debug.Log("TeeheeHEEE I picked up items!");
            }
            else if (diceRollDouble == 2) //if true, i am carrying and will attempt to steal OR will just wait/move randomly (probably not the latter it's 3:30 am)
            {
                if (carry == true) //if true, i am carrying and will attempt to steal
                {
                    //set target exit
                    Debug.Log("I AM RUNNING AWAY CATCH ME IF YOU CAN");
                    MoveToTarget();

                } else if (carry == false) //if true, i am just going to wait/move randomly (probably not the latter it's 3:30 am)
                {
                    Debug.Log("i will move a bit more...");
                    customerAnimationState = AnimationState.Standby;
                    //set target random around
                    MoveToTarget();
                    DeltaWaitingCalculations();
                }
            }
        }
    
    }

    void PathfindTEST()
    {
        //agent.SetDestination(target.position);
    }
    void TargetSpawningTEST()
    {


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
            criminalWillBe = true; //will be innocent but DONT FORGET TO MAKE THIS FALSE
        } else if (criminalChanceUsing <= 50) 
        {
            criminalWillBe = true; //will steal
        }

        currentTime = Time.deltaTime;
        waitTime = 7;
    }

   

    // Update is called once per frame
    void Update()
    {
        MainAI2();
        //PathfindTEST();
        //Animate();
        //StartCoroutine(MainAI()); no. just no.
        //StartCoroutine(RandomWaitInterval()); //DO NOT FORGET TO COMMENT THIS OUT (Doesn't crash. It's not the yield implementetion. It's the MainAI coroutine causing the problem itself.)
    }
}
