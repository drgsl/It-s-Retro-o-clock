using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{

    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];


    // if a vector is (1.0001, 2) it will round to (1,2)
    // we need this function because rotations may cause the coordinates to not be round anymore.
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    //check if a certain coordinate is in between the borders or if it's outside of the borders
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 2);
    }

    //delete all blocks in a certain row
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //whenever a row was deleted, the above rows should fall towards the bottom one unit
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x,y] != null)
            {
                //move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                //update block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //decrease all rows above a deleted one
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
        {
            decreaseRow(i);
        }
    }

    //finds out if a row is full of blocks
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    //deletes all full rows and always decreases the above row's y
    //coordinate by one.
    public static void deleteFullRows()
    {
        int deletedRows = 0;
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                deletedRows++;
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
        calculatePoints(deletedRows);
    }

    public static void calculatePoints(int deletedRows)
    {
        int points = 0;
        switch (deletedRows)
        {
            case 0:
                break;

            case 1:
                points = 40;
                break;

            case 2:
                points = 100;
                break;

            case 3:
                points = 300;
                break;

            case 4:
                points = 1200;
                break;
            case 5:
                points = 5000;
                break;

            default: // 6,7,8,9,10,11,12
                points = 20000;
                break;
        }

        UpdateScoreTetris.UpdateScore(points);

        //return points;
    }
}
