using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsManager : MonoBehaviour
{

    [TextArea]
    public string[] creditsText;

    public static int ind;
    int prevInd = -1;

    public TextMeshProUGUI text;

    private void Start()
    {
        ind = 0;
        prevInd = -1;
    }

    void Update()
    {
        if (prevInd != ind)
        {
            text.text = creditsText[ind % creditsText.Length];
        }
        prevInd = ind;
    }
}
