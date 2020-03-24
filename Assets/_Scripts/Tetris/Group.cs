using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{

    float lastFall = 0;

    public bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            //Not inside border?
            if (!Playfield.insideBorder(v))
                return false;

            //Block in grid cell(and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }


    public void updateGrid()
    {
        //remove old children from grid
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        //add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void mixColors()
    {
        Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = c;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        mixColors();

        if (!isValidGridPos())
        {
            //Debug.Log("Game Over cause by: " + gameObject.transform.name);
            
            Destroy(gameObject);
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            //modify position
            transform.position += new Vector3(-1, 0, 0);

            //see if it's valid
            if (isValidGridPos())
            {
                //it's valid. Update Grid.
                updateGrid();
            }
            else
            {
                //its not valid. revert.
                transform.position += new Vector3(1, 0, 0);
            }
        }


        //Move Right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            //modify position
            transform.position += new Vector3(1, 0, 0);

            //see if it's valid
            if (isValidGridPos())
            {
                //it's valid. Update Grid.
                updateGrid();
            }
            else
            {
                //its not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
            }
        }


        //Rotate
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //Rotate
            transform.Rotate(0, 0, -90);

            //see if it's valid
            if (isValidGridPos())
            {
                //it's valid. Update Grid.
                updateGrid();
            }
            else
            {
                //its not valid. revert.
                transform.Rotate(0, 0, 90);
            }
        }

        //Move Downwards and Fall
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Time.time - lastFall >=1)
        {
            //Rotate
            transform.position += new Vector3(0, -1, 0);

            //see if it's valid
            if (isValidGridPos())
            {
                //it's valid. Update Grid.
                updateGrid();
            }
            else
            {
                //its not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                //Clear filled horizontal lines
                Playfield.deleteFullRows();

                //spawn next group
                FindObjectOfType<Spawner>().SpawnNext();

                //disable script
                enabled = false;

                //change Tag
                //gameObject.tag = "Untagged";
            }
            lastFall = Time.time;
        }
    }
}
