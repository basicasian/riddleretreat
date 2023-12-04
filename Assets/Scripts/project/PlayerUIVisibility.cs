using UnityEngine;

public class PlayerUIVisibility : MonoBehaviour
{
    public Transform vrCameraTransform;  // Reference to the VR camera transform
    public Canvas canvas;  // Reference to the Canvas

    private float forwardDirectionCanvas;
    private float eulerForward;

    public System.Collections.Generic.List<GameObject> childObjects;

    public Game1UiRenderer game1UiRenderer;
    private void Start()
    {
        eulerForward = canvas.transform.rotation.eulerAngles[1];
        forwardDirectionCanvas = canvas.transform.rotation.y;
    }


    void Update()
    {
        if (game1UiRenderer.gameHasStarted)
        {
            float angleToPlayer = Mathf.Atan2(vrCameraTransform.position.x - canvas.transform.position.x, vrCameraTransform.position.z - canvas.transform.position.z) * (180 / Mathf.PI);

            // Normalize angles to be in the range [0, 360)
            angleToPlayer = (angleToPlayer % 360 + 360) % 360;

            if ((angleToPlayer <= ((270 + eulerForward) % 361) && (angleToPlayer >= ((90 + eulerForward) % 361))))
            {
                if (canvas.enabled == false)
                {
                    if (childObjects != null && childObjects.Count > 0)
                    {
                        // Iterate through the list of child objects
                        foreach (GameObject childObject in childObjects)
                        {
                            // Check if the child object is not null
                            if (childObject != null)
                            {
                                // Disable the child object
                                childObject.SetActive(true);
                            }
                        }
                    }
                }
                canvas.enabled = true;
            }
            else
            {
                if (canvas.enabled == true)
                {
                    if (childObjects != null && childObjects.Count > 0)
                    {
                        // Iterate through the list of child objects
                        foreach (GameObject childObject in childObjects)
                        {
                            // Check if the child object is not null
                            if (childObject != null)
                            {
                                // Disable the child object
                                childObject.SetActive(false);
                            }
                        }
                    }
                }
                canvas.enabled = false;
            }
        }
       
    }
}
