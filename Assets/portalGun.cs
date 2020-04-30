using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalGun : MonoBehaviour
{

    public Camera fpsCam;
    int range = 100;

    public GameObject BluePortal;
    public GameObject OrangePortal;

    public GameObject RightHand;

    Vector3 RightHandActivePos = new Vector3 (0.22f, 0.11f, -0.18f);
    Vector3 RightHandInactivePos = new Vector3 (0.22f, 0, -0.48f);

    private void OnEnable()
    {
        RightHand.transform.localPosition = RightHandInactivePos;
        //Debug.Log(RightHandInactivePos);
    }

    private void OnDisable()
    {
        //Debug.Log(RightHandActivePos);
        RightHand.transform.localPosition = RightHandActivePos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Blue
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (!hit.transform.CompareTag("Main/Player"))
                {
                    BluePortal.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
                    BluePortal.transform.rotation = Quaternion.LookRotation(hit.normal);
                }
            }
        }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Orange
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    if (!hit.transform.CompareTag("Main/Player"))
                    {
                        OrangePortal.transform.position = new Vector3(hit.point.x - 1f, hit.point.y + 1f, hit.point.z - 1f);
                        OrangePortal.transform.rotation = Quaternion.LookRotation(hit.normal);
                }
            }
            }
    }
}
