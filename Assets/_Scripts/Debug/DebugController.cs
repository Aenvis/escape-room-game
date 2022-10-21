using System;
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
        //here serialize all Game Events to be invoked with commands
        [SerializeField] private GameEvent enablePlayerMovement;
        [SerializeField] private GameEvent disablePlayerMovement;
        [SerializeField] private GameEvent speedUp;
        
        private static DebugCommand<int> s_SPEED_UP;
        private static DebugCommand s_HELP;

        private bool m_showConsole;
        private bool m_showHelp;
        private string m_input;
        private PlayerActionMaps m_playerActionMaps;

        private List<object> m_commandList = new List<object>();

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
            
            //display all commands if help command was used
            if (m_showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");
                Rect viewPort = new Rect(0, 0, Screen.width - 30, 20 * m_commandList.Count);
                m_scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), m_scroll, viewPort);

                for (int i = 0; i < m_commandList.Count; i++)
                {
                    DebugCommandBase cmd = m_commandList[i] as DebugCommandBase;
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
        ToggleCursor(m_showConsole);
        if (m_showConsole) disablePlayerMovement.Invoke();
        else enablePlayerMovement.Invoke();
        }

        private void ToggleCursor(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
        }

        private void OnEnterPressed(InputAction.CallbackContext context)
        {
            if (!m_showConsole) return;
            HandleInput();
            m_input = "";
        }
        
        private void InitCommands()
        {
            s_SPEED_UP = new DebugCommand<int>("speed_up", "Speeds up the character.", "speed_up <value>", (val) =>
            {
                speedUp.InvokeWithIntParam(val);
            });
            s_HELP = new DebugCommand("help", "Shows all available commands.", "help", () =>
            {
                m_showHelp = true;
            });
            
            //TODO: automize insertion of commands so we don't have to do it manually :(
            m_commandList.Add(s_HELP);
            m_commandList.Add(s_SPEED_UP);
        }
        
        private void HandleInput()
        {
            string[] properties = new string[2];
            properties = m_input.Split(' ');
            for (int i = 0; i < m_commandList.Count; i++)
            {
                DebugCommandBase commandBase = m_commandList[i] as DebugCommandBase;
                if (commandBase is null || !m_input.Contains(commandBase.CommandId)) continue;
                
                if (m_commandList[i] is DebugCommand) (m_commandList[i] as DebugCommand)?.Invoke();
                else if (m_commandList[i] is DebugCommand<int>)
                {
                    if (String.IsNullOrEmpty(properties[1]) || String.IsNullOrWhiteSpace(properties[1])) properties[1] = "0"; //TODO: it still throws an error (out of bounds), try to fix it?
                    (m_commandList[i] as DebugCommand<int>)?.Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
