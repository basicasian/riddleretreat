using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public GameObject gameTable;
    public GameObject description;
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    public void StartGame()
    {
        gameTable.SetActive(true);
        description.SetActive(true);
        button.SetActive(false);
    }
}
