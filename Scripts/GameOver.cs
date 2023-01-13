using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Recommencer
    public void Retry()
    {
        SceneManager.LoadScene("Level");
    }

    //Retour au menu
    public void Return()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
