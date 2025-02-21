using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begger : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetBool("Beg",true);
    }
}