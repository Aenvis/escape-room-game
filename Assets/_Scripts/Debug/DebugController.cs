using System.Collections.Generic;
using Project.Systems.GameEvents;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace Project.Debug
{
    public class DebugController : MonoBehaviour
    {
        [SerializeField] private GameEvent enablePlayerMovement;
        [SerializeField] private GameEvent disablePlayerMovement;
        
        private static DebugCommand<int> SPEED_UP;
        private static DebugCommand HELP;

        private bool m_showConsole;
        private bool m_showHelp;
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

        private Vector2 m_scroll;
        private void OnGUI()
        {
            if (!m_showConsole) return;

            float y = 0f;
            
            if (m_showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");
                Rect viewPort = new Rect(0, 0, Screen.width - 30, 20 * CommandList.Count);
                m_scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), m_scroll, viewPort);

                for (int i = 0; i < CommandList.Count; i++)
                {
                    DebugCommandBase cmd = CommandList[i] as DebugCommandBase;
                    string label = $"{cmd.CommandFormat} -  {cmd.CommandDescription}";
                    Rect labelRect = new Rect(5, 20 * i, viewPort.width - 100, 20);
                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();
                
                y += 100;
            }
            
        
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            m_input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 100f), m_input);
        }

        private void OnToggleDebug(InputAction.CallbackContext context)
        {
        m_showConsole = !m_showConsole;
        if (m_showConsole) disablePlayerMovement.Invoke();
        else enablePlayerMovement.Invoke();
        }

        private void OnEnterPressed(InputAction.CallbackContext context)
        {
            if (!m_showConsole) return;
            HandleInput();
            m_input = "";
        }
        
        //TODO: Refactor this so it uses GameEvent instead of Action (easy)  
        private void InitCommands()
        {
            SPEED_UP = new DebugCommand<int>("speed_up", "Speeds up the character.", "speed_up", (val) =>
            {
                UnityEngine.Debug.Log(val);
            });
            CommandList.Add(SPEED_UP);

            HELP = new DebugCommand("help", "Shows all available commands.", "help", () =>
            {
                m_showHelp = true;
            });
            CommandList.Add(HELP);
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
