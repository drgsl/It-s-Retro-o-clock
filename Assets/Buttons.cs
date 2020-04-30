using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Buttons : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingsMenu;

    public GameObject QuitPopup;

    public Animator camAnim;

    private void Start()
    {
        camAnim = Camera.main.GetComponent<Animator>();
    }

    public void StartGame()
    {
        SoundManager.StopMusic();

        SceneManager.LoadScene("Story");
        // story
        Cursor.visible = false;
    }


    public void OpenSettings()
    {
        camAnim.enabled = true;
        camAnim.SetBool("Settings", true);
    }

    public void CloseSettings()
    {
        camAnim.enabled = true;
        camAnim.SetBool("Settings", false);
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
        //UnityEditor.EditorApplication.isPlaying = false;
    }


    //Settings Buttons

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int ind)
    {
        QualitySettings.SetQualityLevel(ind);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}
