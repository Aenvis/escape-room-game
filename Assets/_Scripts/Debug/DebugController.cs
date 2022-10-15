using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Debug
{
    public class DebugController : MonoBehaviour
    {
        public static DebugCommand KILL_ALL;
    
        private bool m_showConsole = false;
        private string m_input;
        private PlayerInputActions m_playerInputActions;

        public List<object> commandList;
    
        private void Awake()
        {
            InitCommands();
            m_playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            m_playerInputActions.Console.Enable();
            m_playerInputActions.Console.ToggleDebug.performed += OnToggleDebug;
        }
        private void OnDisable()
        {
            m_playerInputActions.Console.Disable();
            m_playerInputActions.Console.ToggleDebug.performed -= OnToggleDebug;
        }
    
        private void OnGUI()
        {
            if (!m_showConsole) return;

            float y = 0f;
        
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            m_input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), m_input);
        }
    
        private void OnToggleDebug(InputAction.CallbackContext context) => m_showConsole = !m_showConsole;
    
        private void InitCommands()
        {
            KILL_ALL = new DebugCommand("kill_all", "Removes all heroes from the scene.", "kill_all", () =>
            {
                UnityEngine.Debug.Log("testing");
            });
        }
    }
}
