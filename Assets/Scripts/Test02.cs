using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Test02 : MonoBehaviour
{
    public GameObject eye;
    public float forceY;
    public float forceZ;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var g = Instantiate(eye, transform.position, quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(new Vector3(0,forceY,forceZ)*Time.deltaTime,ForceMode.Acceleration);
        }
    }
}
