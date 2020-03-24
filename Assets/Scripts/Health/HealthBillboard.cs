using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the prefab's health bar always face the main camera
/// </summary>
public class HealthBillboard : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
