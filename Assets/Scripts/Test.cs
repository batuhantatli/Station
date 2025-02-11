using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject tugu;
    public GameObject umut;
    public float moveYLength;
    public float moveSpeed;
    public AudioSource _source;

    public PosterizeEffect p;

    public RGBShiftEffect r;
    public float effectTime;
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCoroutine(MoveTogo(tugu));
            // c.LookAt = t;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(MoveTogo(umut));
        }



        if (Input.GetKeyDown(KeyCode.Alpha6))
        {

            // c.LookAt = null;
        }
    }

    public IEnumerator MoveTogo(GameObject player)
    {
        player.gameObject.SetActive(true);
        player.transform.DOMoveY(moveYLength, moveSpeed).SetRelative();
        p.on = true;
        r.on = true;
        DOTween.To(() => p.level, x => p.level = x, 3, .2f);
        DOTween.To(() => r.amount, x => r.amount = x, 0.01f, .2f);
        yield return new WaitForSeconds(effectTime);
        DOTween.To(() => p.level, x => p.level = x, 100, .2f);
        DOTween.To(() => r.amount, x => r.amount = x, 0, .2f).OnComplete((() =>
        {
            p.on = false;
            r.on = false;
        }));
    }
}