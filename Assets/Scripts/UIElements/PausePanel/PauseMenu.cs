using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void CloseMenu()
    {
        Main.pause.Unpause();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
}
