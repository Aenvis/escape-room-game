using Project.Systems.GameEvents;
using UnityEngine;


namespace Project.Systems.Interactable
{
    public class DoorWheel : MonoBehaviour
    {
        [SerializeField] private GameEvent openDoor;
        [SerializeField] private GameEvent closeDoor;
    }
}