using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

    public Transform player;
    Transform teleportableObj;
    public Transform receiver;

    bool playerIsOverlapping = false;
    bool objectIsOverlapping = false;

    static bool canTeleport = true;



    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping && canTeleport)
        {
            StartCoroutine(Teleport(player));
        }

        if (objectIsOverlapping && canTeleport && teleportableObj)
        {
            StartCoroutine(Teleport(teleportableObj));
        }
    }

    IEnumerator Teleport(Transform target)
    {
        {
            Vector3 portalToPlayer = target.position - transform.position;
            //float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            // If this is true: The player has moved across the portal
            //if (Vector3.Dot(transform.up, portalToPlayer) < 0f) {
            // Teleport him!
            float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
            rotationDiff += 180;
            target.Rotate(Vector3.up, rotationDiff);

            Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

            target.position = receiver.position + positionOffset;

            if (objectIsOverlapping && false)
            {
                Vector3 unitVector = Random.onUnitSphere;
                unitVector = Vector3.Scale(unitVector, unitVector);
                teleportableObj.GetComponent<Rigidbody>().AddForce(unitVector * 25f, ForceMode.Impulse);
            }
            playerIsOverlapping = false;
            objectIsOverlapping = false;
        }
        Debug.Log("Teleported");

        canTeleport = false;
        yield return new WaitForSeconds(1f);
        canTeleport = true;

        if (objectIsOverlapping && teleportableObj.position.y < transform.position.y - 2f)
        {
            teleportableObj.position = new Vector3(teleportableObj.position.x, transform.position.y + 3, teleportableObj.position.z);
            teleportableObj.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 5f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main/Player"))
        {
            playerIsOverlapping = true;
        }
        if (other.CompareTag("Portal/Teleportable"))
        {
            Debug.Log("Object Touched");
            objectIsOverlapping = true;
            teleportableObj = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Main/Player"))
        {
            playerIsOverlapping = false;
        }

        if (other.CompareTag("Portal/Teleportable"))
        {
            Debug.Log("Object untouched");
            objectIsOverlapping = false;
        }
    }
}
