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
    IEnumerator ShootingPenalty()
    {
        isFreeToShoot = false;
        Debug.Log("I am WAITING");
        yield return new WaitForSeconds(10f);
        Debug.Log("I am NOW DONE WAITING");
        
    }

    void ShootLogic() //Doðru çalýþmýyor burayý yeniden yap. Void ve ifler yerine hepsini bir IEnumerator içinde kullanmayý dene (
    {
        if (Input.GetMouseButtonDown(0) && isFreeToShoot) 
        {

            Debug.Log("SHOOT SHOOT BAM BAM");
            shootTrigger = true;
            //isFreeToShoot = false;
            //ses kodu gelecek buraya
            

        } else if (!isFreeToShoot)
        {
            shootTrigger = false;
            Debug.Log("i cant shoot right now PENALTY");
        
        }
        else 
        {
            
        }   
    }

    void CursorLogic() 
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
        ShootLogic();
        


        //Transform.Position.x = aimPos.x; 
        //Transform.Position.x = aimPos.y;
    }
}
