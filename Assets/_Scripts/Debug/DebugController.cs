using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Debug
{
    public class DebugController : MonoBehaviour
    {
        public static DebugCommand KILL_ALL;

        private bool m_showConsole;
        private string m_input;
        private PlayerActionMaps m_playerActionMaps;

        public List<object> CommandList;

        [Inject]
        private void Injection(PlayerActionMaps playerInput)
        {
            m_playerActionMaps = playerInput;
        }
        
        private void Awake()
        {
            InitCommands();
        }

        private void OnEnable()
        {
            m_playerActionMaps.Console.Enable();
            m_playerActionMaps.Console.ToggleDebug.performed += OnToggleDebug;
        }
        private void OnDisable()
        {
            m_playerActionMaps.Console.Disable();
            m_playerActionMaps.Console.ToggleDebug.performed -= OnToggleDebug;
        }
    
        private void OnGUI()
        {
            if (!m_showConsole) return;

            Cursor.visible = true;
            float y = 0f;
        
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            m_input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 100f), m_input);
        }

        private void OnToggleDebug(InputAction.CallbackContext context)
        {
        m_showConsole = !m_showConsole;
        //if(m_showConsole) m_playerActionMaps.
        }
        private void InitCommands()
        {
            KILL_ALL = new DebugCommand("kill_all", "Removes all heroes from the scene.", "kill_all", () =>
            {
                UnityEngine.Debug.Log("testing");
            });
        }
    }
}
