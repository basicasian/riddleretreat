using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class NetworkGhostScript : MonoBehaviour, IPunObservable
{
    // clone
    public GameObject head;
    public GameObject body;
    public GameObject leftHand;
    public GameObject rightHand;
    private PhotonView photonView;

    // original xr gameobjct
    private GameObject xrCamera;
    private GameObject xrLeftHand;
    private GameObject xrRightHand;

    public Vector3 startPosition;
    public PlayerStatus status;

    public bool isLocal;
    List<Renderer> rendererList = new List<Renderer>();

    public InputActionReference startReference = null;

    public GameObject gameLogicManager;
    private StartGameLogic startGameLogicScript;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // get xr game objects
        xrCamera = GameObject.Find("XR Origin/Camera Offset/Main Camera");
        xrLeftHand = GameObject.Find("XR Origin/Camera Offset/Left Controller");
        xrRightHand = GameObject.Find("XR Origin/Camera Offset/Right Controller");

        // get the Renderer components
        rendererList.Add(head.GetComponentInChildren<Renderer>());
        rendererList.Add(body.GetComponentInChildren<Renderer>());

        status = PlayerStatus.isWaiting;

        startPosition = transform.position; // should work like this

        gameLogicManager = GameObject.Find("Managers/GameLogic Manager");
        startGameLogicScript = gameLogicManager.GetComponent<StartGameLogic>();

    }

    // Update is called once per frame
    void Update()
    {
        // synchronize xr objects to clones
        if (photonView.IsMine)
        {
            // don't show spawned game objects stuff it's yourself
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            // head.gameObject.SetActive(false);

            isLocal = true;

            MapXRPosition(head, xrCamera);
            MapXRPosition(body, xrCamera);
            MapXRPosition(leftHand, xrLeftHand);
            MapXRPosition(rightHand, xrRightHand);

            if (startGameLogicScript.isReset)
            {
                status = PlayerStatus.hasRestarted;
            }
            if (startGameLogicScript.isPlaying)
            {
                status = PlayerStatus.isReady;
            }
        }
    }

    void MapXRPosition(GameObject target, GameObject gameObject)
    {
        target.transform.position = gameObject.transform.position;

        if (target != body)
        {
            target.transform.rotation = gameObject.transform.rotation;
        }
        else
        {
            target.transform.rotation = Quaternion.identity;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            status = (PlayerStatus)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(status);
        }
    }

    public PlayerStatus GetPlayerStatus()
    {
        return status;
    }

    public void SetPlayerStatus(PlayerStatus value)
    {
        this.status = value;
    }


}