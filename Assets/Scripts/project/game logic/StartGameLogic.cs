using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class StartGameLogic : MonoBehaviour
{
    public GameObject gameTable;
    public GameObject player2Uis;

    public Game1UiRenderer game1UiRenderer;

    public GameObject[] startButtons;
    public GameObject[] resetButtons;
    public GameObject[] backDescriptions;

    public GameObject[] buttonWalls;
    public bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        gameTable.SetActive(false);
        player2Uis.SetActive(false);
    }

    void Update()
    {
        if (gameStarted)
        {
            return;
        }

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
                gameStarted = true;
            }
        }

        /*
        if ((rightResetReady == true) && (leftResetReady == true))
        {
            ResetGame();
        }*/
    }


    private void StartGame()
    {
        Debug.Log("Start Game");
        game1UiRenderer.gameHasStarted = true;
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
