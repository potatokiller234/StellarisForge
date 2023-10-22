using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveUINode
{
    public data_system save_Data_System;
    public string save_initName;
    public saveColor save_Color;
    public List<System.Guid> save_spawnedKids;
    public System.Guid save_ID;


    public void saveNewUI(GameObject toSaveUINode)
    {
        UI_nodeDataSystem temp = toSaveUINode.GetComponent<UI_nodeDataSystem>();
        save_Data_System = temp.getSystemData();
        save_initName = temp.initName;
        save_Color = new saveColor(temp.color);
        save_spawnedKids = temp.spawnedKids;
    }

    public void loadNewUI(GameObject toLoadUINode)
    {
        UI_nodeDataSystem temp = toLoadUINode.GetComponent<UI_nodeDataSystem>();
        temp.Data_System = save_Data_System;
        temp.initName = save_initName;
        temp.color = save_Color.getColor();
        temp.spawnedKids = new List<System.Guid>(save_spawnedKids);
    }
}
