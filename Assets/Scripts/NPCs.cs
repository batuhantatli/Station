using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : MonoBehaviour,IInteractable
{
    public bool isInteracting;
    public Transform cameraLookPoint;
    public void Interactable()
    {
        
    }

    public bool SetDistance(Transform player, float minDistance)
    {
        if (Vector3.Distance(transform.position , player.position) <= minDistance)
        {
            return true;
        }

        return false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public float InteractTime()
    {
        return 0;
    }
}
