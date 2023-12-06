using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class StartGameLogic : MonoBehaviour
{
    public GameObject gameTable;
    public GameObject player2Uis;

    public GameObject leftDescription;
    public GameObject rightDescription;

    //public GameObject leftStartButton;
    //public bool leftStartReady = false;
    //public GameObject rightStartButton;
    //public bool rightStartReady = false;
    
    public GameObject rightResetButton;
    public GameObject leftResetButton;
    private bool rightResetReady = false;
    private bool leftResetReady = false;

    bool[] startReady = new bool[2];

    public Game1UiRenderer game1UiRenderer;



    public GameObject[] walls;

    // Start is called before the first frame update
    void Start()
    {
        rightResetButton.SetActive(false);
        leftResetButton.SetActive(false);
        gameTable.SetActive(false);
        leftDescription.SetActive(false);
        rightDescription.SetActive(false);

        player2Uis.SetActive(false);
    }

    void Update()
    {

        /*
        walls = GameObject.FindGameObjectsWithTag("ButtonWall");

        if (walls != null && walls.Length == 2)
        {
            int counter = 0; 
            foreach (GameObject wall in walls)
            {
                if (wall.GetComponent<ButtonController>().isClicked)
                {
                    counter++;
                }

            }
            if (counter == 2)
            {
                StartGame();
                counter = 0;
            }

        }
        */
        /*
        Debug.Log(leftStartButton.GetComponent<ButtonController>().isClicked + ", " + rightStartButton.GetComponent<ButtonController>().isClicked);
        if ((leftStartButton.GetComponent<ButtonController>().isClicked == true) || (rightStartButton.GetComponent<ButtonController>().isClicked == true)){
            StartGame();
        }*/

        /*
        if ((rightResetReady == true) && (leftResetReady == true))
        {
            ResetGame();
        }*/
    }

    /*
    public void SetRightReady()
    {
        rightStartButton.SetActive(false);
        rightStartReady = true;
    }


    public void SetLeftReady()
    {
        leftStartButton.SetActive(false);
        leftStartReady = true;
    }
    */
    private void StartGame()
    {
        game1UiRenderer.gameHasStarted = true;
        PlaceGameObjects();
    }


    private void PlaceGameObjects()
    {
        leftDescription.SetActive(true);
        rightDescription.SetActive(true);

        leftStartButton.SetActive(false);
        rightStartButton.SetActive(false);

        gameTable.SetActive(true);
        player2Uis.SetActive(true);
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
