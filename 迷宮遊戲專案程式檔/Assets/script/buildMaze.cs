using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildMaze : MonoBehaviour
{
    public GameObject wall;
    public Transform walls;
    public int mazeRowSize;
    public int mazeColSize;
    public float height;
    public float space;
    
    maze myMaze;


    void Awake()
    {
        height = wall.transform.localPosition.y;
        space = wall.transform.localScale.y;
        myMaze = new maze(mazeRowSize, mazeColSize, height, space);

        
        for (int i = 0; i < myMaze.index; i++)
        {
            GameObject g = Instantiate(wall);
            g.transform.SetParent(walls);
            g.transform.localPosition = myMaze.points[i];
        }
     

    }


    // Update is called once per frame
    void Update()
    {

    }
}
