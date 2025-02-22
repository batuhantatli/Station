using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zomb : MonoBehaviour
{
    private Animator _animator;
    public enum WalkerType
    {
        Carrier,
        Old,
        Normal
    }
    public float speed;
    public WalkerType walkerType;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (walkerType == WalkerType.Carrier)
        {
            _animator.SetBool("CarryWalk",true);
        }
        else if (walkerType == WalkerType.Old)
        {
            _animator.SetBool("OldWalk",true);
        }
        else
        {
            var g = Random.Range(0, 3);
            _animator.SetBool("Walk"+g,true);
        }
    }
    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

}
