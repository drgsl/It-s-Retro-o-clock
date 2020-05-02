using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static Animator anim;

    public static LevelLoader instance;

    public static GameObject player;

    public static Vector3 lastPlayerPos = new Vector3(-34, 15, -49);

    private void Start()
    {
        anim = GetComponent<Animator>();
        instance = this;
    }

    public static void LoadScene(string SceneName, float TransitionTime = 1f)
    {
        player = GameObject.FindGameObjectWithTag("Main/Player");

        if (player)
        {
            lastPlayerPos = player.transform.position;
            Debug.Log(lastPlayerPos);
        }

        instance.StartCoroutine(instance.LoadAnim(SceneName, TransitionTime));
    }

    IEnumerator LoadAnim(string SceneName, float TransitionTime)
    {
        LevelLoader.anim.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(SceneName);
    }

    public void SoundUpdate()
    {
        SoundManager.UpdateSound();
    }

    public void MusicStop()
    {
        SoundManager.StopMusic();
    }
}
