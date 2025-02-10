using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounder : MonoBehaviour
{
    public Transform playerTransform;
    private AudioSource _audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public float distance;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 rightPos = Vector3.up * distance;

            _audioSource.clip = audioClips[0];
            SetAudioClip(rightPos);
            // 4 Noktanın Pozisyonları
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 rightPos = Vector3.down * distance;

            _audioSource.clip = audioClips[1];
            SetAudioClip(rightPos);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {      
            Vector3 rightPos = Vector3.left * distance;

            _audioSource.clip = audioClips[2];
            SetAudioClip(rightPos);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Vector3 rightPos = Vector3.right * distance;
            _audioSource.clip = audioClips[3];
            SetAudioClip(rightPos);

        }
    }

    public void SetAudioClip(Vector3 audioSourcePos)
    {
        _audioSource.transform.localPosition = audioSourcePos;
        _audioSource.Play();

    }
    
}
