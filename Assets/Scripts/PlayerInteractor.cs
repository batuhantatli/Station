using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interact ready settings")]
    public Image crosshair;
    public Camera _camera;
    public Color crosshairInteractColor;
    public float increaseCrosshairAmount;
    public float increaseSizeSpeed;
    public float minDistanceWithInteractable;

    private Color _crosshairBaseColor;
    private bool _isSized;
    private float _radius = 0.5f;  // Küre yarıçapı
    private float _rayDistance = 100f;  // Ray uzunluğu
    private Vector3 _hitPoint;  // Çarpışma noktası
    private bool _hitDetected = false;  // Çarpışma olup olmadığını kontrol eder

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
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            _hitPoint = hit.point; // Çarpışma noktasını kaydet
            _hitDetected = true;
            if (hit.collider.gameObject.TryGetComponent(out IInteractable t ))
            {
                if (t.SetDistance(transform,minDistanceWithInteractable))
                {
                    if (Input.GetKeyDown(KeyCode.E) && !t.IsInteracting())
                    {
                        t.Interactable();
                    }
                    IncreaseSizeCrosshair();
                }
                else
                {
                    ResetScale();
                }
            }
            else
            {
                ResetScale();
            }
        }
        else
        {
            _hitDetected = false;
            _isSized = false;
            ResetScale();
        }
    }

    public void IncreaseSizeCrosshair()
    {
        if (_isSized) return;
        _isSized = true;
        crosshair.transform.DOScale(Vector3.one * increaseCrosshairAmount, increaseSizeSpeed);
        crosshair.DOColor(crosshairInteractColor, increaseSizeSpeed);

    }
    
    private void ResetScale()
    {
        if (crosshair.transform.localScale != Vector3.one) // Gereksiz işlemi önle
        {
            crosshair.transform.localScale = Vector3.one;
            crosshair.DOColor(_crosshairBaseColor, .1f);
            _isSized = false;
        }
    }
    
    
    private void OnDrawGizmos()
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
