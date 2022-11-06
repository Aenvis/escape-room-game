using System;
using DG.Tweening;
using JetBrains.Annotations;
using Project.Systems.Equipment;
using Project.Systems.Quest;
using UnityEngine;
using Zenject;

namespace Project.Systems.Interactable
{
    public class Door : Interactable
    {
        [SerializeField] private float openAngle;
        [SerializeField] private float openDuration;
        [SerializeField] [CanBeNull] private QuestData quest;

        private Inventory m_inventory;
        private bool m_isOpen;

        [Inject]
        private void Injection(Inventory inventory)
        {
            m_inventory = inventory;
        }

        protected override void Start()
        {
            base.Start();
            if(quest != null) quest.Completed = false;
        }
        
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
            if (quest != null)
            {
                if (!quest.Completed)
                {
                    if (m_inventory.Contains(quest.GetRequiredItem()))
                    {
                        m_inventory.RemoveItem(quest.GetRequiredItem());
                        quest.Completed = true;
                    }
                    else
                    {
                        Debug.Log(quest.GetText());
                        return;
                    }
                }
            }

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
