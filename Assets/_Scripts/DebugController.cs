using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;

    public static DebugCommand KILL_ALL;

    public List<object> commandList;

    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    private void Awake()
    {
        KILL_ALL = new DebugCommand("kill_all", "Removes all heroes from the scene.", "kill_all", () =>
        {
            // Controller.instance.KillAllHeros();
        });
    }

    private void OnGUI()
    {
        if (!showConsole) {return; }

        float y = 0f;
        
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }
}
