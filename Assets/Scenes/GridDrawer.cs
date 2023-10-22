using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    [Header("Grid Changes")]
    public int grid_size = 4;//Grid size is this number ^ 2 so if it was four there would be 16 grid sections
    public int grid_sizeForDarkLines = 5;
    public int grid_numOfLinesInBetweenDark = 5;
    public Transform parentHolder;

    //public int grid_X_spaceBetweenLines = 5;// This would be X
    //public int grid_Z_spaceBetweenLines = 5;//If comparing it to a 2d grid this would be Y



    public LineRenderer grid_linerend_outside;
    public LineRenderer grid_linerend_inside_dark_downUp;
    public LineRenderer grid_linerend_inside_dark_leftRight;
    public LineRenderer grid_linerend_inside_upDown;
    public LineRenderer grid_linerend_inside_leftRight;






    public Queue<Vector3> pos;
    public Queue<Vector3> posTwo;
    // Start is called before the first frame update
    void Start()
    {

        grid_linerend_outside.useWorldSpace = false;
        grid_linerend_outside.transform.position += new Vector3(0, 1, 0);
        grid_linerend_inside_dark_downUp.useWorldSpace = false;
        grid_linerend_inside_dark_downUp.transform.position += new Vector3(0, 0.5f, 0);

        if (grid_size % 2 != 0)
        {
            Debug.LogWarning("You must have the grid_size be an even number (adding one to make it even)");
            grid_size++;
        }
        if(grid_size < 2)
        {
            Debug.LogWarning("grid_size must be 2 or larger");
            grid_size = 2;
        }

        grid_initialize_outside();
        grid_initialize_inside_downUp();
        grid_initialize_inside_leftRight();
        //spawnTouchCubes();





    }

    // Update is called once per frame
    void Update()
    {
        //for(int i =0;i<pos.Length;i++)
        //{
        //    lineren.SetPosition(i, pos[i]);
        //}
    }

    private void grid_initialize_outside()
    {
        //Generates the outer box of the grid and the two main lines for the grid

        //First we must change the size of the lineren so it can store all the new points
        //We choose six for four lines on the edge and two center ones

        //this.gameObject.transform.position = new Vector3(grid_size * grid_X_spaceBetweenLines * -1, 0, grid_size * grid_Z_spaceBetweenLines * -1);
        pos = new Queue<Vector3>();

        int gridSize2 = grid_size * grid_size;

        //Starts from bottom left
        Vector3 tempCurrentPoint = new Vector3(gridSize2, 0, gridSize2);
        Vector3 tempNextPoint = new Vector3(gridSize2, 0, gridSize2);

        //Make Bottom Left Line to Top Left
        tempNextPoint = new Vector3(tempNextPoint.x, 0, tempNextPoint.z * -1);
        pos.Enqueue(tempCurrentPoint);
        pos.Enqueue(tempNextPoint);


        //Make Top Left to Top Right
        tempCurrentPoint = tempNextPoint;
        tempNextPoint = new Vector3(tempNextPoint.x * -1, 0, tempNextPoint.z);
        pos.Enqueue(tempNextPoint);



        //Make Top Right to Bottom Right
        tempCurrentPoint = tempNextPoint;
        tempNextPoint = new Vector3(tempNextPoint.x, 0, tempNextPoint.z * -1);
        pos.Enqueue(tempNextPoint);


        //Make Bottom right to Bottom Left
        tempCurrentPoint = tempNextPoint;
        tempNextPoint = new Vector3(tempNextPoint.x * -1, 0, tempNextPoint.z);
        pos.Enqueue(tempNextPoint);


        //Need to add theese lines so lines don't cross
        pos.Enqueue(new Vector3(gridSize2, 0, gridSize2));






        //Generates the two center lines


        //Middle Top to Middle Bottom
        tempCurrentPoint = new Vector3(0, 0, gridSize2);
        tempNextPoint = new Vector3(0, 0, tempCurrentPoint.z * -1);
        pos.Enqueue(tempCurrentPoint);
        pos.Enqueue(tempNextPoint);

        //Middle Left to Middle Bottom
        tempCurrentPoint = new Vector3(gridSize2, 0, 0);
        tempNextPoint = new Vector3(tempCurrentPoint.x * -1, 0, 0);
        pos.Enqueue(new Vector3(0, 0, 0));
        pos.Enqueue(tempCurrentPoint);
        pos.Enqueue(new Vector3(0, 0, 0));

        pos.Enqueue(tempNextPoint);
        pos.Enqueue(new Vector3(gridSize2 * -1,0,gridSize2));









        //Draws the lines
        grid_linerend_outside.positionCount = pos.Count;
        int tempCounter = 0;

        foreach (Vector3 gridSection in pos)
        {
            grid_linerend_outside.SetPosition(tempCounter, gridSection);
            tempCounter++;
        }

    }
    private void grid_initialize_inside_downUp()
    {
        //initializes the queues
        pos = new Queue<Vector3>();
        posTwo = new Queue<Vector3>();
        //This method will start with the left side and then do the right side

        //Temps used for the lowest level of the grid
        int currentDiff = 0;
        int nextDiff = 0;

        //Loop for left side of the grid
        for(int i = 1;i * grid_sizeForDarkLines <= grid_size * grid_size; i++)
        {
            //Used to find the diff sections inbetween the dark lines
            nextDiff = (i * grid_sizeForDarkLines) - currentDiff;

            //Using the last value we can get a step which will allow us to get the correct line pos
            float stepDif = nextDiff / grid_numOfLinesInBetweenDark;


            //Loop to get the correct points
            for (int o = 1; o <= grid_numOfLinesInBetweenDark; o++)
            {
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff) * -1, 0, 0));
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff) * -1, 0, grid_size * grid_size));
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff) * -1, 0, grid_size * grid_size * -1));
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff) * -1, 0, 0));
            }


            //This is for the DARK lines to be shown
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines, 0, 0));
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines, 0, grid_size * grid_size));
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines, 0, grid_size * grid_size * -1));
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines, 0, 0));

            //Sets current diff = next diff for future use
            currentDiff = (i * grid_sizeForDarkLines);
        }

        //Sets it back to zero the be reused
        pos.Enqueue(new Vector3(0, 0, 0));
        posTwo.Enqueue(new Vector3(0, 0, 0));


        //UNDER ME IS NOW FOR THE RIGHT SIDE OF THE GRID

        //Rested current diff since we are working on the right side now

        currentDiff = 0;
        for (int i = 1; i * grid_sizeForDarkLines <= grid_size * grid_size; i++)
        {
            //Used to find the diff sections inbetween the dark lines
            nextDiff = (i * grid_sizeForDarkLines) - currentDiff;

            //Using the last value we can get a step which will allow us to get the correct line pos
            float stepDif = nextDiff / grid_numOfLinesInBetweenDark;


            //Loop to get the correct points
            for (int o = 1; o <= grid_numOfLinesInBetweenDark; o++)
            {
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff), 0, 0));
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff), 0, grid_size * grid_size));
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff), 0, grid_size * grid_size * -1));
                posTwo.Enqueue(new Vector3(((stepDif * o) + currentDiff), 0, 0));
            }


            //This is for the DARK lines to be shown
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines * -1, 0, 0));
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines * -1, 0, grid_size * grid_size));
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines * -1, 0, grid_size * grid_size * -1));
            pos.Enqueue(new Vector3(i * grid_sizeForDarkLines * -1, 0, 0));

            //Sets current diff = next diff for future use
            currentDiff = (i * grid_sizeForDarkLines);

        }


        grid_linerend_inside_dark_downUp.positionCount = pos.Count;
        int tempCounter = 0;
        foreach (Vector3 gridSection in pos)
        {
            grid_linerend_inside_dark_downUp.SetPosition(tempCounter, gridSection);
            tempCounter++;
        }

        grid_linerend_inside_upDown.positionCount = posTwo.Count;

        tempCounter = 0;
        foreach (Vector3 gridSection in posTwo)
        {
            grid_linerend_inside_upDown.SetPosition(tempCounter, gridSection);
            tempCounter++;
        }








    }
    private void grid_initialize_inside_leftRight()
    {
        GameObject tempRend = Instantiate(grid_linerend_inside_dark_downUp.gameObject);
        grid_linerend_inside_dark_leftRight = tempRend.GetComponent<LineRenderer>();

        tempRend.transform.parent = this.gameObject.transform;
        tempRend.GetComponent<LineRenderer>().useWorldSpace = false;
        tempRend.transform.Rotate(new Vector3(0, 90, 0));


        GameObject tempRend2 = Instantiate(grid_linerend_inside_upDown.gameObject);
        grid_linerend_inside_leftRight = tempRend2.GetComponent<LineRenderer>();

        tempRend2.transform.parent = this.gameObject.transform;
        tempRend2.GetComponent<LineRenderer>().useWorldSpace = false;
        tempRend2.transform.Rotate(new Vector3(0, 90, 0));

    }
    private void spawnTouchCubes()
    {
        Vector3 startPos = new Vector3(36, 0, 36);
        int diff = grid_sizeForDarkLines / grid_numOfLinesInBetweenDark;
        Debug.Log(diff);

       for(int i = (diff * -1);i<=diff;i++)
       {
            for(int o = (diff * -1); o <= diff; o++)
            {
                GameObject bob = GameObject.CreatePrimitive(PrimitiveType.Cube);
                bob.transform.position = new Vector3(i * diff, 5, o * diff);
                bob.transform.localScale *= 4;
                bob.transform.parent = parentHolder.transform;
                bob.layer = LayerMask.NameToLayer("NoRender");
            }

       }




    }

}
