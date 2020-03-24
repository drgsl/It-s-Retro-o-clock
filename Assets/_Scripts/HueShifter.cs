using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueShifter : MonoBehaviour
{

    public Material material;

    public Material bkgr;

    float timeLeft;
    Color targetColor = new Color(1f,1f,1f);

    float maxTime = 10.0f;

    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            material.color = targetColor;
            if (bkgr != null)
                bkgr.color = new Color(1.0f - targetColor.r, 1.0f - targetColor.g, 1.0f - targetColor.b);
            // start a new transition
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = maxTime;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            material.color = Color.Lerp(material.color, targetColor, Time.deltaTime / timeLeft);
            if (bkgr != null)
                bkgr.color = new Color(1.0f - material.color.r, 1.0f - material.color.g, 1.0f - material.color.b);

            // update the timer
            timeLeft -= Time.deltaTime;
        }
    }

}
