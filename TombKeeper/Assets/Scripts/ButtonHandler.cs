using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public GameObject appearText;
    public MenuEscape menuEscape;
    
    public void OnClickResume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        menuEscape.optionsMenu.gameObject.SetActive(false);
    }

    public void OnClickPlay()
    {
        appearText.SetActive(true);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
    }

    public void OnClickAppearClose()
    {
        appearText.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("MainGame");
    }
}
