using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public GameObject center;
    public GameObject exit;

    public GameObject shelvesA;
    public GameObject shelvesB;
    public GameObject shelvesFridge;

    public GameObject checkoutA1;
    public GameObject checkoutA2;
    public GameObject checkoutA3;
    public GameObject checkoutB1;
    public GameObject checkoutB2;
    public GameObject checkoutB3;
    //private object shelvesABounds;

    public void TargetRandomShelf()
    {
        Collider2D shelvesACollider = shelvesA.GetComponent<Collider2D>();
        Collider2D shelvesBCollider = shelvesB.GetComponent<Collider2D>();
        Collider2D shelvesFridgeCollider = shelvesFridge.GetComponent<Collider2D>();

        Bounds shelvesABounds = shelvesACollider.bounds;
        Bounds shelvesBBounds = shelvesBCollider.bounds;
        Bounds shelvesFridgeBounds = shelvesFridgeCollider.bounds;

        int edge = Random.Range(0, 4); // 0: left, 1: right, 2: top, 3: bottom
        int shelvesSelector = Random.Range(1, 4);
        Vector2 newPosition = Vector2.zero;

        if (shelvesSelector == 1) //shelvesA
        {
            switch (edge)
            {
                case 0: // left edge
                    newPosition.x = shelvesABounds.min.x;
                    newPosition.y = Random.Range(shelvesABounds.min.y, shelvesABounds.max.y);
                    break;
                case 1: // right edge
                    newPosition.x = shelvesABounds.max.x;
                    newPosition.y = Random.Range(shelvesABounds.min.y, shelvesABounds.max.y);
                    break;
                case 2: // top edge
                    newPosition.x = Random.Range(shelvesABounds.min.x, shelvesABounds.max.x);
                    newPosition.y = shelvesABounds.max.y;
                    break;
                case 3: // bottom edge
                    newPosition.x = Random.Range(shelvesABounds.min.x, shelvesABounds.max.x);
                    newPosition.y = shelvesABounds.min.y;
                    break;
            }
        }
        else if (shelvesSelector == 2) //shelvesB 
        
        switch (edge)
        {
            case 0: // left edge
                newPosition.x = shelvesBBounds.min.x;
                newPosition.y = Random.Range(shelvesBBounds.min.y, shelvesBBounds.max.y);
                break;
            case 1: // right edge
                newPosition.x = shelvesBBounds.max.x;
                newPosition.y = Random.Range(shelvesBBounds.min.y, shelvesBBounds.max.y);
                break;
            case 2: // top edge
                newPosition.x = Random.Range(shelvesBBounds.min.x, shelvesBBounds.max.x);
                newPosition.y = shelvesBBounds.max.y;
                break;
            case 3: // bottom edge
                newPosition.x = Random.Range(shelvesBBounds.min.x, shelvesBBounds.max.x);
                newPosition.y = shelvesBBounds.min.y;
                break;
            }
        else if (shelvesSelector == 3) //shelvesFridge
        {
            // bottom edge
            newPosition.x = Random.Range(shelvesFridgeBounds.min.x, shelvesFridgeBounds.max.x);
            newPosition.y = shelvesFridgeBounds.min.y;
                
        }
        
        transform.position = newPosition;
    }
    void TargetRandomCheckout() 
    { 
    
    }
    public void TargetCenter()
    {
        //Collider2D centerCollider = center.GetComponent<Collider2D>();
        Transform centerTransfrom = center.GetComponent<Transform>();

        transform.position = centerTransfrom.position;

        //Vector2 newPosition = Vector2.zero;

    }

    public void TargetExit()
    {
        Transform exitTransfrom = exit.GetComponent<Transform>();

        transform.position = exitTransfrom.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Collider2D centerCollider = center.GetComponent<Collider2D>();
        Collider2D exitCollider = exit.GetComponent<Collider2D>();

        //Collider2D shelvesACollider = shelvesA.GetComponent<Collider2D>();
        //Collider2D shelvesBCollider = shelvesB.GetComponent<Collider2D>();
        //Collider2D shelvesFridgeCollider = shelvesFridge.GetComponent<Collider2D>();

        Collider2D checkoutA1collider = checkoutA1.GetComponent<Collider2D>();
        Collider2D checkoutA2collider = checkoutA2.GetComponent<Collider2D>();
        Collider2D checkoutA3collider = checkoutA3.GetComponent<Collider2D>();
        Collider2D checkoutB1collider = checkoutB1.GetComponent<Collider2D>();
        Collider2D checkoutB2collider = checkoutB2.GetComponent<Collider2D>();
        Collider2D checkoutB3collider = checkoutB3.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
