using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Animator pausePanel;

    private float prePauseTimescale;

    public void OpenPauseMenu()
    {
        if (Time.timeScale != 0f)
        {
            prePauseTimescale = Time.timeScale;

            Time.timeScale = 0f;
            
            pausePanel.gameObject.SetActive(true);

            pausePanel.Play("PausePanelOpen"); 
        }
    }

    public void ClosePauseMenu()
    {
        Debug.Log("BLUATT SUKA YIA EBAL");

        pausePanel.Play("PausePanelClose");
    }

    public void Unpause()
    {
        pausePanel.gameObject.SetActive(false);
    
        Time.timeScale = prePauseTimescale;
    }
}
