using System.Collections;
using System.Collections.Generic;


public class stellarisDataHolder
{
    public List<varableNode> variableHolder = new List<varableNode>();
    string finalOutput = "";
    public stellarisNode infoHolder;
    int doneTimes = 0;
    public stellarisDataHolder(string headNodeName)
    {
        infoHolder = new stellarisNode();
        infoHolder.name = headNodeName;
        infoHolder.data = null;
        infoHolder.changeParent(infoHolder,infoHolder);
        infoHolder.refToParentDataHolder = this;
    }
    public stellarisNode addChild(string name, string data, stellarisNode parentNode)
    {
        if(parentNode.dataHolder == null)
        {
            parentNode.dataHolder = new List<stellarisNode>();
        }
        stellarisNode bob = new stellarisNode(name, data,parentNode);
       // myStaticScripts.staticScripts.debugMessage("Name " + bob.name + " Data " + bob.data + " parent ");

        parentNode.dataHolder.Add(bob);
        return bob;
        
    }
    public stellarisNode addChild(string name, string data, string fileID, stellarisNode parentNode)
    {
        if (parentNode.dataHolder == null)
        {
            parentNode.dataHolder = new List<stellarisNode>();
        }
        stellarisNode bob = new stellarisNode(name, data,fileID, parentNode);
        // myStaticScripts.staticScripts.debugMessage("Name " + bob.name + " Data " + bob.data + " parent ");

        parentNode.dataHolder.Add(bob);
        return bob;

    }
    public stellarisNode addChild(stellarisNode parentNode, stellarisNode thingToAdd)
    {
        if (parentNode.dataHolder == null)
        {
            parentNode.dataHolder = new List<stellarisNode>();
        }

        // myStaticScripts.staticScripts.debugMessage("Name " + bob.name + " Data " + bob.data + " parent ");

        parentNode.dataHolder.Add(thingToAdd);
        return thingToAdd;

    }

    /// <summary>
    /// USE ONLY FOR ERROR HANDLING ONLY!!!!!
    /// </summary>
    /// <param name="name"></param>
    /// <param name="data"></param>
    /// <param name="parentNode"></param>
    /// <returns></returns>
    public stellarisNode addChild_ERROR(stellarisNode child, stellarisNode parentNode)
    {
        if (parentNode.dataHolder == null)
        {
            parentNode.dataHolder = new List<stellarisNode>();
        }
        // myStaticScripts.staticScripts.debugMessage("Name " + bob.name + " Data " + bob.data + " parent ");

        parentNode.dataHolder.Add(child);
        return child;
    }

    public stellarisNode findData(string name, string data, stellarisNode parentNode)
    {
        foreach(stellarisNode throg in parentNode.dataHolder)
        {
            if(throg.name == name && throg.data == data)
            {
                return throg;
            }
        }
        return null;
    }
    public string printOutTree(stellarisNode parent)
    {
        finalOutput = "START OF NEW FOLDER----------------------------------------------------\n\n\n";
        //myStaticScripts.staticScripts.debugMessage("Use MEEEEEEEE " + parent.dataHolder[0].name + " (name) " + parent.dataHolder[0].data + " (data)");
        printOutTreeRecursive(parent,0);
        finalOutput += printOutVariables();
        myStaticScripts.staticScripts.debugMessage("Final output length " + finalOutput.Length);
        return finalOutput;        
    }

    public string preOrder(stellarisNode parent)
    {
        string result = "";
        if (parent == null)
            return result;

        Stack<stellarisNode> nodeStack = new Stack<stellarisNode>();
        nodeStack.Push(parent);
        int level = 1;

        stellarisNode temp;
        while(nodeStack.Count > 0)
        {
            temp = nodeStack.Peek();
            result += System.String.Format("(Name)-{0,-30} (Data)-{1,-30} (FileName)-{2,-20} (Level)-{3,-4}", temp.name, temp.data, temp.fileID, 0);
            result += "\n";

            if(temp.dataHolder != null)
            {
                for (int i = 0; i < temp.dataHolder.Count; i++)
                {
                    nodeStack.Push(temp.dataHolder[i]);
                }
            }
        }

        return result;
    }

    public List<string> printOutTreeNames(stellarisNode parent)
    {
        List<string> output = new List<string>();
        printOutTreeNameRecursive(parent, 0, output);
        //finalOutput += printOutVariables();
        return output;
    }
    private string printOutVariables()
    {
        string varCombined = "\n\nVariables\n";
        foreach(varableNode shrekLover69 in variableHolder)
        {
            string tempFormat = System.String.Format("(Name)-{0,-10} (Data)-{1,-10} (FileName)-{2,-10}", shrekLover69.name, shrekLover69.data, shrekLover69.fileID);
            varCombined += tempFormat + "\n";
        }
        varCombined += "\n\n------------------------------------------------------------\n\n\n";
        return varCombined;
    }
    int calledAmount = 0;
    private void printOutTreeRecursive(stellarisNode parent,int level)
    {
        string tempFormat = System.String.Format("(Name)-{0,-40} (Data)-{1,-30} (FileName)-{2,-20} (Level)-{3,-4}", parent.name, parent.data,parent.fileID, level);
        //finalOutput += "(Name)-" +parent.name + " (Data)-" + parent.data + " (Level)-" + level + "\n";
        finalOutput += tempFormat + "\n";
        if (parent.dataHolder == null)
        {
            //STOP tree has not been made
        }
        else
        {
            if(parent.dataHolder != null)
            {
                for(int i =0;i<parent.dataHolder.Count;i++)
                {

                    printOutTreeRecursive(parent.dataHolder[i], level + 1);
                }
 
            }
        }
    }
    private void printOutTreeNameRecursive(stellarisNode parent, int level, List<string> output)
    {
        bool isUnique = true;

        foreach(string bob in output)
        {
            if(bob == parent.name)
            {
                isUnique = false;
                break;
            }
        }

        if(isUnique)
            output.Add(parent.name);




        if (parent.dataHolder == null)
        {
            //STOP tree has not been made
        }
        else
        {
            if (parent.dataHolder != null)
            {
                for (int i = 0; i < parent.dataHolder.Count; i++)
                {

                    printOutTreeNameRecursive(parent.dataHolder[i], level + 1, output);
                }

            }
        }
    }
}

public class stellarisNode
{
    public stellarisNode parent;
    public List<stellarisNode> dataHolder;
    public stellarisDataHolder refToParentDataHolder;

    public string data;//Stored after the =
    public string name;//Thing stored before the =
    public string fileID;//Id of file 
    public stellarisNode(string name,string data, stellarisNode parent)
    {
        //myStaticScripts.staticScripts.debugMessage("Name " + name + " Data " + data + " parent " + parent.name);
        this.parent = parent;
        this.data = data;
        this.name = name;
        dataHolder = null;
    }
    public stellarisNode(string name, string data,string fileID, stellarisNode parent)
    {
        //myStaticScripts.staticScripts.debugMessage("Name " + name + " Data " + data + " parent " + parent.name);
        this.parent = parent;
        this.data = data;
        this.name = name;
        this.fileID = fileID;
        dataHolder = null;
    }
    public stellarisNode(string name, string data)
    {
        this.parent = null;
        this.data = data;
        this.name = name;
        dataHolder = null;
    }
    public stellarisNode()
    {
        parent = null;
        dataHolder = null;
        data = "";
    }
    public void changeParent(stellarisNode parent, stellarisNode child)
    {
        child.parent = parent;
    }

    public string getVariableValue(stellarisNode start, string variableToFind)
    {
        stellarisNode temp = start;

        while(temp.parent != null && temp.parent != temp)
        {
            temp = temp.parent;
        }

        //Now we are at headNode
        for(int i = 0;i<temp.refToParentDataHolder.variableHolder.Count;i++)
        {
            if(variableToFind == temp.refToParentDataHolder.variableHolder[i].name)
            {
                return temp.refToParentDataHolder.variableHolder[i].data;
            }
        }
        return null;
    }
}


public class varableNode
{
    public string data;
    public string name;
    //Name of Mod that has this var
    public string fileID;

    //Use for scripted_variables
    public varableNode(string name, string data)
    {
        this.data = data;
        this.name = name;
    }
    public varableNode(string name, string data, string fileID)
    {
        this.data = data;
        this.name = name;
        this.fileID = fileID;
    }

    public override string ToString()
    {
        return System.String.Format("(Name)-{0,-50} (Data)-{1,-7} (FileName)-{2,-20}", name, data,fileID);
    }
}
