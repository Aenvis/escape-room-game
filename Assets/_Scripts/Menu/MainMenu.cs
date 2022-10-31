using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene("TestBartek");
   }
   
   public void GoToStarMenu()
   {
      SceneManager.LoadScene("StartMenu");
   }

   public void LoadCustomScene(string sceneName)
   {
      SceneManager.LoadScene(name);
   }
   
   public void GoToSettingMenu()
   {
      SceneManager.LoadScene("SettingMenu");
   }

   public void GoToMainMenu()
   {
      SceneManager.LoadScene("MainMenu");
   }

   public void GoToCredis()
   {
      SceneManager.LoadScene("Credis");
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   public void OpenURL(string link)
   {
      Application.OpenURL(link);
   }
   
}

