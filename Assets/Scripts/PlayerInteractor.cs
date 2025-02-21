using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using StarterAssets;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interact ready settings")] public Image crosshair;
    public Color crosshairInteractColor;
    public float increaseCrosshairAmount;
    public float increaseSizeSpeed;
    public float interactDistance;

    public GameObject searchImage;
    public Image searchFiller;

    [Header("Npc Speak Settings")] public CinemachineVirtualCamera npcCamera;
    public CinemachineVirtualCamera playerCamera;

    private bool _isInteracted;
    private Color _crosshairBaseColor;
    private bool _isSized;
    private Vector3 _hitPoint;
    private bool _hitDetected = false;
    private float _rayDistance = 100f;
    private float _radius = 0.5f;
    private Camera _camera;
    private Ray _ray;
    private RaycastHit _hit;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _crosshairBaseColor = crosshair.color;
    }

    void Update()
    {
        _ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(_ray, out _hit, _rayDistance))
        {
            _hitPoint = _hit.point;
            
            if (_hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (!interactable.SetDistance(transform)) return;

                ReadyForInteract();

                if (interactable is Npcs npcs)
                {
                    if (Input.GetKeyDown(KeyCode.E) && !_isInteracted)
                    {
                        _isInteracted = true;
                        SetInteract(npcs.GetCameraLookPoint() , npcs.cameraPos);
                        npcs.SetPlayerInteractor(this);
                        npcs.Dialog((() =>
                        {
                            NpcCamera(false);
                        }));
                    }
                }
            }
            else
            {
                WaitForInteract();
            }
        }
        else
        {
            
            WaitForInteract();
        }
    }

    public void ReadyForInteract()
    {
        if (_isSized) return;
        _isSized = true;
        crosshair.transform.DOScale(Vector3.one * increaseCrosshairAmount, increaseSizeSpeed);
        crosshair.DOColor(crosshairInteractColor, increaseSizeSpeed);
    }

    private void WaitForInteract()
    {
        if (crosshair.transform.localScale != Vector3.one) // Gereksiz işlemi önle
        {
            crosshair.transform.localScale = Vector3.one;
            crosshair.DOColor(_crosshairBaseColor, .1f);
            _isSized = false;
            _isInteracted = false;
        }
    }

    public void FillSearchImage(float fillTime)
    {
        crosshair.gameObject.SetActive(false);
        searchImage.gameObject.SetActive(true);

        searchFiller.DOFillAmount(1, fillTime).SetEase(Ease.Linear).OnComplete((() =>
        {
            searchImage.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(true);
            searchFiller.fillAmount = 0;
        }));
    }

    public void NpcCamera(bool isSpeaking, Transform npcLookPoint = null, Transform npcCameraPos = null)
    {
        if (isSpeaking)
        {
            SetInteract(npcLookPoint , npcCameraPos);
        }
        else
        {
            SetPlayerCamera();
        }
    }

    public void SetInteract(Transform npc , Transform npcCameraPos)
    {
        npcCamera.LookAt = npc;
        npcCamera.transform.position = npcCameraPos.position;
        npcCamera.Priority = 11;
        playerCamera.Priority = 10;
    }

    public void SetPlayerCamera()
    {
        npcCamera.Priority = 10;
        playerCamera.Priority = 11;
    }


    private void OnDrawGizmos()
    {
        if (_camera != null)
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

            // Yeşil Ray çiz
            Gizmos.color = Color.green;
            Gizmos.DrawRay(ray.origin, ray.direction * _rayDistance);

            // Çarpışma olduysa kırmızı küre çiz
            if (_hitDetected)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_hitPoint, _radius);
            }
        }
    }
}