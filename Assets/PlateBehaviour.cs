using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBehaviour : MonoBehaviour
{
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private GameEvent activatePlate;
    [SerializeField] private GameEvent deactivatePlate;
    

    private Material m_material;
    
    private void Start()
    {
        m_material = GetComponent<Material>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;
        activatePlate.Invoke();
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.CompareTag("Player")) return;
        deactivatePlate.Invoke();
    }

    public void Activate()
    {
        m_material = green; 
    }
    
    public void Deactivate()
    {
        m_material = red;
    }
}
