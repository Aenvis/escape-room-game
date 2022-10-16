using System.Collections.Generic;
using Project.Systems.GameEvents;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Debug
{
    public class DebugController : MonoBehaviour
    {
        [SerializeField] private GameEvent EnablePlayerMovement;
        [SerializeField] private GameEvent DisablePlayerMovement;
        
        private static DebugCommand<int> SPEED_UP;

        private bool m_showConsole;
        private string m_input;
        private PlayerActionMaps m_playerActionMaps;

        public List<object> CommandList = new List<object>();

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
            m_playerActionMaps.Console.Enter.performed += OnEnterPressed;
        }
        private void OnDisable()
        {
            m_playerActionMaps.Console.Disable();
            m_playerActionMaps.Console.ToggleDebug.performed -= OnToggleDebug;
            m_playerActionMaps.Console.Enter.performed -= OnEnterPressed;
        }
    
        private void OnGUI()
        {
            if (!m_showConsole) return;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            float y = 0f;
        
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            m_input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 100f), m_input);
        }

        private void OnToggleDebug(InputAction.CallbackContext context)
        {
        m_showConsole = !m_showConsole;
        if (m_showConsole) DisablePlayerMovement.Invoke();
        else EnablePlayerMovement.Invoke();
        }

        private void OnEnterPressed(InputAction.CallbackContext context)
        {
            if (!m_showConsole) return;
            HandleInput();
            m_input = "";
        }
        
        private void InitCommands()
        {
            SPEED_UP = new DebugCommand<int>("speed_up", "Speeds up the character.", "speed_up", (val) =>
            {
                UnityEngine.Debug.Log(val);
            });
            CommandList.Add(SPEED_UP);
        }

        private void HandleInput()
        {
            string[] properties = m_input.Split(' ');
            for (int i = 0; i < CommandList.Count; i++)
            {
                DebugCommandBase commandBase = CommandList[i] as DebugCommandBase;
                (CommandList[i] as DebugCommand)?.Invoke();
                (CommandList[i] as DebugCommand<int>)?.Invoke(int.Parse(properties[1]));
            }
        }
    }
}
