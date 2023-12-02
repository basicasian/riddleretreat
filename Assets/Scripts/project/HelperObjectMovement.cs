using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class HelperObjectMovement : MonoBehaviour
{
    public XROrigin xrOrigin = null;
    private BodyBasedSteering bodyBasedSteeringScript;


    // Start is called before the first frame update
    void Start()
    {
        bodyBasedSteeringScript = xrOrigin.GetComponent<BodyBasedSteering>();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            bodyBasedSteeringScript.setIsCollidingObstacle(true);
            bodyBasedSteeringScript.setCollidingObstacle(other);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        bodyBasedSteeringScript.setIsLookingAtObstacle(other);
    }

    private void OnTriggerExit(Collider other)
    {
        bodyBasedSteeringScript.setIsCollidingObstacle(false);
    }

}
