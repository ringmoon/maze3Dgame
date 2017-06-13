using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze {

    public int row_count;
    public int col_count;
    public float space;
    public float height;
    public grid[] grids;
    public Vector3[] points;
    public int index;

    public maze(int row,int col,float height,float space)
    {   
        row_count = row;
        col_count = col;
        this.space = space;
        this.height = height;
        grids = new grid[row * col];
        points = new Vector3[(2*row+1)*(2*col+1)-row*col];
        index = 0;

        //初步建立row*col大小宮格
        creatGrids(row, col, height, 2*space);

        //執行迷宮建立演算法
        visitGrid(Random.Range(0,row_count*col_count), -1);

        //清除重複的邊
        clearRepeatEdge();

        //出口
        grids[0].up.through();

        //取得迷宮做標點給points陣列
        getTotalMazePoints();
    }

    //建立宮格方法
    void creatGrids(int row,int col,float height,float space)
    {

        Vector3 startA = new Vector3((col / 2) * (-space), height, (row / 2) * space);
        Vector3 startD = new Vector3((col / 2) * (-space), height, (row / 2 - 1) * space);
    
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                grids[i * col + j] = new grid(startA + j * new Vector3(space, 0, 0), startA + (j + 1) * new Vector3(space, 0, 0), startD + (j + 1) * new Vector3(space, 0, 0), startD + j * new Vector3(space, 0, 0), height);
            }
            startA = startD;
            startD += new Vector3(0, 0, -space);
        }
    }

    //迷宮建立演算法
    void visitGrid(int visitingGridIndex, int fromDirection)
    {
        //case: 0->up 1->right 2->down 3->left 
        // -1->first go in function
        grids[visitingGridIndex].isVisited = true;
        int willGoDirection;
        int nextVisitGridIndex;
        int[] willVisitDirections =new int[4];
        int visitDirections;

        //find direction of last visit gird
        if (fromDirection == -1)
        {
            willVisitDirections[3] = Random.Range(0,4);
            visitDirections = 4;
        }
        else
        {
            willVisitDirections[3] = fromDirection;
            visitDirections = 3;
        }

        //create will visit order to willVisitDirection array
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 4);
            willVisitDirections[i] = index;
            if (willVisitDirections[i] == willVisitDirections[3])
            {
                i--;
                continue;
            }
            for (int j = 0; j < i; j++)
            {
                if (willVisitDirections[i] == willVisitDirections[j])
                {
                    i--;
                    break;
                }
            }
        }

        for (int i = 0; i < visitDirections; i++)
        {
            switch (willVisitDirections[i])
            {
                case 0:
                    if (((visitingGridIndex - col_count) >= 0 && !grids[visitingGridIndex - col_count].isVisited))
                    {
                        nextVisitGridIndex = visitingGridIndex - col_count;
                        grids[visitingGridIndex].up.through();
                        grids[nextVisitGridIndex].down.through();
                        willGoDirection = 0;
                        visitGrid(nextVisitGridIndex, (willGoDirection + 2) % 4);
                    }
                    break;
                case 1:
                    if ((((visitingGridIndex + 1) % col_count) != 0 && (visitingGridIndex + 1) < row_count * col_count && !grids[visitingGridIndex + 1].isVisited))
                    {
                        nextVisitGridIndex = visitingGridIndex + 1;
                        grids[visitingGridIndex].right.through();
                        grids[nextVisitGridIndex].left.through();
                        willGoDirection = 1;
                        visitGrid(nextVisitGridIndex, (willGoDirection + 2) % 4);
                    }
                    break;
                case 2:
                    if (((visitingGridIndex + col_count) < row_count * col_count && !grids[visitingGridIndex + col_count].isVisited))
                    {
                        nextVisitGridIndex = visitingGridIndex + col_count;
                        grids[visitingGridIndex].down.through();
                        grids[nextVisitGridIndex].up.through();
                        willGoDirection = 2;
                        visitGrid(nextVisitGridIndex, (willGoDirection + 2) % 4);
                    }
                    break;
                case 3:
                    if ((((visitingGridIndex - 1) % col_count) != (col_count - 1) && (visitingGridIndex - 1) >= 0 && !grids[visitingGridIndex - 1].isVisited))
                    {
                        nextVisitGridIndex = visitingGridIndex - 1;
                        grids[visitingGridIndex].left.through();
                        grids[nextVisitGridIndex].right.through();
                        willGoDirection = 3;
                        visitGrid(nextVisitGridIndex, (willGoDirection + 2) % 4);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //清除重複邊方法
    void clearRepeatEdge()
    {
        for (int i = 0; i < row_count; i++)
        {
            for (int j = 0; j < col_count; j++)
            {
                if (i > 0 && i < row_count)
                {
                    grids[i * col_count + j].up.through();
                }
                if (j > 0)
                {
                    grids[i * col_count + j].left.through();
                }
            }
        }
    }

    //取得所有迷宮座標點方法
    void getTotalMazePoints()
    {
        for (int i = 0; i < row_count; i++)
        {
            for (int j = 0; j < col_count; j++)
            {
                if (i == 0 && j == 0)
                {
                    getFirstRowAndFirstCol();
                }else if (i == 0)
                {
                    getFirstRowAndAfterFirstCol(j);
                }else if (j == 0)
                {
                    getAfterFirstRowAndFirstCol(i);
                }else
                {
                    getAfterFirstRowAndAfterFirstCol(i, j);
                }
            }
        }
    }
    

    //取得第一列的第一行grid點座標    0 1  2
    void getFirstRowAndFirstCol()   //7 // 3 
    {                               //6 5  4
        points[index++] = new Vector3(grids[0].middlePoint.x - space, height, grids[0].middlePoint.z + space);
        if (grids[0].up.edgeIsExisted()) points[index++] = new Vector3(grids[0].middlePoint.x, height, grids[0].middlePoint.z + space);
        points[index++] = new Vector3(grids[0].middlePoint.x +space, height, grids[0].middlePoint.z + space);
        if (grids[0].right.edgeIsExisted()) points[index++] = new Vector3(grids[0].middlePoint.x+space, height, grids[0].middlePoint.z);
        points[index++] = new Vector3(grids[0].middlePoint.x + space, height, grids[0].middlePoint.z-space);
        if (grids[0].down.edgeIsExisted()) points[index++] = new Vector3(grids[0].middlePoint.x , height, grids[0].middlePoint.z-space);
        points[index++] = new Vector3(grids[0].middlePoint.x - space, height, grids[0].middlePoint.z - space);
        points[index++] = new Vector3(grids[0].middlePoint.x - space, height, grids[0].middlePoint.z);
    }

    //取得第一列的第一行之後grid點座標          // 0  1
    void getFirstRowAndAfterFirstCol(int col)   // // 2
    {                                           // 4  3
        points[index++] = new Vector3(grids[col].middlePoint.x, height, grids[col].middlePoint.z + space);
        points[index++] = new Vector3(grids[col].middlePoint.x+space, height, grids[col].middlePoint.z + space);
        if(grids[col].right.edgeIsExisted()) points[index++] = new Vector3(grids[col].middlePoint.x+space, height, grids[col].middlePoint.z);
        points[index++] = new Vector3(grids[col].middlePoint.x+space, height, grids[col].middlePoint.z - space);
        if (grids[col].down.edgeIsExisted()) points[index++] = new Vector3(grids[col].middlePoint.x, height, grids[col].middlePoint.z-space);
    }

    //取得第一列之後的第一行資料              // // //    
    void getAfterFirstRowAndFirstCol(int row)//4 //  0
    {                                        //3  2  1
        if (grids[row*col_count].right.edgeIsExisted()) points[index++] = new Vector3(grids[row * col_count].middlePoint.x + space, height, grids[row * col_count].middlePoint.z);
        points[index++] = new Vector3(grids[row * col_count].middlePoint.x + space, height, grids[row * col_count].middlePoint.z-space);
        if (grids[row * col_count].down.edgeIsExisted()) points[index++] = new Vector3(grids[row * col_count].middlePoint.x, height, grids[row * col_count].middlePoint.z-space);
        points[index++] = new Vector3(grids[row * col_count].middlePoint.x - space, height, grids[row * col_count].middlePoint.z - space);
        points[index++] = new Vector3(grids[row * col_count].middlePoint.x - space, height, grids[row * col_count].middlePoint.z);
    }

    //取得非第一列也非第二行資料                                         // // //
    void getAfterFirstRowAndAfterFirstCol(int row,int col)               // //  0
    {                                                                    //  2  1
        if(grids[row*col_count+col].right.edgeIsExisted()) points[index++] = new Vector3(grids[row*col_count+col].middlePoint.x + space, height, grids[row * col_count + col].middlePoint.z);
        points[index++] = new Vector3(grids[row * col_count + col].middlePoint.x + space, height, grids[row * col_count + col].middlePoint.z-space);
        if (grids[row * col_count + col].down.edgeIsExisted()) points[index++] = new Vector3(grids[row * col_count + col].middlePoint.x , height, grids[row * col_count + col].middlePoint.z-space);
    }

}
