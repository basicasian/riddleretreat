using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    public GameObject spawnedHelperPrefab;
    public GameObject spawnedWall;

    public GameObject xrOrigin;
    private GameLogic gameLogic;
    public Vector3 playerPosition = new Vector3(3, 0, -5);
    public Vector3 helperPosition = new Vector3(3, 0, -2);
    public Vector3 wallPosition = new Vector3(-2.5f, 1, -4.5f);


    public void Start()
    {
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

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
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        PhotonNetwork.Destroy(spawnedHelperPrefab);
        PhotonNetwork.Destroy(spawnedWall);
    }
}
