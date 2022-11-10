using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Systems.Interactable
{
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if(!collider.CompareTag("Player")) return;
            
            SceneManager.LoadScene("Credits");
        }
        
    }
}
