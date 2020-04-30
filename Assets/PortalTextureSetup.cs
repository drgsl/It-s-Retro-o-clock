using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{

    public Camera cameraB;
    public Camera cameraA;

    public Material cameraMatB;
    public Material cameraMatA;

    // Start is called before the first frame update
    void Start()
    {
		if (cameraA.targetTexture != null)
		{
			cameraA.targetTexture.Release();
		}
		cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24)
		{
			antiAliasing = 8
		};
		cameraMatA.mainTexture = cameraA.targetTexture;

		if (cameraB.targetTexture != null)
		{
			cameraB.targetTexture.Release();
		}
		cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24)
		{
			antiAliasing = 8
		};
		cameraMatB.mainTexture = cameraB.targetTexture;
	}
}
