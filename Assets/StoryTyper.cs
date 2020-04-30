using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StoryTyper : MonoBehaviour
{

	[System.Serializable]
	public struct Dialogue
	{
		[TextArea(0, 1)]
		public string Character;

		[TextArea(1, 5)]
		public string Text;
	}

	//public Image loadingScreen;
	//public Sprite[] windowsAnim;
	//float windowsInd = 0;

	public GameObject animGO;

	public TextMeshProUGUI CharacterTxt;
	public TextMeshProUGUI txt;
	public TextMeshProUGUI nextButtonTxt;

	string ButtonText = "Press G to continue";
	int buttonInd;

	public Dialogue[] dialogues;

	Dialogue active;

	int dialogueInd;
	int charInd;

	int phase = 0;

	int actInd = 0;
	void Start()
	{
		//txt.text = "";
		CharacterTxt.text = "";
		active = dialogues[actInd++];
		InvokeRepeating("WriteText", 5f, 0.01f); //StartCoroutine("PlayText");

		//GameObject.FindGameObjectWithTag("UI/WinningScreen").SetActive(false);
		LevelMenu.DeactivateWinning();
		Cursor.lockState = CursorLockMode.Locked; //Debug.Log(Cursor.lockState);
		Cursor.visible = false;

		animGO.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			if (actInd >= dialogues.Length - 1)
			{
				ButtonText = "Press G to start the game";
				//Debug.Log("change button txt from next dialogue to load lvl");
			}
			if (actInd >= dialogues.Length)
			{
				//loadingScreen.color = Color.white;
				animGO.SetActive(true);
				SceneManager.LoadSceneAsync("City");
				//StartCoroutine(LoadAsynchronously("city"));//SceneManager.LoadScene("City");
			}

			active = dialogues[actInd++];

			if (actInd == 6)
			{
				phase = 1;
			}
			
			txt.text = "";
			CharacterTxt.text = "";
			nextButtonTxt.text = "";

			dialogueInd = 0;
			charInd = 0;
			buttonInd = 0;
		}
	}

	void WriteText()
	{
		if (phase == 0)
		{
			txt.text = "";
			phase = -1;
		}
		if (phase >= 1)
		{
			if (charInd < active.Character.Length)
			{
				CharacterTxt.text += active.Character[charInd++];
			}
		}

		if (dialogueInd < active.Text.Length)
		{
			txt.text += active.Text[dialogueInd];
			dialogueInd++;
		}
		else
		{
			if (buttonInd < ButtonText.Length)
				nextButtonTxt.text += ButtonText[buttonInd++];
		}
	}

	//IEnumerator LoadAsynchronously(string sceneName)
	//{
	//	AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
	//	while (!operation.isDone)
	//	{
	//		//windowsInd = operation.progress * 120f;
	//		//windowsInd = windowsAnim.Length / progress;
	//		//Debug.Log(windowsInd);
	//		//loadingScreen.sprite = windowsAnim[(int)windowsInd];
	//		//loadingText.text = "" + progress;
	//		yield return null;
	//	}
	//}


}
