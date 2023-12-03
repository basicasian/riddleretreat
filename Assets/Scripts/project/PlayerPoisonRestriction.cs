using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using System;

public class PlayerPoisonRestriction : MonoBehaviour
{
    public Camera mainCamera = null;
    public XROrigin xrOrigin = null;

    public bool isStandingOnPoison = false;
    private Vector3 startPosition;

    public GameObject networkManager;
    private NetworkPlayerSpawner networkPlayerSpawnerScript;

    // Start is called before the first frame update
    void Start()
    {
        networkPlayerSpawnerScript = networkManager.GetComponent<NetworkPlayerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        isStandingOnPoison = checkCollider("Poison", "HelperObject");
    }

    public bool checkCollider(string tag1, string tag2)
    {
        var offset = new Vector3(0, 2, 0);
        var localPoint0 = mainCamera.transform.position - offset;
        var localPoint1 = mainCamera.transform.position + offset;

        var colliders = Physics.OverlapCapsule(localPoint0, localPoint1, 0.1f);

        if (colliders.Length > 0)
        {

            foreach (Collider col in colliders)
            {
                if (col.CompareTag(tag2))
                {
                    return false;
                }
            }

            foreach (Collider col in colliders)
            {
                if (col.CompareTag(tag1))
                {
                    startPosition = networkPlayerSpawnerScript.startPosition;
                    xrOrigin.transform.position = startPosition;
                    networkPlayerSpawnerScript.spawnedHelperPrefab.transform.position = startPosition + new Vector3(0, 0.075f, 3);
                    isStandingOnPoison = true;
                }
            }

        }
        return false;
    }
}
