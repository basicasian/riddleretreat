using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private GameObject spawnedHelperPrefab;
    private GameObject spawnedWall;

    public GameObject xrOrigin;
    public GameObject lobby;

    public GameObject exitButtonWrist;
    public GameObject connectButtonWrist;
    public GameObject disconnectButtonWrist;
    public GameObject resetButtonWrist;

    private GameLogic gameLogic;
    public Vector3 playerPosition = new Vector3(3, 0, -5);
    public Vector3 helperPosition = new Vector3(3, 0, -2);
    public Vector3 wallPosition = new Vector3(2.5f, 1, 4.5f);

    public void Start()
    {

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (PhotonNetwork.IsMasterClient)
        {
            resetButtonWrist.SetActive(true);
        }

        connectButtonWrist.SetActive(false);
        disconnectButtonWrist.SetActive(true);
        lobby.SetActive(true);

        // player position
        // depends on sign assigned left or right
        Vector3 sign = new Vector3(1, 1, 1);
        if (PhotonNetwork.PlayerList.Length % 2 != 0) {
            sign = new Vector3(-1, 1, 1);
        }

        xrOrigin.transform.position = Vector3.Scale(playerPosition, sign);
        // player
        playerPosition = Vector3.Scale(playerPosition, sign);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/Network Player", playerPosition, Quaternion.identity);

        // helperObject
        helperPosition = Vector3.Scale(helperPosition, sign);
        spawnedHelperPrefab = PhotonNetwork.Instantiate("Prefabs/Project/HelperObject", helperPosition, Quaternion.identity);

        // helperObject
        wallPosition = Vector3.Scale(wallPosition, sign);
        spawnedWall = PhotonNetwork.Instantiate("Prefabs/Project/ButtonWall", wallPosition, Quaternion.identity);

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        if (spawnedPlayerPrefab != null)
        {
            PhotonNetwork.Destroy(spawnedPlayerPrefab);
        }
        if (spawnedHelperPrefab != null)
        {
            PhotonNetwork.Destroy(spawnedHelperPrefab);
        }
        if (spawnedWall != null)
        {
            PhotonNetwork.Destroy(spawnedWall);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            resetButtonWrist.SetActive(true);
        }

        connectButtonWrist.SetActive(true);
        disconnectButtonWrist.SetActive(false);
        lobby.SetActive(false);
    }

    public GameObject GetHelperObject()
    {
        return spawnedHelperPrefab;
    }
}
