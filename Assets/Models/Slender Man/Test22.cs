using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test22 : Singleton<Test22>
{
    
    public Transform targetObject;
    public Camera playerCamera;
    public float minScreenDistance = 0f;
    public float maxScreenDistance = 100f;
    
    public float minValue = 0f;
    public float maxValue = 10f;
    
    private Renderer objectRenderer;
    [Header("Slice")]

    public PosterizeEffect PosterizeEffect;
    public float minPosterize;
    public float maxPosterize;
    
    public BloomEffect BloomEffect;
    public float minBloom;
    public float maxBloom;
    
        
    public BadTVEffect badtvEffect;
    public float minBadTv;
    public float maxBadTv;

    public AudioSource source;

    public float minDistance;


    private void Update()
    {
        Vector3 screenPoint = playerCamera.WorldToScreenPoint(targetObject.position);
        bool isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < Screen.width && screenPoint.y > 0 && screenPoint.y < Screen.height && Vector3.Distance(transform.position,targetObject.position) <minDistance ;

        if (isVisible)
        {
            float screenDistance = Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(screenPoint.x, screenPoint.y));
            
            float t = Mathf.InverseLerp(maxScreenDistance, minScreenDistance, screenDistance);
            
            float value = Mathf.Lerp(minValue, maxValue, t);
            PosterizeEffect.on = true;
            BloomEffect.on = true;
            source.enabled = true;

            PosterizeEffect.level = (int)Mathf.Lerp(minPosterize, maxPosterize,t);
            BloomEffect.strength = Mathf.Lerp(minBloom, maxBloom,t);
            badtvEffect.fineDistort = Mathf.Lerp(minBadTv, maxBadTv, t);
            source.volume = Mathf.Lerp(0, 1, t);
            // sliceEffect.slices = (int)Mathf.Lerp(minSliceEffect, maxSliceEffect, t);
            Debug.Log($"Value: {value}");
        }
        else
        {
            // PosterizeEffect.on = false;
            // BloomEffect.on = false;
            // source.enabled = false;
        }
    }

    public void Tester1()
    {
        PosterizeEffect.on = true;
        BloomEffect.on = true;
        PosterizeEffect.level = (int)maxPosterize;
        BloomEffect.strength =  maxBloom;
        badtvEffect.fineDistort = maxBadTv;
    }
    
    
    public void Tester2()
    {
        PosterizeEffect.on = false;
        BloomEffect.on = false;
        PosterizeEffect.level = (int)maxPosterize;
        BloomEffect.strength =  maxBloom;
        badtvEffect.fineDistort = minBadTv;
    }


}
