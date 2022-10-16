using System;
using System.Collections;
using System.Collections.Generic;
using Project.Systems.GameEvents;
using UnityEngine;
using Project.Systems.GameEvents;

public class PlateBehaviour : MonoBehaviour
{
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private GameEvent activatePlate;
    [SerializeField] private GameEvent deactivatePlate;
    

    private Renderer m_renderer;
    
    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
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
        m_renderer.material = green;
    }
    
    public void Deactivate()
    {
        m_renderer.material = red;
    }
}
