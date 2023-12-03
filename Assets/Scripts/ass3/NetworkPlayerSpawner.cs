using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    public GameObject spawnedHelperPrefab;

    public GameObject xrOrigin;
    private GameLogic gameLogic;
    public Vector3 startPosition = new Vector3(3, 0, -5);


    public void Start()
    {
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // track
        //Vector3 distance = new Vector3((PhotonNetwork.PlayerList.Length -1) * 10, 0, 0);

        // player position
        if (PhotonNetwork.PlayerList.Length % 2 == 0) {
            startPosition = new Vector3(3, 0, -5);
        } 

        xrOrigin.transform.position = startPosition;
        // player
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/Network Player", startPosition, Quaternion.identity);

        // helperObject
        spawnedHelperPrefab = PhotonNetwork.Instantiate("Prefabs/Project/HelperObject", (startPosition + new Vector3(0, 0.075f, 3)), Quaternion.identity);

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        PhotonNetwork.Destroy(spawnedHelperPrefab);
    }
}
