using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public AudioSource _source;
    public Animator _animator;
    public AudioClip hitClip;

    private void Awake()
    {
    }

    public void Hit()
    {
        _animator.SetBool("Stun",true);
    }

    // Start is called before the first frame update
    public void HitSound()
    {
        _source.clip = hitClip;
        _source.Play();
    }
}
