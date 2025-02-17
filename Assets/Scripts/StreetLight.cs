using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Windows.WebCam;

public class StreetLight : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    [Button]
    public void PlayAction()
    {
        _audioSource.Play();
        _animator.SetBool("Action",true);
        
    }
    [Button]
    public void EndAction()
    {
        _audioSource.Stop();
        _animator.SetBool("Action",false);
    }
    
    public void PlayAction2()
    {
        _audioSource.Play();
        _animator.SetBool("Action2",true);
        
    }
    [Button]
    public void EndAction2()
    {
        _audioSource.Stop();
        _animator.SetBool("Action2",false);
    }
    
}
