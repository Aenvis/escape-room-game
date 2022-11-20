using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.Interactable
{
    public class MachineMaterialChanger : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private GameObject redButton;
        [SerializeField] private GameObject greenButton;
        [Header("Materials")]
        [SerializeField] private Material displayMaterial;
        [SerializeField] private Material redButtonMaterial;
        [SerializeField] private Material greenButtonMaterial;

        public void ChangeMaterial()
        {
            //display
            gameObject.GetComponent<Renderer>().material = displayMaterial;
            redButton.GetComponent<Renderer>().material = redButtonMaterial;
            greenButton.GetComponent<Renderer>().material = greenButtonMaterial;
        }
    }
}
