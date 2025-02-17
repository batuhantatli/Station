using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enviroments : MonoBehaviour
{

    public List<GameObject> environmentTiles = new List<GameObject>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!environmentTiles.Contains(other.gameObject))
            return;

        
        Debug.Log("test");
        int index = environmentTiles.IndexOf(other.gameObject);

        if (index == 0) // İlk obje ise
        {
            GameObject lastTile = environmentTiles[environmentTiles.Count - 1];
            lastTile.transform.localPosition += new Vector3(0, 0, -300);
        }
        else if (index == environmentTiles.Count - 1) // Son obje ise
        {
            GameObject firstTile = environmentTiles[0];
            firstTile.transform.localPosition += new Vector3(0, 0, 300);
        }

        // Listeyi pozisyonlarına göre sırala
        environmentTiles = environmentTiles.OrderBy(tile => tile.transform.position.z).ToList();
    }
}
