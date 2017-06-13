using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edge  {

      public Vector3[] points = new Vector3[2];
        
      public edge(Vector3 p1,Vector3 p2)
    {
        this.points[0] = p1;
        this.points[1] = p2;
    }

    public void setEdge(Vector3 p1, Vector3 p2)
    {
        this.points[0] = p1;
        this.points[1] = p2;
    }

    public void through()
    {
        points[0] = points[1] = new Vector3(0, 0);
    }

    public Vector3 getPoint1()
    {
        return points[0];
    }

    public Vector3 getPoint2()
    {
        return points[1];
    }

    public bool edgeIsExisted()
    {
        return !(points[0].x == points[1].x && points[0].z== points[1].z);
    }
}
