using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class BodyBasedSteering : MonoBehaviour
{
    public InputActionReference steeringReference = null;
    public Camera mainCamera = null;
    public XROrigin xrOrigin = null;

    private CharacterController characterController = null;
    public float speed = 0;

    public GameObject helperObject;
    public LayerMask helperObjectLayer;  // Layer mask for obstacles
    public Transform forwardDirection;
    private bool isCollidingWithObject = false;

    void Start()
    {
        characterController = xrOrigin.GetComponent<CharacterController>();
    }

    void Update()
    {
        isCollidingWithObject = checkCollider();

        if (steeringReference.action.IsPressed() && isCollidingWithObject)
        {      
            Steering();
        }
    }

    public void Steering()
    {
        Vector3 deltaSteering = (Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)));
        xrOrigin.transform.position += deltaSteering * speed * Time.deltaTime;
        helperObject.transform.position += deltaSteering * speed * Time.deltaTime;
    }


    public bool checkCollider()
    {
        var offset = new Vector3(0, 2, 0);
        var localPoint0 = mainCamera.transform.position - offset;
        var localPoint1 = mainCamera.transform.position + offset;

        var colliders = Physics.OverlapCapsule(localPoint0, localPoint1, 0.2f);

        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {

                //Debug.Log(col);
                if (col.CompareTag("HelperObject"))
                {
                    return true;
                }
            }
        }
         return false;
    }

    public bool getSetIsCollidingWithObject()
    {
        return isCollidingWithObject;
    }

}
