using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBackground : MonoBehaviour
{

    public Image PauseImage;
    public Sprite[] sprites;

    float timer = 1f;
    int ind = 0;

    void Update()
    {
        if (timer <= 0f)
        {
            PauseImage.sprite = sprites[ind];
            ind++;
            ind %= sprites.Length;
            timer = 1f;
        }
        else
        {
            timer -= 0.07f;
        }
    }
}
