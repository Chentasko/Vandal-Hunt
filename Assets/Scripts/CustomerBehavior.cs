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
    //booleans
    [SerializeField] bool ActivateAI; 
    [SerializeField] private bool criminalWillBe;
    [SerializeField] private bool criminal = false;
    [SerializeField] private bool carry = false;
    bool touching = false;
    bool touch = false;
    bool init = false;
    bool justStarted = true;

    //floats and integers
    [SerializeField] private float criminalChance;
    float waitMultiplier = 1; //will be 0 at initialisation
    float currentTime;
    public int diceRollDouble;
    public int diceRollTriple;
    int waitMin = 1;
    int waitMax = 4;
    int waitTime;


    [SerializeField] Transform target;

    [SerializeField] GameObject text;

    SpriteRenderer skin;

    Vector2 lastPosition;
   
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
        //Debug.Log("Touched target!");

        if (init!) 
        {
            //Debug.Log("Touched target!");
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
            //Debug.Log("With the target!");
        } else 
        {
            touching = false;
        }
        customerState = State.Idle;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("It is gone!");
        if (touch) 
        {
            //Debug.Log("It is gone!");
            touch = false;
            customerState = State.Moving;
        } else 
        {
            touch = true;
        }
    }
    IEnumerator DeltaWaitingCalculationsCoroutine()
    {
        waitTime = UnityEngine.Random.Range(waitMin, waitMax);
        Debug.Log("I will now start waiting for " + waitTime + " seconds!");

        currentTime = 0;

        while (currentTime <= waitTime)
        {
            currentTime += Time.deltaTime * waitMultiplier;
            yield return null; // Wait for the next frame
        }

        Debug.Log("I've waited exactly " + waitTime + " seconds");
    }
    IEnumerator MoveToTarget2(Transform target)
    {
        customerAnimationState = AnimationState.Walking;

        while (Vector2.Distance(transform.position, target.position) > 0.2f)
        {
            agent.SetDestination(target.position);
            yield return null; // Wait for the next frame
        }

        customerAnimationState = AnimationState.Standby;
        Debug.Log("MOVE DONE");
    }

    IEnumerator MainAI2()
    {
        if (justStarted) 
        {
            target.SendMessage("TargetCenter"); // set target store
            yield return StartCoroutine(MoveToTarget2(target)); //MoveToTarget(); // coming to store
        }

        if (criminalWillBe)
        {
            Debug.Log("I am a criminal!");
            yield return StartCoroutine(DeltaWaitingCalculationsCoroutine());
            diceRollDouble = UnityEngine.Random.Range(1, 3);
            if (diceRollDouble == 1) // if true, I will pick up items
            {
                Debug.Log("I'm going to pick up items!");
                target.SendMessage("TargetRandomShelf"); // set target shelf
                yield return StartCoroutine(MoveToTarget2(target)); // moving to a shelf
                customerAnimationState = AnimationState.HandsMoving;
                yield return StartCoroutine(DeltaWaitingCalculationsCoroutine()); //keeping the animation going for a random time
                carry = true;
                Debug.Log("TeeheeHEEE I picked up items!");
            }
            else if (diceRollDouble == 2) // if true, I am carrying and will attempt to steal OR will just wait/move randomly
            {
                if (carry) // if true, I am carrying and will attempt to steal
                {
                    criminal = true;
                    Debug.Log("I AM RUNNING AWAY CATCH ME IF YOU CAN");
                    target.SendMessage("TargetExit");
                    yield return StartCoroutine(MoveToTarget2(target)); 
                    text.SendMessage("Lost");
                    Destroy(gameObject);

                }
                else // if true, I am just going to wait/move randomly
                {
                    Debug.Log("I will move a bit more... don't have any items");
                    customerAnimationState = AnimationState.Standby;
                    target.SendMessage("TargetCenter"); // set target random around
                    yield return StartCoroutine(MoveToTarget2(target));
                    yield return StartCoroutine(DeltaWaitingCalculationsCoroutine());
                }
            }
        }
        else 
        {
            Debug.Log("I am innocent");
            yield return StartCoroutine(DeltaWaitingCalculationsCoroutine());
            diceRollDouble = UnityEngine.Random.Range(1, 3);
            if (diceRollDouble == 1) // if true, I will pick up items
            {
                Debug.Log("I'm going to pick up items!");
                target.SendMessage("TargetRandomShelf"); // set target shelf
                yield return StartCoroutine(MoveToTarget2(target)); // moving to a shelf
                customerAnimationState = AnimationState.HandsMoving;
                yield return StartCoroutine(DeltaWaitingCalculationsCoroutine()); //keeping the animation going for a random time
                carry = true;
                Debug.Log("TeeheeHEEE I picked up items!");
            }
            else if (diceRollDouble == 2) // if true, I am carrying and will attempt to pay check OR will just wait/move randomly
            {
                if (carry) // if true, I am carrying and will self checkout, then leave
                {
                    // set target exit
                    Debug.Log("I will pay now");
                    target.SendMessage("TargetRandomCheckout");
                    yield return StartCoroutine(MoveToTarget2(target));
                    yield return StartCoroutine(DeltaWaitingCalculationsCoroutine());
                    target.SendMessage("TargetExit");
                    yield return StartCoroutine(MoveToTarget2(target));
                    text.SendMessage("WonPassive");
                    Destroy(gameObject);

                }
                else // if true, I am just going to wait randomly
                {
                    Debug.Log("Innocent will move a bit more... don't have any items");
                    yield return StartCoroutine(DeltaWaitingCalculationsCoroutine());
                    customerAnimationState = AnimationState.Standby;
                }
            }

        }
        justStarted = false;
    }
    IEnumerator MainAI2Loop() //this loops the coroutine without putting a stop to the update method. so much headache just becauase i didn't do this
    {
        while (true)
        {
            yield return StartCoroutine(MainAI2());
        }
    }

    public void OnDeath() 
    { 
        if (criminal) 
        {
            Debug.Log("!! !! !!YOU WIN!! !! !!");
            text.SendMessage("Won");
            Destroy(gameObject);

        } else 
        {
            Debug.Log("!! !! LOSER LOL !! !!");
            text.SendMessage("Lost");
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.Find("Target");
        GameObject text = GameObject.Find("Text");
        agent = GetComponent<NavMeshAgent>();
        skin = GetComponent<SpriteRenderer>();     
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lastPosition = transform.position;
        

        customerState = State.Moving;
        customerAnimationState = AnimationState.Standby;
        float criminalChanceUsing = UnityEngine.Random.Range(1, 101); //determines if the customer will steal or not at start
        if (criminalChanceUsing > 50) 
        {
            criminalWillBe = false; //will be innocent (pay)
        } else if (criminalChanceUsing <= 50) 
        {
        criminalWillBe = true; //will be criminal (steal)
        }
        currentTime = Time.deltaTime;
        waitTime = 2;
        StartCoroutine(MainAI2Loop());
    }

    // Update is called once per frame
    void Update()
    {
        Animate();   
    }
}
