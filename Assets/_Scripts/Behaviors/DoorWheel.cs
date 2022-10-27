using System;
using Project.Systems.GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Behaviors
{
    public class DoorWheel : MonoBehaviour
    {
        [SerializeField] private GameEvent openDoor;
        [SerializeField] private GameEvent closeDoor;
    }
}