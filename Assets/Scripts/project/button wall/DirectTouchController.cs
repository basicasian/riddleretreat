using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectTouchController : MonoBehaviour
{
    public GameObject wallGameObject;
    private ButtonController buttonControllerScript; 

    // Start is called before the first frame update
    void Start()
    {
        buttonControllerScript = wallGameObject.GetComponent<ButtonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if touch start button
        if (name == "StartButton" && buttonControllerScript != null)
        {
            buttonControllerScript.isClicked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if touch start button
        if (name == "StartButton" && buttonControllerScript != null)
        {
            buttonControllerScript.isClicked = false;
        }
    }
}
