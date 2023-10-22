using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hardNodes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UI_nodeDataSystem bob = this.GetComponent<UI_nodeDataSystem>();

        if(bob.ID == System.Guid.Empty)
        {
            bob.ID = idGenerator._idGenerator.generateNewID();
            this.GetComponent<SaveableEntity>().setID(bob.ID);
        }
    }
}
