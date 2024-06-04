using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AimBehavior : MonoBehaviour
{
    Vector2 aimPos; //niþan
    Vector2 mousPos; //fare 
    bool isFreeToShoot = true;
    bool shootTrigger = false;
    float loadTime = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().position = aimPos;
        GetComponent<Collider2D>().isTrigger = shootTrigger;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }    
    
    IEnumerator ShootLogic() //main shooting coroutine
    {
        if (Input.GetMouseButtonDown(0) && isFreeToShoot) 
        {
            shootTrigger = true;
            isFreeToShoot = false;
            Debug.Log("Shot!");
            //shooting sound code 
        
        } else if (shootTrigger)
        { 
            shootTrigger = false;
            Debug.Log("Waiting...");  
            yield return new WaitForSeconds(loadTime);
            isFreeToShoot = true;
            Debug.Log("Done! Next round?");
            //reloading sound code 
        }    
    }

    void CursorLogic() //handling cursor mouse positioning yeah
    {
        Cursor.visible = false;
        //Debug.Log(aimPos);
        mousPos = Input.mousePosition;
        aimPos = Camera.main.ScreenToWorldPoint(mousPos);
        transform.position = aimPos;
    }

    // Update is called once per frame
    void Update()
    {
        CursorLogic();
        StartCoroutine(ShootLogic());
        
        //Transform.Position.x = aimPos.x; 
        //Transform.Position.x = aimPos.y;
    }
}
