using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class place_manager : MonoBehaviour
{
    public static place_manager _place_manager;
    [Header("Holders")]
    public Transform parentHolder;
    public List<GameObject> grid_placedThings;

    [Header("Placed Things")]
    public gridPlaceType currentPlaceType = gridPlaceType.hyperLane;
    public data_system currentPlaceSystem;
    public GameObject UI_currentButton; 
    public TextMeshProUGUI textForSelected;
    public System.Guid UI_fatherID = System.Guid.Empty;


    [Header("Default Placed")]
    public GameObject defaultCube;
    public GameObject defaultSphere;

    //Dumb 
    public List<Vector3> posOfCubes;


    //HyperLane vars
    [Header("Hyperlane")]
    public GameObject defaultHyperlane;


    public int currentSpawnedHyperLane;
    public GameObject firstHyperTemp;
    public bool canCheckForSecondHyperlane = false;
    public GameObject lastNodeTouched;


    [Header("Cool Dudes")]
    public coolDude supreamDude;

    

    private void Awake()
    {
        _place_manager = this;
        grid_placedThings = new List<GameObject>();
        posOfCubes = new List<Vector3>();

    }
    void Start()
    {
        supreamDude = new coolDude();
    }


    public void UI_buttonNodeOnClick(data_system systemData, GameObject currentButton, System.Guid id)
    {
        currentPlaceType = gridPlaceType.solarSystemInitializer;
        currentPlaceSystem = null;
        textForSelected.text = systemData.initializerName;
        UI_currentButton = currentButton;
        UI_fatherID = id;
    }

    public void UI_buttonNode_Hyperlane()
    {
        currentPlaceType = gridPlaceType.hyperLane;
        currentPlaceSystem = null;
        textForSelected.text = "Hyperlane";
        UI_currentButton = null;
    }
    public void UI_buttonNode_Wormhole()
    {
        currentPlaceType = gridPlaceType.wormHole;
        currentPlaceSystem = null;
        textForSelected.text = "Wormhole";
        UI_currentButton = null;

    }
    public void UI_buttonNode_EmpireSpawn()
    {
        currentPlaceType = gridPlaceType.empireSpawn;
        currentPlaceSystem = null;
        textForSelected.text = "Empire Spawn";
        UI_currentButton = null;

    }
    public void UI_buttonNode_Nebula()
    {
        currentPlaceType = gridPlaceType.nebula;
        currentPlaceSystem = null;
        textForSelected.text = "Nebula";
        UI_currentButton = null;

    }



    // Update is called once per frame
    void Update()
    {

        if (UI_mouseManager._UI_mouseManager.isInGridArea == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (currentPlaceType)
                {
                    case gridPlaceType.solarSystemInitializer:
                        #region Places solar system init on the grid

                        //Hit first time to find the closets section
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        // Casts the ray and get the first game object hit
                        Physics.Raycast(ray, out hit);
                        Vector3 hitPointOutPut = hit.point;
                        hitPointOutPut.y = 0;
                        hitPointOutPut = fourRound(hitPointOutPut);

                        Vector3 tempPos = hitPointOutPut + new Vector3(0, 5, 0);

                        foreach(Vector3 bob in posOfCubes)
                        {
                            if (bob == tempPos)
                                return;
                        }

                        if (hit.transform.gameObject.tag != "gridNode")
                        {
                            //User can change look in future but for now lets just defualt it to the sphere
                            //Spawning in the gameObject and setting it up
                            GameObject bob = Instantiate(defaultSphere);
                            bob.transform.parent = parentHolder;
                            bob.transform.position = hitPointOutPut;
                            bob.transform.position += new Vector3(0, 5, 0);

                            //Setting up its data
                            gridData TempgridData = bob.GetComponent<gridData>();
                            UI_nodeDataSystem tempDataSytem = UI_currentButton.GetComponent<UI_nodeDataSystem>();
                         //   TempgridData.setUpData(tempDataSytem.color, gridPlaceType.solarSystemInitializer, tempDataSytem.getSystemData());
                            grid_placedThings.Add(bob);
                            posOfCubes.Add(bob.transform.position);
                            TempgridData.UI_father = UI_fatherID;
                            System.Guid nodeID = supreamDude.addFella(bob);

                            TempgridData.ID = nodeID;
                            TempgridData.placeType = gridPlaceType.solarSystemInitializer;
                            TempgridData.systemData = tempDataSytem.Data_System;
                            TempgridData.setUpColor(tempDataSytem.color);
                            tempDataSytem.addKid(nodeID);
                            TempgridData.text_nameOfSystem.text = TempgridData.systemData.initializerName;

                            bob.GetComponent<SaveableEntity>().setID(nodeID);

                        }
                        #endregion
                        break;
                    case gridPlaceType.empireSpawn:
                        // code block
                        break;
                    case gridPlaceType.hyperLane:
                        #region hyperlane add
                        //If there is no hyperlane connect it
                        //Hit first time to find the closets section
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        // Casts the ray and get the first game object hit
                        Physics.Raycast(ray, out hit);

                        if (hit.transform.gameObject.tag == "gridNode" && currentSpawnedHyperLane == 0)
                        {
                            //Save for check
                            lastNodeTouched = hit.transform.gameObject;

                            //Line Render Stuff
                            firstHyperTemp = Instantiate(defaultHyperlane);
                            firstHyperTemp.transform.parent = parentHolder.transform;
                            LineRenderer hyperLine = firstHyperTemp.GetComponent<LineRenderer>();
                            currentSpawnedHyperLane = 1;
                            canCheckForSecondHyperlane = true;

                            //Set up hyperlane connections for first node
                            System.Guid ID = supreamDude.addFella(firstHyperTemp);
                            firstHyperTemp.GetComponent<gridHyperlane>().ID = ID;
                            firstHyperTemp.GetComponent<SaveableEntity>().setID(ID);

                            firstHyperTemp.GetComponent<gridHyperlane>().setFirstNode(supreamDude.getID(hit.transform.gameObject));

                            //Add it to holder
                            grid_placedThings.Add(firstHyperTemp);

                            //Moves hyperlane to look good
                            StartCoroutine(checkForSecondHyerplane(hit.transform.position.y, hyperLine));
                        }
                        if (hit.transform.gameObject.tag == "gridNode" && currentSpawnedHyperLane == 1 && hit.transform.gameObject != lastNodeTouched)
                        {
                            //Sets up all hyperlane connections and checks if it is a valid node host
                            if(firstHyperTemp.GetComponent<gridHyperlane>().setSecondNode(supreamDude.getID(hit.transform.gameObject)) == true)
                            {
                                //Reset to defaults
                                canCheckForSecondHyperlane = false;
                                currentSpawnedHyperLane = 0;
                                lastNodeTouched = null;
                                firstHyperTemp = null;
                            }       
                        }

                        #endregion
                        break;
                    case gridPlaceType.nebula:
                        // code block
                        break;
                    case gridPlaceType.wormHole:
                        // code block
                        break; 
                    default:
                        Debug.LogError("Type " + currentPlaceType + " isInvalid or Does not have a case");
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                #region Sets up ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                #endregion

                #region if on hyperLane and hyperlane count cound is 1 remove current hyperlane
                if (hit.transform.tag == "hyperlane")
                {
                    removeHyperlane(hit.transform.gameObject);
                    return;
                }
                #endregion

                #region removing gridNode from screen
                if (hit.transform.tag == "gridNode")
                {
                    gridData temp = hit.transform.gameObject.GetComponent<gridData>();
                    //gridNode.removeFella(hit.transform.gameObject);
                    posOfCubes.Remove(temp.gameObject.transform.position);
                    temp.removeGridData();
                    return;
                }
                #endregion

                //Should always go last
                #region Multi Select Box

                #endregion

            }
            if (Input.GetKey(KeyCode.Escape))
            {
                switch (currentPlaceType)
                {
                    case gridPlaceType.solarSystemInitializer:
                        break;
                    case gridPlaceType.empireSpawn:
                        // code block
                        break;
                    case gridPlaceType.hyperLane:
                        if(currentSpawnedHyperLane == 1)
                        {
                            removeHyperlane(firstHyperTemp);
                            canCheckForSecondHyperlane = false;
                            currentSpawnedHyperLane = 0;
                            lastNodeTouched = null;
                            firstHyperTemp = null;
                        }
                        break;
                    case gridPlaceType.nebula:
                        // code block
                        break;
                    case gridPlaceType.wormHole:
                        // code block
                        break;
                    default:
                        Debug.LogError("Type " + currentPlaceType + " isInvalid or Does not have a case");
                        break;
                }
            }
            
        }
    }

    private void removeHyperlane(GameObject hit)
    {
        if (hit.transform.tag == "hyperlane")
        {
            Debug.Log("balls");

            hit.transform.gameObject.GetComponent<gridHyperlane>().removeHyperlane();
            return;
        }
    }

    public void removeThingFromPlaceManager(GameObject thingToRemove, gridPlaceType type)
    {
        switch (type)
        {
            case gridPlaceType.solarSystemInitializer:
                supreamDude.removeFella(thingToRemove);
                break;
            case gridPlaceType.empireSpawn:
                // code block
                break;
            case gridPlaceType.hyperLane:
                supreamDude.removeFella(thingToRemove);
                break;
            case gridPlaceType.nebula:
                // code block
                break;
            case gridPlaceType.wormHole:
                // code block
                break;
            default:
                Debug.LogError("Type " + currentPlaceType + " isInvalid or Does not have a case");
                break;
        }
        grid_placedThings.Remove(thingToRemove);
    }

    IEnumerator checkForSecondHyerplane(float yPos, LineRenderer hyperlane)
    {
        Ray ray;
        RaycastHit hit;

        while (canCheckForSecondHyperlane == true)
        {
            if (hyperlane != null)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Casts the ray and get the first game object hit
                Physics.Raycast(ray, out hit);
                hyperlane.SetPosition(1, new Vector3(hit.point.x, yPos, hit.point.z));
                yield return new WaitForEndOfFrame();
            }
        }
    }


    public Vector3 fourRound(Vector3 hitPointOutPut)
    {
        if (Mathf.Abs(hitPointOutPut.x % 4) >= 2)
        {
            if (hitPointOutPut.x < 0)
            {
                //Negative
                hitPointOutPut.x = (((hitPointOutPut.x % 4) + 4) * -1) + hitPointOutPut.x;
            }
            else
            {
                hitPointOutPut.x = (((hitPointOutPut.x % 4) - 4) * -1) + hitPointOutPut.x;

            }
        }
        else
        {
            //Lower then two
            if (hitPointOutPut.x < 0)
            {
                //Negative
                hitPointOutPut.x = hitPointOutPut.x + (hitPointOutPut.x % 4) * -1;
            }
            else
            {
                hitPointOutPut.x = hitPointOutPut.x - (hitPointOutPut.x % 4);
            }
        }

        if (Mathf.Abs(hitPointOutPut.z % 4) >= 2)
        {
            if (hitPointOutPut.z < 0)
            {
                //Negative
                hitPointOutPut.z = (((hitPointOutPut.z % 4) + 4) * -1) + hitPointOutPut.z;
            }
            else
            {
                hitPointOutPut.z = (((hitPointOutPut.z % 4) - 4) * -1) + hitPointOutPut.z;

            }
        }
        else
        {
            //Lower then two
            if (hitPointOutPut.z < 0)
            {
                //Negative
                hitPointOutPut.z = hitPointOutPut.z + (hitPointOutPut.z % 4) * -1;
            }
            else
            {
                hitPointOutPut.z = hitPointOutPut.z - (hitPointOutPut.z % 4);
            }
        }




        return hitPointOutPut;
    }
}

public enum gridPlaceType
{
    hyperLane,
    wormHole,
    empireSpawn,
    nebula,
    solarSystemInitializer,
    nullPlace
}
