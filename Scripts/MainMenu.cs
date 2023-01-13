using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Lancement du jeu
    public void Begin()
    {
        SceneManager.LoadScene("Level");
    }

    //Quitter l'appli
    public void Quit() {
        Application.Quit();
    }
}
