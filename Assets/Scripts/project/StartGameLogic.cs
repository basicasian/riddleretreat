using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameLogic : MonoBehaviour
{
    public GameObject gameTable;
    public GameObject description;
    public GameObject player2Uis;


    public GameObject leftStartButton;
    private bool leftStartReady = false;
    public GameObject rightStartButton;
    private bool rightStartReady = false;

    public GameObject rightResetButton;
    public GameObject leftResetButton;
    private bool rightResetReady = false;
    private bool leftResetReady = false;

    public Game1UiRenderer game1UiRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rightResetButton.SetActive(false);
        leftResetButton.SetActive(false);
        gameTable.SetActive(false);
        description.SetActive(false);

        player2Uis.SetActive(false);
    }

    void Update()
    {
        if ((leftStartReady == true) && (rightStartReady == true)){
            StartGame();
        }

        if ((rightResetReady == true) && (leftResetReady == true))
        {
            ResetGame();
        }
    }


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

    public void StartGame()
    {
        game1UiRenderer.gameHasStarted = true;
        PlaceGameObjects();
        ResetGame();
    }


    public void PlaceGameObjects()
    {
        gameTable.SetActive(true);
        description.SetActive(true);
        player2Uis.SetActive(true);
    }


    public void ResetGame()
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


}
