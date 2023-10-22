using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridHyperlane : MonoBehaviour, ISaveable
{
    public System.Guid firstNode = System.Guid.Empty, secondNode = System.Guid.Empty;
    public System.Guid ID;

    //Private
    public GameObject gameObject_firstNode,gameObject_secondNode;
    public LineRenderer linerend;
    public gridData grid_first, grid_second;



    #region Setting Up Data
    public void setFirstNode(System.Guid firstMarriedNode)
    {
        firstNode = firstMarriedNode;

        //Sets up line rend
        linerend.positionCount = 2;
        getGameobject();
        getGridData();
        grid_first.addHyperlane(place_manager._place_manager.supreamDude.getID(this.gameObject));
        linerend.SetPosition(0, grid_first.transform.position);
    }
    public bool setSecondNode(System.Guid secondMarriedNode)
    {
        //Set Class Var
        secondNode = secondMarriedNode;

        //Get Ref to the gridData even if we don't have it
        getGameobject();
        getGridData();

        //If nodes are already connected don't connect again
        if (grid_first.isAlreadyConnectedNode(secondNode) == true)
            return false;
        //We now know that the nodes are not connected;
        //We first get the data
        //Now we set the data

        System.Guid hyperID = place_manager._place_manager.supreamDude.getID(this.gameObject);

        grid_first.addNode(secondNode, hyperID);
        grid_second.addNode(firstNode, hyperID);
        //Then we set the pos for the line render
        linerend.SetPosition(1, gameObject_secondNode.transform.position);
        generateMeshCollider();


        //We are done
        return true;
    }
    #endregion
    #region Getting Data
    /// <summary>
    /// Gets the gridData if they are null and -1
    /// </summary>
    private void getGridData()
    {
        if(firstNode != System.Guid.Empty && grid_first == null)
            grid_first = gameObject_firstNode.GetComponent<gridData>();
        if(secondNode != System.Guid.Empty && grid_second == null)
            grid_second = gameObject_secondNode.GetComponent<gridData>();
    }
    /// <summary>
    /// Gets the gameObjects for firstNode and secondNode if they are null and -1
    /// </summary>
    public void getGameobject()
    {
        if(firstNode != System.Guid.Empty && gameObject_firstNode == null)
        {
            gameObject_firstNode = place_manager._place_manager.supreamDude.getID(firstNode,true);
        }
        if(secondNode != System.Guid.Empty && gameObject_secondNode == null)
        {
            gameObject_secondNode = place_manager._place_manager.supreamDude.getID(secondNode,true);
        }
    }
    #endregion
    #region Utilities
    public void generateMeshCollider()
    {
        if(this.gameObject.GetComponent<MeshCollider>().sharedMesh == null)
        {
            float tempWidth = linerend.widthMultiplier;
            linerend.widthMultiplier = 3;

            MeshCollider collider = this.gameObject.GetComponent<MeshCollider>();
            Mesh mesh = new Mesh();
            linerend.BakeMesh(mesh);
            collider.sharedMesh = mesh;

            linerend.widthMultiplier = tempWidth;
        }
    }
   
    public void removeHyperlane()
    {
        //Debug.Log("Called");
        getGameobject();
        getGridData();
        System.Guid hyperID = place_manager._place_manager.supreamDude.getID(this.gameObject);

        //SecondNode
        if (secondNode != System.Guid.Empty)
        {
            //Debug.Log(grid_first.ID);
            grid_first.removeNode(secondNode);
            grid_first.removeHyperlane(hyperID);
            grid_second.removeNode(firstNode);
            grid_second.removeHyperlane(hyperID);

        }
        else
        {
            //No secondNode
            grid_first.removeHyperlane(hyperID);
        }
        place_manager._place_manager.removeThingFromPlaceManager(this.gameObject, gridPlaceType.hyperLane);
        Destroy(this.gameObject);
    }
    #endregion
    #region Save/Load
    [System.Serializable]
    private struct SaveData
    {
        //ID
        public System.Guid save_ID;

        //Connections
        public System.Guid save_firstNode, save_secondNode;
        public saveVector3 save_startPos, save_endPos;
    }

    public void loadState(object state)
    {
        SaveData saveData = (SaveData)state;

        firstNode = saveData.save_firstNode;
        secondNode = saveData.save_secondNode;
        ID = saveData.save_ID;
        linerend.positionCount = 2;
        linerend.SetPosition(0, saveData.save_startPos.getVector3());
        linerend.SetPosition(1, saveData.save_endPos.getVector3());
        generateMeshCollider();
    }
    public object saveState()
    {
        return new SaveData()
        {
            save_firstNode = firstNode,
            save_secondNode = secondNode,
            save_ID = ID,
            save_startPos = new saveVector3(linerend.GetPosition(0)),
            save_endPos = new saveVector3(linerend.GetPosition(1))
        };
    }
    #endregion
}
