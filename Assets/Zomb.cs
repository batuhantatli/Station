using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zomb : MonoBehaviour
{
    public float speed;

    public AudioSource s;

    public List<AudioClip> walks = new List<AudioClip>();
    // Update is called once per frame


    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    public void WalkSound()
    {
        var g = walks[Random.Range(0, walks.Count)];
        s.clip = g;
        s.Play();
    }
}
