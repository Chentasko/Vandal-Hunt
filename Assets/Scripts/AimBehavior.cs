using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AimBehavior : MonoBehaviour
{
    Vector2 aimPos; //niþan
    Vector2 mousPos; //fare 
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().position = aimPos;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Debug.Log(aimPos);
        mousPos = Input.mousePosition;
        aimPos = Camera.main.ScreenToWorldPoint(mousPos);
        transform.position = aimPos;



        //Transform.Position.x = aimPos.x; 
        //Transform.Position.x = aimPos.y;
    }
}
