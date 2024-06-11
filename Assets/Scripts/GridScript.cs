using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int width;
    int height;
    int[,] gridArray;

    public Grid(int width, int height) 
    { 
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];

        Debug.Log(width + " " + height);
    }
}
