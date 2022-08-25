using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject EndGamePanel;
    private void Awake()
    {
        instance = this;
    }

    public void WinPanel()
    {
        EndGamePanel.SetActive(true);
        EndGamePanel.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void LosePanel()
    {
        EndGamePanel.SetActive(true);
        EndGamePanel.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
