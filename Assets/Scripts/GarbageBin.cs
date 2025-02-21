using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GarbageBin : MonoBehaviour,IInteractable
{
    [Header("Search")]
    public bool isSearching;
    [Range(.1f,5)]public float searchTime;
    public float strentgh=4;
    public int vibration=4;
    public float randomness = 90;
    public List<AudioClip> searchSound = new List<AudioClip>();
    
    [Header("Interact")]
    public float interactDistance;
    
    private AudioSource _source;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Interactable()
    {
        isSearching = true;
        _source.clip = GetRandomTrashSound();
        _source.Play();
        // SearchRotate();
        this.CallWithDelay((() =>
        {
            isSearching = false;
            _source.Stop();
        }),searchTime);
    }

    public bool SetDistance(Transform player)
    {
        if (Vector3.Distance(transform.position , player.position) <= interactDistance)
        {
            return true;
        }

        return false;
    }

    [Button]
    public void SearchRotate()
    {
        transform.DOShakeRotation(searchTime, Vector3.one * strentgh, vibration, randomness).SetEase(Ease.Linear);
    }

    public bool IsInteracting()
    {
        return isSearching;
    }

    public AudioClip GetRandomTrashSound()
    {
        return searchSound.GetRandomElementEnd(searchSound.Count);
    }

    public float InteractTime()
    {
        return searchTime;
    }

    public float InteractDistance()
    {
        return interactDistance;
    }
}
