using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project.Systems.GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Behaviors
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float openAngle;
        [SerializeField] private float openDuration;

        private Transform m_playerTransform;
        
        
        private bool m_isOpen = false;
        private bool m_canOpen = true;

        private PlayerActionMaps m_playerInput;

        [Inject]
        private void Inject(PlayerActionMaps playerActionMaps)
        {
            m_playerInput = playerActionMaps;
        }

        private void OnEnable()
        {
            m_playerInput.Interactions.Enable();
            m_playerInput.Interactions.Interact.performed += TryOpenDoor;
        }

        private void Start()
        {
            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnDisable()
        {
            m_playerInput.Interactions.Disable();
            m_playerInput.Interactions.Interact.performed -= TryOpenDoor;
        }

        private void OnMouseOver()
        {
            m_canOpen = true;
        }

        private void OnMouseExit()
        {
            m_canOpen = false;
        }

        private void TryOpenDoor(InputAction.CallbackContext context)
        {
            if (!m_canOpen || Vector3.Distance(transform.position, m_playerTransform.position) > 5f) return;

            if (!m_isOpen)
            {
                m_isOpen = true;
                Open();
            }
            else
            {
                m_isOpen = false;
                Close();
            }
        }
        
        private void Open()
        {
            transform.DORotate(new Vector3(0, -openAngle, 0), openDuration, RotateMode.Fast);
        }

        private void Close()
        {
            transform.DORotate(new Vector3(0, 0, 0), openDuration, RotateMode.Fast);
        }
    }
}
