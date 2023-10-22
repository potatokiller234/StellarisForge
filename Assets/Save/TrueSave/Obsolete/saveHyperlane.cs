using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveHyperlane
{
    public System.Guid save_firstNode = System.Guid.Empty, save_secondNode = System.Guid.Empty;
    public System.Guid save_ID;

    public Vector3 save_startPos, save_endPos;


    public void saveNewHyperlane(GameObject toSaveHype)
    {
        gridHyperlane temp = toSaveHype.GetComponent<gridHyperlane>();
        save_firstNode = temp.firstNode;
        save_secondNode = temp.secondNode;
        save_ID = temp.ID;
        save_startPos = temp.linerend.GetPosition(0);
        save_endPos = temp.linerend.GetPosition(1);

    }

    public void loadNewHyperlane(GameObject dataToLoad)
    {
        gridHyperlane temp = dataToLoad.GetComponent<gridHyperlane>();
        temp.firstNode = save_firstNode;
        temp.secondNode = save_secondNode;
        temp.ID = save_ID;
        temp.linerend.positionCount = 2;
        temp.linerend.SetPosition(0, save_startPos);
        temp.linerend.SetPosition(1, save_endPos);
    }
}
