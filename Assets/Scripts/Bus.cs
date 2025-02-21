using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Bus : MonoBehaviour
{
    public List<GameObject> tires = new List<GameObject>();
    public Vector3 moveDirectionVector;
    public float targetSpeed = 5f; // Maksimum hız
    public float acceleration = 2f; // Hızlanma oranı
    public float deceleration = 5f; // Yavaşlama oranı
    private float currentSpeed = 0f; // Mevcut hız
    private bool isObstacleAhead = false; // Engel var mı?

    public Vector3 rotateDirectionVector;
    public float rotationSpeed = 90f; // Maksimum dönüş hızı (derece/saniye)
    public float rotationAcceleration = 2f; // Dönüş hızlanma
    public float rotationDeceleration = 5f; // Dönüş yavaşlama
    private float currentRotationSpeed = 0f; // Mevcut rotasyon hı

    void Update()
    {
        // Engel varsa yavaşça dur, yoksa hızlan
        float speedLerpFactor = isObstacleAhead ? deceleration : acceleration;
        float desiredSpeed = isObstacleAhead ? 0f : targetSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, desiredSpeed, speedLerpFactor * Time.deltaTime);

        // Nesneyi hareket ettir
        transform.Translate(moveDirectionVector * currentSpeed * Time.deltaTime);

        // Dönüş için aynı mantık
        float rotationLerpFactor = isObstacleAhead ? rotationDeceleration : rotationAcceleration;
        float desiredRotationSpeed = isObstacleAhead ? 0f : rotationSpeed;
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, desiredRotationSpeed, rotationLerpFactor * Time.deltaTime);

        // Dönüşü uygula
        TireRotator();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteractor playerInteractor)) // Engellerin "Obstacle" tag'i olmalı
        {
            isObstacleAhead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteractor playerInteractor))
        {
            isObstacleAhead = false;
        }
    }


    public void TireRotator()
    {
        foreach (var tire in tires)
        {
            tire.transform.Rotate(rotateDirectionVector * currentRotationSpeed * Time.deltaTime);
        }
    }
}
