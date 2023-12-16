using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;


public class StartGameLogic : MonoBehaviour, IPunObservable
{
    public bool isPlaying = false;
    public bool isReset = false;
    public bool localisReset = false;

    private GameObject[] startButtons;
    private GameObject[] buttonWalls;

    // for visibility
    public GameObject lobby;
    public GameObject gameTable;
    public GameObject player2Uis;
    public GameObject winScreen;
    public Game1UiRenderer game1UiRenderer;

    // for haptic feedback
    public GameObject leftController;
    public GameObject rightController;

    // for reset
    public XROrigin xrOrigin = null;
    public GameObject networkManager;
    private NetworkPlayerSpawner networkPlayerSpawnerScript;
    public GameObject checkerPlate;
    private ObjectChecker objectCheckerScript;
    public GameObject walls;
    public GameObject teleportArea;

    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        lobby.SetActive(false);
        gameTable.SetActive(false);
        player2Uis.SetActive(false);
        //winScreen.SetActive(false);

        networkPlayerSpawnerScript = networkManager.GetComponent<NetworkPlayerSpawner>();
        objectCheckerScript = checkerPlate.GetComponent<ObjectChecker>();   
    }

    void Update()
    {
        if (isReset && !localisReset)
        {
            ResetGame();
        }
        if (isReset)
        {
            checkBothIsReset();
        }

        // check if player touches button at the same time
        if (!isPlaying)
        {
            buttonWalls = GameObject.FindGameObjectsWithTag("ButtonWall");
            if (buttonWalls != null && buttonWalls.Length != 0)
            {
                int counter = 0;
                foreach (GameObject btnWall in buttonWalls)
                {
                    if (btnWall.GetComponent<ButtonController>().isTouched)
                    {
                        counter++;
                    }
                }

                if (counter == PhotonNetwork.PlayerList.Length)
                {
                    StartGame();
                    isPlaying = true;
                }
            }
        }
    }


    private void StartGame()
    {
        Debug.Log("Start Game!");
        game1UiRenderer.gameHasStarted = true;

        // send impulse
        leftController.GetComponent<HapticFeedbackOnHover>().StartHapticPulse();
        rightController.GetComponent<HapticFeedbackOnHover>().StartHapticPulse();

        PlaceGameObjects();
    }

    private void PlaceGameObjects()
    {
        gameTable.SetActive(true);
        player2Uis.SetActive(true);
        startButtons = GameObject.FindGameObjectsWithTag("StartButton");
        foreach (GameObject btn in startButtons)
        {
            btn.SetActive(false);
        }
    }

    private void ResetGame()
    {
        // reset visibilities
        gameTable.SetActive(false);
        player2Uis.SetActive(false);
        if (startButtons != null && startButtons.Length == PhotonNetwork.PlayerList.Length)
        {
            foreach (GameObject btn in startButtons)
            {
                btn.SetActive(true);
            }
        }
        winScreen.SetActive(false);
        teleportArea.SetActive(false);

        // reset player position
        xrOrigin.transform.position = networkPlayerSpawnerScript.playerPosition;

        // reset helper object position
        GameObject helperObject = networkPlayerSpawnerScript.GetHelperObject();
        if (helperObject != null)
        {
            helperObject.transform.position = networkPlayerSpawnerScript.helperPosition;
        }

        // reset tasks achieved
        objectCheckerScript.tasksAchieved = false;
        objectCheckerScript.object1Found = false;
        objectCheckerScript.object2Found = false;
        objectCheckerScript.object3Found = false;
        foreach (GameObject btnWall in buttonWalls)
        {
            if (btnWall != null)
            {
                btnWall.GetComponent<ButtonController>().isTouched = false;
            }
        }

        // delete created objects
        GameObject[] createdObjects = GameObject.FindGameObjectsWithTag("CreatedObject");
        foreach (GameObject createdObject in createdObjects)
        {
            PhotonNetwork.Destroy(createdObject);
        }

        // reset game status
        isPlaying = false;
        localisReset = true;
    }

    public void checkBothIsReset()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        int counter = 0;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkPlayerScript>().GetPlayerStatus() == PlayerStatus.hasRestarted)
            {
                counter++;
            }
        }
        if (counter == PhotonNetwork.PlayerList.Length)
        {
            isReset = false;
            localisReset = false;
        }
    }

    public void setResetGame(bool value)
    {
        isReset = value;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            isReset = (bool)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(isReset);
        }
    }


    /*
    private void ResetGame()
    {

        // TODO RESET EVERYTHING TO BEGINNING... ?
        PlaceGameObjects();

        leftStartButton.SetActive(false);
        rightStartButton.SetActive(false);
        leftResetButton.SetActive(true);
        rightResetButton.SetActive(true);
        leftStartReady = false;
        rightStartReady = false;
        leftStartReady = false;
        rightStartReady = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        startReady[0] = leftStartReady;
        startReady[1] = rightStartReady;

        if (stream.IsReading)
        {
            startReady = (bool[])stream.ReceiveNext();

            leftStartReady = startReady[0];
            rightStartReady = startReady[1];
        }
        else if (stream.IsWriting)
        {

            stream.SendNext(startReady);
        }
    }*/

}
