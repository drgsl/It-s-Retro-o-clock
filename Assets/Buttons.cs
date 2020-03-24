using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingsMenu;

    public GameObject QuitPopup; 

    public void StartGame()
    {
        SceneManager.LoadScene("City");
        // story
        Cursor.visible = !Cursor.visible;
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OpenQuitPopup()
    {
        QuitPopup.SetActive(true);
    }

    public void CloseQuitPopup()
    {
        QuitPopup.SetActive(false);
    }

    public void QuitGame()
    {
        //QuitPopup.SetActive(false);
        //close tv anim
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
