using DG.Tweening;
using UnityEngine;

namespace Project.Systems.Interactable
{
    public class Door : Interactable
    {
        [SerializeField] private float openAngle;
        [SerializeField] private float openDuration;

        private bool m_isOpen;
        
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
