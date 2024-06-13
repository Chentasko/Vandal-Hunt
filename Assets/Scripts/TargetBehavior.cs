using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        Collider2D checkoutA1Collider = checkoutA1.GetComponent<Collider2D>();
        Collider2D checkoutA2Collider = checkoutA2.GetComponent<Collider2D>();
        Collider2D checkoutA3Collider = checkoutA3.GetComponent<Collider2D>();

        Collider2D checkoutB1Collider = checkoutB1.GetComponent<Collider2D>();
        Collider2D checkoutB2Collider = checkoutB2.GetComponent<Collider2D>();
        Collider2D checkoutB3Collider = checkoutB3.GetComponent<Collider2D>();

        Bounds checkoutA1Bounds = checkoutA1Collider.bounds;
        Bounds checkoutA2Bounds = checkoutA2Collider.bounds;
        Bounds checkoutA3Bounds = checkoutA3Collider.bounds;

        Bounds checkoutB1Bounds = checkoutB1Collider.bounds;
        Bounds checkoutB2Bounds = checkoutB2Collider.bounds;
        Bounds checkoutB3Bounds = checkoutB3Collider.bounds;

        
        int checkoutSelector = Random.Range(1, 7);
        Vector2 newPosition = Vector2.zero;
        switch (checkoutSelector) 
        {
            case 1: //checkoutA1

                newPosition.x = checkoutA1Bounds.max.x;
                newPosition.y = Random.Range(checkoutA1Bounds.min.y, checkoutA1Bounds.max.y);
                break;

            case 2: //checkoutA2
                newPosition.x = checkoutA2Bounds.max.x;
                newPosition.y = Random.Range(checkoutA2Bounds.min.y, checkoutA2Bounds.max.y);
                break;
            case 3: //checkoutA3
                newPosition.x = checkoutA3Bounds.max.x;
                newPosition.y = Random.Range(checkoutA3Bounds.min.y, checkoutA3Bounds.max.y);
                break;
            case 4: //checkoutB1
                newPosition.x = checkoutB1Bounds.min.x;
                newPosition.y = Random.Range(checkoutB1Bounds.min.y, checkoutB1Bounds.max.y);
                break;
            case 5: //checkoutB2
                newPosition.x = checkoutB2Bounds.min.x;
                newPosition.y = Random.Range(checkoutB2Bounds.min.y, checkoutB2Bounds.max.y);
                break;
            case 6: //checkoutB3
                newPosition.x = checkoutB3Bounds.min.x;
                newPosition.y = Random.Range(checkoutB3Bounds.min.y, checkoutB3Bounds.max.y);
                break;
        }
        transform.position = newPosition;
    }
    public void TargetCenter()
    {
        //collider2D centerCollider = center.GetComponent<Collider2D>();
        Transform centerTransform = center.GetComponent<Transform>();

        transform.position = centerTransform.position;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
