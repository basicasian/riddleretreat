using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public Game1UiRenderer gameStarted;
    private bool tasksAchieved;
    public GameObject objectToBuild1;
    public GameObject objectToBuild2;
    public GameObject objectToBuild3;

    // Start is called before the first frame update
    void Start()
    {
        tasksAchieved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            return; 
        }


    }
}
