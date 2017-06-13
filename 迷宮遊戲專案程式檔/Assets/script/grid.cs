using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid {

    public edge up;
    public edge right;
    public edge down;
    public edge left;
    public Vector3 middlePoint;
    public bool isVisited;  

    public grid(Vector3 p1,Vector3 p2,Vector3 p3,Vector3 p4,float height)
    {
        up = new edge(p1, p2);
        right = new edge(p2, p3);
        down = new edge(p3, p4);
        left = new edge(p4, p1);
        middlePoint = new Vector3((p1.x + p2.x) / 2, height , (p2.z + p3.z) / 2);
        isVisited = false;
    }     
}
