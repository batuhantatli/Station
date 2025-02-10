using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour,IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interactable()
    {
        
    }

    public bool SetDistance(Transform player , float minDistance)
    {
        if (Vector3.Distance(transform.position , player.position) <= minDistance)
        {
            return true;
        }

        return false;
    }
}
