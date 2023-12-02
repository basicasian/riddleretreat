using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using System;


public class BodyBasedSteering : MonoBehaviour
{
    public InputActionReference steeringReference = null;
    public Camera mainCamera = null;
    public XROrigin xrOrigin = null;
    public float speed = 0;

    public Vector3 forwardDirection;
    private bool isStandingOnHelper = false;
    public GameObject currentHelper;

    private bool isCollidingObstacle = false;
    private Collider collidingObstacle;
    private bool isLookingAtObstacle = true;
    Vector3 movementRestriction = new Vector3(1.0f, 0.0f, 1.0f);

    void Start()
    {
    }

    void Update()
    {
        if (steeringReference.action.IsPressed() && checkCollider("HelperObject"))
        {      
            Steering();
        }
    }

    public void Steering()
    {

        // if is not colliding against anything,  move in x and z direction
        if (!isCollidingObstacle)
        {
            movementRestriction = new Vector3(1.0f, 0.0f, 1.0f);
        } 

        Vector3 deltaSteering = (Vector3.Scale(mainCamera.transform.forward, movementRestriction));
        xrOrigin.transform.position += deltaSteering * speed * Time.deltaTime;
        currentHelper.transform.position += deltaSteering * speed * Time.deltaTime;
    }



    public bool checkCollider(string tag)
    {
        var offset = new Vector3(0, 2, 0);
        var localPoint0 = mainCamera.transform.position - offset;
        var localPoint1 = mainCamera.transform.position + offset;

        var colliders = Physics.OverlapCapsule(localPoint0, localPoint1, 0.1f);

        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                //Debug.Log(col);
                if (col.CompareTag(tag))
                {
                    currentHelper = col.gameObject;
                    return true;
                }
            }
        }
         return false;
    }

    public void setIsCollidingObstacle(bool value)
    {
        isCollidingObstacle = value;
    }

    public void setCollidingObstacle(Collider collider)
    {
        collidingObstacle = collider;

   
        // if is colliding against obstacle, do not move in direction of obstacle; set once during collision start
        // TODO: currently stick to the obstacle - can be used as a feature
        Vector3 closestPoint = collidingObstacle.ClosestPointOnBounds(currentHelper.transform.position);

        // if closestPoint is closer to x direction 
        // do not move in x direction
        if (Math.Abs(closestPoint.x - currentHelper.transform.position.x) <= 0.2)
        {
            movementRestriction = new Vector3(1.0f, 0.0f, 0.0f);
        }

        // if closestPoint is closer to z direction 
        // do not move in z direction
        else if (Math.Abs(closestPoint.z - currentHelper.transform.position.z) <= 0.2)
        {
            movementRestriction = new Vector3(0.0f, 0.0f, 1.0f);
        }

    }

    // not used yet
    public void setIsLookingAtObstacle(Collider collider)
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        isLookingAtObstacle = collider.bounds.IntersectRay(ray);
    }
}
