using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] PlayerMovement PM;

    private void Start()
    {
        if (PM != null)
            PM = PM.GetComponent<PlayerMovement>();
    }
    public void Testing()
    {
        Debug.Log("Button Pushed");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CloseMainMenu()
    {
        PM.ClosePause();
    }
}
