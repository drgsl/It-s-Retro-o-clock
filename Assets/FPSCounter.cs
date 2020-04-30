using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class FPSCounter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fpsText;

    bool enabl = true;

    private void Start()
    {
        if (!fpsText)
        {
            fpsText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (SceneManager.GetActiveScene().name == "Story")
        {
            enabl = false;
        }

            InvokeRepeating("showFps", 0f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            enabl = !enabl;
        }
    }

    void showFps()
    {
        if (enabl)
            fpsText.text = " FPS: " + (int)(1f / Time.unscaledDeltaTime);
        else
            fpsText.text = "";
    }
}
