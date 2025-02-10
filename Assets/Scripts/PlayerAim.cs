using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;

    }

    public Image crosshair;
    public float radius = 0.5f;  // Küre yarıçapı
    public float rayDistance = 100f;  // Ray uzunluğu
    private Vector3 hitPoint;  // Çarpışma noktası
    private bool hitDetected = false;  // Çarpışma olup olmadığını kontrol eder

    void Update()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            hitPoint = hit.point; // Çarpışma noktasını kaydet
            hitDetected = true;
            if (hit.collider.gameObject.TryGetComponent(out IInteractable t ))
            {
                IncreaseSizeCrosshair();
                Debug.Log("t");
            }
            else
            {
                ResetScale();
            }
        }
        else
        {
            hitDetected = false;
            isSized = false;
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        
        // Yeşil Ray çiz
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);

        // Çarpışma olduysa kırmızı küre çiz
        if (hitDetected)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitPoint, radius);
        }
    }

    public float increaseCrosshairAmount;
    public float increaseSizeSpeed;
    public bool isSized;
    public void IncreaseSizeCrosshair()
    {
        if (isSized) return;
        isSized = true;
        crosshair.transform.DOScale(Vector3.one * increaseCrosshairAmount, increaseSizeSpeed).SetRelative();
    }
    
    private void ResetScale()
    {
        if (crosshair.transform.localScale != Vector3.one) // Gereksiz işlemi önle
        {
            crosshair.transform.localScale = Vector3.one;
        }
    }
}
