using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Systems.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private float playerDistance;
        
        private Transform m_playerTransform;
        private PlayerActionMaps m_playerInput;
        
        private bool m_canInteract = true;
        

        [Inject]
        private void Inject(PlayerActionMaps playerActionMaps)
        {
            m_playerInput = playerActionMaps;
        }

        protected virtual void OnEnable()
        {
            m_playerInput.Interactions.Enable(); //FIXME: its being enabled many times in different places 
            m_playerInput.Interactions.Interact.performed += OnInteractButton;
        }
        
        protected virtual void Start()
        {
            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        protected virtual void OnDisable()
        {   //we dont want to disable action map after one object is deleted - it cuts all the interactions
            m_playerInput.Interactions.Interact.performed -= OnInteractButton;
        }

        private void OnMouseOver()
        {
            m_canInteract = true;
        }

        private void OnMouseExit()
        {
            m_canInteract = false;
        }
        
        protected virtual void OnInteractButton(InputAction.CallbackContext context)
        {
            if(!m_canInteract || Vector3.Distance(transform.position, m_playerTransform.position) > playerDistance) return;
            
            Interaction();
        }
        
        protected abstract void Interaction();
    }
}