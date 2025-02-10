using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform x;

    public bool y;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            y = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            y = false;
        }

        if (y)
        {
            transform.LookAt(x);
        }
    }
}