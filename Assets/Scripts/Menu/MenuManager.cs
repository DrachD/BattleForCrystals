using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// the control menu is needed in order to set the necessary call events to the buttons
public class MenuManager : MonoBehaviour
{
    // Singleton
    private static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int value)
    {
        SceneManager.LoadScene(value, LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
