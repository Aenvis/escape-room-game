using UnityEngine;

namespace Project
{
    public class PostCreditsActions : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private float m_currAnimTime;
        private bool m_animFinished;
        
        private void Update()
        {
            if (m_animFinished) return;

            m_currAnimTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (m_currAnimTime < 1.0f) return;

            m_animFinished = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
