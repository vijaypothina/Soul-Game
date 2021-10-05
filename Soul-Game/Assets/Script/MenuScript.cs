using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNewGame()
    {
        Debug.Log(" Loading New Game! ");
    }

    public void loadSavedGame()
    {
        Debug.Log(" Loading Saved Game! Hurray =) ");
    }

    public void loadOptionsMenu()
    {
        Debug.Log(" Loading Options Menu ! ");
    }

    public void loadCreditsMenu()
    {
        Debug.Log(" Loading Credits Menu !");
    }
}
