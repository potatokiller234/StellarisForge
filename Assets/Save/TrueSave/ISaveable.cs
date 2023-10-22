using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    object saveState();
    void loadState(object state);
}

