using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanRef : MonoBehaviour
{
    public static cleanRef _cleanRef;
    public UI_galaxy_info _UI_galaxy_info;
    public solarSystemSpawn _solarSystemSpawn;
    public UI_buttonsToStatic _UI_buttonsToStatic;
    public UI_parsedToButtons _UI_parsedToButtons;


    private void Awake()
    {
        _cleanRef = this;
    }

    public IEnumerator disableEnable(float timeTillDisable, GameObject thingToEnableAndDisable)
    {
        thingToEnableAndDisable.SetActive(true);
        yield return new WaitForSeconds(1f);
        thingToEnableAndDisable.SetActive(false);
    }
}
