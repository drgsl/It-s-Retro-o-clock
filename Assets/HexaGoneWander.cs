using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class HexaGoneWander : MonoBehaviour
{
	public float speed = 5;
	public float directionChangeInterval = 1;

	CharacterController controller;
	float heading;

	int minZ = -80;
	int maxZ = 69;

	int minX = -85;
	int maxX = 80;
	

	void Awake()
	{
		controller = GetComponent<CharacterController>();

		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);

		InvokeRepeating("NewHeadingRoutine", 0f, directionChangeInterval);

		RandomSpawn();
	}

	void Update()
	{
		transform.eulerAngles = new Vector3(0, heading, 0);
		var forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * speed);

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
		{
			if (hit.transform.CompareTag("Hexagone/Wall"))
			{
				heading = 360 - heading;
			}
		}
	}

	void NewHeadingRoutine()
	{
		heading = Random.Range(0, 360);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag("Hexagone/Tile"))
		{
			hit.gameObject.GetComponent<TileScript>().hasBeenTouched = true;
		}
		if (hit.gameObject.CompareTag("Hexagone/BottomLayer"))
		{
			RandomSpawn();
		}
	}


	void RandomSpawn()
	{
		float x = Random.Range(minX, maxX);
		float z = Random.Range(minZ, maxZ);

		transform.position = new Vector3(x, 30, z);
	}
}