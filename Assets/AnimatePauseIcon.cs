using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatePauseIcon : MonoBehaviour
{

    GameObject[] icons;

    bool startAnim = false;

    float timer = 1f;

    TMP_Text timerText;

    private void Start()
    {
        icons = GameObject.FindGameObjectsWithTag("UI/Pause Icon");
        timerText = GameObject.FindGameObjectWithTag("UI/Pause Timer").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        int counter = (int)Time.time;

        int hours = counter / 3600;
        int minutes = (counter % 3600) / 60;
        int seconds = (counter % 3600) % 60;

        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

        if (timerText == null)
        {
            timerText = GameObject.FindGameObjectWithTag("UI/Pause Timer").GetComponent<TextMeshProUGUI>();
            Debug.Log(timeText);    // Debug.Log(hours + " " + minutes + " " + seconds);
        }
        timerText.text = timeText;

        startAnim = true;
    }

    private void OnDisable()
    {
        startAnim = false;
    }

    private void Update()
    {
        if (startAnim)
        {
            if (timer <= 0f)
            {
                Blink();
                timer = 1f;
            }
            else
            {
                timer -= 0.01f;
            }

        }
    }

    void Blink()
    {
        for(int i =0;i<icons.Length;i++)
        icons[i].SetActive(!icons[i].activeSelf);
        //timerText.gameObject.SetActive(!icon.activeSelf);
        //Debug.Log(icon);
    }
}
