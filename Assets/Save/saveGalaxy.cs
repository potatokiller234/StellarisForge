using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveGalaxy
{
    //Holdes saved Nodes Data
    //Holdes saved Hyperlane Data
    //For Ui when parsing back to unity find if parsed fileNames

    public List<saveGridNode> nodes;
    public List<saveHyperlane> hyperlanes;
    public List<saveUINode> UI_nodes;

    //Holds all parsed dataSystemsBefore
    public List<data_system> systemHolder;


    public List<int> gridNode;
    public List<int> hyperlane;
    public List<int> UI_nodeButtons;

    //Used for good fellas
    public int goodfella_gridNode_currentID;
    public int goodfella_hyperlane_currentID;
    public int goodfella_UI_nodeButtons_currentID;


    public string save_unityName;
    public string save_version;
    public string save_stellarisMapTitle;
    public string save_currentProjectName;
    public string save_steamInfo;

    public int ID;


    public saveGalaxy(int ID)
    {
        this.ID = ID;
    }

    public void saveNodeData()
    {
        ////First we get all the ID refs
        //gridNode = new List<int>(place_manager._place_manager.gridNode.getSmallFellaID());
        //hyperlane = new List<int>(place_manager._place_manager.hyperlane.getSmallFellaID());
        //UI_nodeButtons = new List<int>(place_manager._place_manager.UI_nodeButtons.getSmallFellaID());

        ////Then we init nodes and hyperlanes
        //nodes = new List<saveGridNode>();
        //hyperlanes = new List<saveHyperlane>();
        //UI_nodes = new List<saveUINode>();

        ////Now we can start storing the gridNodes Data
        ////Used so we don't gotta make a new ref everytime for loop runs
        //saveGridNode tempGridNode;
        //for (int i = 0; i < place_manager._place_manager.gridNode.getCount(); i++)
        //{
        //    tempGridNode = new saveGridNode();
        //    tempGridNode.saveNewNode(place_manager._place_manager.gridNode.getID(i, true));
        //    nodes.Add(tempGridNode);
        //}
        ////Used so we don't gotta make a new ref everytime for loop runs
        //saveHyperlane tempHyperlane;
        //for (int i = 0; i < place_manager._place_manager.gridNode.getCount(); i++)
        //{
        //    tempHyperlane = new saveHyperlane();
        //    tempHyperlane.saveNewHyperlane(place_manager._place_manager.hyperlane.getID(i, true));
        //    hyperlanes.Add(tempHyperlane);
        //}
        //saveUINode tempUINode;
        //for (int i = 0; i < place_manager._place_manager.gridNode.getCount(); i++)
        //{
        //    tempUINode = new saveUINode();
        //    tempUINode.saveNewUI(place_manager._place_manager.UI_nodeButtons.getID(i, true));
        //    UI_nodes.Add(tempUINode);
        //}

        ////Now save infos
        //save_unityName = cleanRef._cleanRef._UI_galaxy_info.galaxyName.text;
        //save_version = cleanRef._cleanRef._UI_galaxy_info.version.text;
        //save_stellarisMapTitle = cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text;
        //save_currentProjectName = cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text;
        //save_steamInfo = cleanRef._cleanRef._UI_galaxy_info.steamInfo.text;

        //this.systemHolder = cleanRef._cleanRef._solarSystemSpawn.systemHolder;
    }

    public void loadNodeData()
    {
        //Spawn in all the nodes
        //Spawn in all hyperlanes
        //Fix connections with UI_buttons (or not)
    }

}
