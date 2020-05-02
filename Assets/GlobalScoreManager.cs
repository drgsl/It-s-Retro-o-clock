using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScoreManager : MonoBehaviour
{
    public static float Tetris = -1f;
    public static float BrickBreaker = -1f;
    public static float Pong = -1f;
    public static float PacMan = -1f;
    public static float Snake = -1f;
    public static float Tron = -1f;

    public static bool FloorCompleted(int FloorInd = 1)
    {
            return Tetris >= 0 && 
                BrickBreaker >= 0 && 
                Pong >= 0 && 
                PacMan >= 0 && 
                Snake >= 0 &&
                Tron >= 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Tetris++;
            BrickBreaker++;
            Pong++;
            PacMan++;
            Snake++;
            Tron++;
        }
    }
}
