using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class HelperObjectMovement : MonoBehaviour
{
    public GameObject helperObject;
    public BodyBasedSteering bodyBasedSteeringScript;


    // Start is called before the first frame update
    void Start()
    {
        bodyBasedSteeringScript = GameObject.Find("Managers/Player Manager").GetComponent<BodyBasedSteering>();
        bodyBasedSteeringScript.setHelperObject(helperObject);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bodyBasedSteeringScript != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                bodyBasedSteeringScript.setIsCollidingObstacle(true);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
            {
                bodyBasedSteeringScript.setIsOnPoison(false);
                bodyBasedSteeringScript.setCollidingObject(other);

            }
        } 

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
