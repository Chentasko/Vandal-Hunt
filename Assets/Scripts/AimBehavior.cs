using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AimBehavior : MonoBehaviour
{
    //Vector2 aimPos;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector2 aimPos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
        //Transform.Position.x = aimPos.x; 
        //Transform.Position.x = aimPos.y;
    }
}
