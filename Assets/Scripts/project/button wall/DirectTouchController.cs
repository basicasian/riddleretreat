using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectTouchController : MonoBehaviour
{
    public GameObject wallGameObject;
    private ButtonController buttonControllerScript;

    public GameObject startGameLogic;
    private StartGameLogic startGameLogicScript;


    // Start is called before the first frame update
    void Start()
    {
        buttonControllerScript = wallGameObject.GetComponent<ButtonController>();
        startGameLogicScript = startGameLogic.GetComponent<StartGameLogic>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        // if touch start button
        if (name == "StartButton" && buttonControllerScript != null)
        {
            buttonControllerScript.isTouched = true;
            GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        // if touch start button
        if (name == "StartButton" && buttonControllerScript != null)
        {
            buttonControllerScript.isTouched = false;
            GetComponent<Image>().color = Color.white;
    }
    }
}
