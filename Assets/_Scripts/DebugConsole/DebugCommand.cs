using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.DebugConsole
{
    public class DebugCommand : DebugCommandBase
    {
        public Action Command;
    
        public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
        {
            Command = command;
        }

        public void Invoke()
        {
            Command.Invoke();
        }
    }
    public class DebugCommand<T>: DebugCommandBase
    {
        public Action<T> Command;
    
        public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description, format)
        {
            Command = command;
        }

        public void Invoke(T value)
        {
            Command.Invoke(value);
        }
    }
}