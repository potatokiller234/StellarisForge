using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_nodeDataFile : MonoBehaviour
{
    public List<UI_nodeDataSystem> UI_nodeDatasystemHolder;
    TextMeshProUGUI UI_text;
    public string fileName;

    public UI_nodeDataMod cool_parent;

    [SerializeField]
    private int disabledAmount = 0;


    public void disableChild(GameObject child)
    {
        child.SetActive(false);
        disabledAmount++;
        if (disabledAmount == UI_nodeDatasystemHolder.Count)
        {
            cool_parent.disableChild(this.gameObject);
        }
    }

    public void resetDisabled()
    {
        disabledAmount = 0;
    }

    public void UI_setNewNodeDataFile(string fileName, TextMeshProUGUI UI_text)
    {
        this.UI_text = UI_text;
        this.UI_text.text = fileName;
        UI_nodeDatasystemHolder = new List<UI_nodeDataSystem>();
        this.fileName = fileName;
    }

    public void UI_addNewSystemNode(UI_nodeDataSystem bob)
    {
        UI_nodeDatasystemHolder.Add(bob);
    }

    private void OnDisable()
    {
        if (UI_nodeDatasystemHolder == null)
            return;

        for (int i = 0;i < UI_nodeDatasystemHolder.Count;i++)
        {
            UI_nodeDatasystemHolder[i].gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (UI_nodeDatasystemHolder == null)
            return;
        disabledAmount = 0;

        for (int i = 0; i < UI_nodeDatasystemHolder.Count; i++)
        {
            UI_nodeDatasystemHolder[i].gameObject.SetActive(true);
        }
    }

}
