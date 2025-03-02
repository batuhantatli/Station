using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class SpawnedCube : MonoBehaviour
{
    public float speed;
    public bool t;
    public List<BoxCollider> c = new List<BoxCollider>();
    public Color changedColor;
    public void MoveNextPoint(Vector3 movePos)
    {
        transform.DOMove(movePos, speed).OnComplete((() =>
        {
            foreach (var VARIABLE in c)
            {
                VARIABLE.enabled = true;
            }
        }));
    }

    public SpawnedCube second;
    
    public bool isFirst;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FirstPersonController player) && !t && second != null) 
        {
            if (isFirst)
            {
                t = true;
                return;
            }

            t = true;
            second.second = CubeTester.Instance.CubeSpawned(this.second,1);
            if ( CubeTester.Instance.mostCube.transform.position.y < transform.position.y && CubeTester.Instance.mostCube != this)
            {
                GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", changedColor);
                CubeTester.Instance.mostCube.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
                CubeTester.Instance.mostCube = this;
            }
        }
        
    }
}
