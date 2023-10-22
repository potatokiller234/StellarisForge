using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveGridNode
{
    [Header("Stellaris Info")]
    //Stellaris Info
    public gridPlaceType save_placeType;
    public data_system save_systemData;
    [Header("Unity Info")]
    public saveColor save_color;
    public saveColor save_emissiveColor;
    public int save_emissiveNumber = -1;
    public System.Guid save_UI_father = System.Guid.Empty;
    public Vector3 save_position;


    public List<System.Guid> save_connectedHyperlanes;
    public List<System.Guid> save_connectedNodes;

    //Used for ID
    public System.Guid save_ID;

    public void saveNewNode(GameObject gridNode)
    {
        gridData temp = gridNode.GetComponent<gridData>();

        save_placeType = temp.placeType;
        save_systemData = temp.systemData;
        save_color = new saveColor(temp.color);
        save_emissiveColor = new saveColor(temp.emissiveColor);
        save_emissiveNumber = temp.emissiveNumber;
        save_UI_father = temp.UI_father;

        save_connectedHyperlanes = new List<System.Guid>(temp.connectedHyperlanes);
        save_connectedNodes = new List<System.Guid>(temp.connectedNodes);
        save_position = gridNode.gameObject.transform.position;
        save_ID = temp.ID;

    }
    public void loadNewNode(GameObject dataToLoad)
    {
        gridData temp = dataToLoad.GetComponent<gridData>();

        temp.placeType = save_placeType;
        temp.systemData = save_systemData;
        temp.color = save_color.getColor();
        temp.emissiveColor = save_emissiveColor.getColor();
        temp.emissiveNumber = save_emissiveNumber;
        temp.UI_father = save_UI_father;

        temp.connectedHyperlanes = new List<System.Guid>(save_connectedHyperlanes);
        temp.connectedNodes = new List<System.Guid>(save_connectedNodes);
        dataToLoad.transform.position = save_position;
        temp.ID = save_ID;

    }
}
