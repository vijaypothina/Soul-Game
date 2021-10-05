using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    public bool pauseCanvas;
    public GameObject pauseUICanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseCanvas == false)
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        Debug.Log(" The Game will unpause and resume! ");
        if (pauseCanvas == true)
        {
            pauseUICanvas.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            pauseCanvas = false;
        }
    }

    public void optionsCanvas()
    {
        Debug.Log(" The Game will bring up the options Canvas! ");
    }

    public void exitGame()
    {
        Debug.Log(" The Game will exit to the main menu! ");
    }

    public void pauseGame()
    {
        Debug.Log(" The Game is currently Paused ! ");
        if (pauseCanvas == false)
        {
            pauseCanvas = true;
            pauseUICanvas.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
