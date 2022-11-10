using System;
using Project.Systems.GameEvents;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private GameEvent disablePlayerMovement;
        [SerializeField] private GameEvent enablePlayerMovement;
        [SerializeField] private GameObject pauseMenu;
        
        private PlayerActionMaps m_input;
        private bool m_isPaused;

        [Inject]
        private void Injection(PlayerActionMaps playerActionMaps)
        {
            m_input = playerActionMaps;
        }

        private void Start()
        {
            TogglePauseMenu(false);
        }

        private void OnEnable()
        {
            m_input.Enable();
            m_input.Pause.PauseGame.performed += PauseGameOnPerformed;
        }

        private void OnDisable()
        {
            m_input.Pause.PauseGame.performed -= PauseGameOnPerformed;
        }

        private void PauseGameOnPerformed(InputAction.CallbackContext context)
        {
            TogglePauseMenu(!m_isPaused);
        }

        public void ContinueButton() => TogglePauseMenu(false);
        
        public void QuitToMenuButton() => SceneManager.LoadScene("StartMenu");

        private void TogglePauseMenu(bool state)
        {
            m_isPaused = state;
            ToggleCursor(state);
            if (state) disablePlayerMovement.Invoke();
            else enablePlayerMovement.Invoke();
            pauseMenu.SetActive(state);
            Time.timeScale = state ? 0f : 1f;
        }
        
        private static void ToggleCursor(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
        }
    }
}
