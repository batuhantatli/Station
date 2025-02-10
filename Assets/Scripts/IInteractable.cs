

using System.Transactions;
using UnityEngine;

public interface IInteractable
{
    public void Interactable();
    public bool SetDistance(Transform player,float minDistance);
    public bool IsInteracting();
}
