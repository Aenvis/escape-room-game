using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ComputerMaterialChanger : MonoBehaviour
    {
        [SerializeField] private GameObject secondDisplay;
        [SerializeField] private Material material;
        
        
        public void ChangeMaterial()
        {
            gameObject.GetComponent<Renderer>().material = material;
            secondDisplay.GetComponent<Renderer>().material = material;
        }
    }
}
