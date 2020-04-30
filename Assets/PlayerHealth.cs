using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    const int MaxHealth = 100;
    public static int Health = MaxHealth;

    public Slider slider;
    public static PlayerHealth instance;

    GameObject player;
    public static Vector3 RespawnPos = new Vector3(30f, 3f, -60f); 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        slider.maxValue = MaxHealth;
        updateHealth();

        player = GameObject.FindGameObjectWithTag("Main/Player");
    }

    public static void updateHealth()
    {
        instance.slider.value = Health;
        if (Health <= 0)
        {
            Debug.Log("Respawn");
            //respawn
            instance.player.transform.position = RespawnPos;
            Health = MaxHealth;
            updateHealth();
        }
    }
}
