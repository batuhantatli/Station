using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private AudioSource s;
    public bool isSound;
    private void Awake()
    {
        s = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out GameObject g) && !isSound)
        {
            isSound = true;
            s.Play();
        }
    }
}
