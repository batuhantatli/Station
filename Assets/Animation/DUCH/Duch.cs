using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Duch : MonoBehaviour
{
    private AudioSource _source;
    private Animator _animator;
    public FirstPersonController _firstPersonController;
    private bool hPoint;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hPoint = true;
            _animator.SetTrigger("FirstMeet");    
            _animator.SetBool("HPoint",hPoint);
        }
    }

    public void HPoint()
    {
        _firstPersonController.enabled = false;
        _source.Play();
    }
}
