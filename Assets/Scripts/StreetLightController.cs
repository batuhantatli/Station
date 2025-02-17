using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightController : MonoBehaviour
{

    public List<StreetLight> streetLights = new List<StreetLight>();


    public void Action()
    {
        foreach (var VARIABLE in streetLights)
        {
            VARIABLE.PlayAction2();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Action();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndAction();
            foreach (var VARIABLE in streetLights)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var VARIABLE in streetLights)
            {
                VARIABLE.gameObject.SetActive(false);
            }
        }
        
    }

    public void EndAction()
    {
        foreach (var VARIABLE in streetLights)
        {
            VARIABLE.EndAction2();
        }
    }
}
