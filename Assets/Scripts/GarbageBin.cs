using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class GarbageBin : MonoBehaviour,IInteractable
{
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioSource _source;
    [Range(.1f,5)]public float searchTime;
    public bool isSearching;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Interactable()
    {
        isSearching = true;
        _source.clip = GetRandomTrashSound();
        _source.Play();
        this.CallWithDelay((() =>
        {
            isSearching = false;
            _source.Stop();
        }),searchTime);
    }

    public bool SetDistance(Transform player , float minDistance)
    {
        if (Vector3.Distance(transform.position , player.position) <= minDistance)
        {
            return true;
        }

        return false;
    }

    public bool IsInteracting()
    {
        return isSearching;
    }

    public AudioClip GetRandomTrashSound()
    {
        return clips.GetRandomElementEnd(clips.Count);
    }
}
