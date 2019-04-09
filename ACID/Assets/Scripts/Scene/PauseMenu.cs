using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public SceneLoader sceneLoader;
    public int menuScene;
    public GameObject pauseMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //lock the cursor and hide it
        Unpause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                //pause the game and unlock the cursor
                paused = true;
                Pause();
            }
            else
            {
                paused = false;
                Unpause();
            }
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuCanvas.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuCanvas.SetActive(true);
    }

    public void Quit()
    {
        sceneLoader.LoadScene(menuScene);
    }
}
