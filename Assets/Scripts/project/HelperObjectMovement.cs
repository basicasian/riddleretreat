using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class HelperObjectMovement : MonoBehaviour
{
    public GameObject helperObject;
    public GameObject xrOrigin = null;
    private BodyBasedSteering bodyBasedSteeringScript;


    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GameObject.Find("XR Origin");
        bodyBasedSteeringScript = xrOrigin.GetComponent<BodyBasedSteering>();
        bodyBasedSteeringScript.setHelperObject(helperObject);
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
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            bodyBasedSteeringScript.setIsOnPoison(false);
            bodyBasedSteeringScript.setCollidingObstacle(other);
            
        }


    }

    private void OnTriggerStay(Collider other)
    {
        //bodyBasedSteeringScript.setIsLookingAtObstacle(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            bodyBasedSteeringScript.setIsCollidingObstacle(false);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            bodyBasedSteeringScript.setIsOnPoison(true);
        }

    }

}
