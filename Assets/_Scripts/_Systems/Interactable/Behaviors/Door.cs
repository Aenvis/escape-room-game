using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project.Systems.GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Systems.Interactable
{
    public class Door : Interactable
    {
        [SerializeField] private float openAngle;
        [SerializeField] private float openDuration;

        private bool m_isOpen = false;

        private void Open()
        {
            transform.DORotate(new Vector3(0, -openAngle, 0), openDuration, RotateMode.Fast);
        }

        private void Close()
        {
            transform.DORotate(new Vector3(0, 0, 0), openDuration, RotateMode.Fast);
        }

        protected override void Interaction()
        {
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
    }
}
