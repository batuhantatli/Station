using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public class WalkSound : MonoBehaviour
{
    public List<AudioClip> walks = new List<AudioClip>();
    public AudioSource s;
    private StarterAssetsInputs starter;
    public bool tester;

    public PostProcessVolume volume;
    public CinemachineVirtualCamera c;
    public float lensTarget;
    public float fovTarget;
    public float fovDefault;
    public float speed;
    public bool isIncreasing;
    public float elapsedTime;
    public LensDistortion lens;
    
    private void Awake()
    {
        starter = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Sağ tık basıldığında
        {
            isIncreasing = true;
            elapsedTime = 0f; // Zamanı sıfırla
        }
        else if (Input.GetMouseButtonUp(1)) // Sağ tık bırakıldığında
        {
            isIncreasing = false;
            c.m_Lens.FieldOfView = 40;
            if (volume.profile.TryGetSettings(out LensDistortion t))
            {
                t.intensity.value = 0;
            }
            
        }

        if (isIncreasing)
        {
            if (volume.profile.TryGetSettings(out LensDistortion t))
            {
                t.intensity.value = Mathf.Lerp(0, lensTarget, elapsedTime / speed);
            }
            elapsedTime += Time.deltaTime;
            c.m_Lens.FieldOfView = Mathf.Lerp(40, fovTarget, elapsedTime / speed);
        }
        
        if (starter.move == Vector2.zero)
        {
            tester = false;
            GetComponent<Animator>().SetBool("Walk",false);
        }
        else
        {
            tester = true;
            GetComponent<Animator>().SetBool("Walk",true);

        }
    }

    public void GetRandomWalk()
    {
        var g = walks[Random.Range(0, walks.Count)];
        s.clip = g;
        s.Play();
    }
}
