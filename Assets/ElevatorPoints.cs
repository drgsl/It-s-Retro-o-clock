using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPoints : MonoBehaviour
{
    [System.Serializable]
    public struct Level
    {
        public string Name;
        public AudioClip Music;
        public float MusicVolume;
        public AudioClip SFX;
        public float SFXVolume;
        public GameObject go;

    }

    public Level[] Levels;

    public GameObject Hands;

    public GameObject AK47;
    public GameObject HealthSlider;

    public GameObject PortalGun;

    public Transform StartPos;

    public static bool canStartPortal;

    CharacterController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Main/Player").GetComponent<CharacterController>();
    }

    public void SwitchToLevel(int lvl = 0)
    {
        if (lvl == 0)
        {
            AK47.SetActive(false);
            Hands.SetActive(true);
            HealthSlider.SetActive(false);
            //deactivate PortalGun
            PortalGun.SetActive(false);

            Levels[0].go.SetActive(true);
            Levels[1].go.SetActive(false);
            Levels[2].go.SetActive(false);

            //Stop spawning Enemies
            CounterStrikeSpawner.Spawning = false;

            SoundManager.UpdateMusic(Levels[0].Music, Levels[0].MusicVolume);
            SoundManager.UpdateSFX(Levels[0].SFX, Levels[0].SFXVolume);

            Debug.Log("Level 0 Loaded");
        }
        else if (lvl == 1)
        {
            AK47.SetActive(true);
            Hands.SetActive(false);
            HealthSlider.SetActive(true);
            //deactivate PortalGun
            PortalGun.SetActive(false);

            Levels[0].go.SetActive(false);
            Levels[1].go.SetActive(true);
            Levels[2].go.SetActive(false);

            //Start spawning Enemies
            CounterStrikeSpawner.Spawning = true;

            SoundManager.UpdateMusic(Levels[1].Music, Levels[1].MusicVolume);
            SoundManager.UpdateSFX(Levels[1].SFX, Levels[1].SFXVolume);

            Debug.Log("Level 1 Loaded");
        }
        else if (lvl == 2)
        {
            AK47.SetActive(false);
            Hands.SetActive(true);
            HealthSlider.SetActive(false);
            //activate PortalGun
            PortalGun.SetActive(true);

            //hide City
            Levels[0].go.SetActive(false);
            Levels[1].go.SetActive(false);
            Levels[2].go.SetActive(true);

            teleportToPortalLvl();

            //Stop spawning Enemies
            CounterStrikeSpawner.Spawning = false;


            SoundManager.UpdateMusic(Levels[2].Music, Levels[2].MusicVolume);
            SoundManager.UpdateSFX(Levels[2].SFX, Levels[2].SFXVolume);

            Debug.Log("Level 2 Loaded");
        }

    }

    void teleportToPortalLvl()
    {
        player.enabled = false;
        player.gameObject.transform.position = StartPos.position;
        player.enabled = true;
    }

    private void Update()
    {
        if (canStartPortal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchToLevel(2);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Main/Player") && PortalStartPos != null)
    //    {
    //        CharacterController cc = other.GetComponent<CharacterController>();
    //        cc.enabled = false;
    //        cc.transform.position = PortalStartPos.position;
    //        cc.enabled = true;
    //    }
    //}
}
