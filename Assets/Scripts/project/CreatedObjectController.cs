using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class CreatedObjectController : MonoBehaviourPunCallbacks
{
    public GameObject leftController;
    public GameObject rightController;
    private XRDirectInteractor leftInteractor;
    private XRDirectInteractor rightInteractor;
    List<IXRInteractable> grabInteractables = new List<IXRInteractable>();

    public float scaleFactor;
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        leftInteractor = leftController.GetComponent<XRDirectInteractor>();
        rightInteractor = rightController.GetComponent<XRDirectInteractor>();       
    }

    public void createObject(string shape)
    {
        string createdObject = "Prefabs/Project/" + shape.ToString();

        GameObject gameObject = PhotonNetwork.Instantiate(createdObject, startPosition, Quaternion.identity);

        switch (shape)
        {
            case "Cube":
                gameObject.GetComponent<CreatedObject>().setShape(Shape.cube);
                break;
            case "Sphere":
                gameObject.GetComponent<CreatedObject>().setShape(Shape.sphere);
                break;
            case "Cylinder":
                gameObject.GetComponent<CreatedObject>().setShape(Shape.cylinder);
                break;
        }
        
        //Instantiate(createdObject, startPosition, Quaternion.identity);
    }


    public void increaseSize()
    {
        grabInteractables.Clear();
        List<IXRInteractable> tempGrabInteractables = new List<IXRInteractable>();

        leftInteractor.GetValidTargets(tempGrabInteractables);
        grabInteractables.AddRange(tempGrabInteractables);
        rightInteractor.GetValidTargets(tempGrabInteractables);
        grabInteractables.AddRange(tempGrabInteractables);

        foreach (var interactable in grabInteractables)
        {
            interactable.transform.gameObject.transform.localScale *= (1 + scaleFactor);
            interactable.transform.gameObject.GetComponent<Rigidbody>().mass *= (1 + scaleFactor);
        }

    }

    public void reduceSize()
    {
        grabInteractables.Clear();
        List<IXRInteractable> tempGrabInteractables = new List<IXRInteractable>();

        leftInteractor.GetValidTargets(tempGrabInteractables);
        grabInteractables.AddRange(tempGrabInteractables);
        rightInteractor.GetValidTargets(tempGrabInteractables);
        grabInteractables.AddRange(tempGrabInteractables);

        foreach (var interactable in grabInteractables)
        {
            interactable.transform.gameObject.transform.localScale *= (1 - scaleFactor);
            interactable.transform.gameObject.GetComponent<Rigidbody>().mass *= (1 - scaleFactor);
        }
    }

    public void colorObject(string color)
    {
        grabInteractables.Clear();
        List<IXRInteractable> tempGrabInteractables = new List<IXRInteractable>();

        leftInteractor.GetValidTargets(tempGrabInteractables);
        grabInteractables.AddRange(tempGrabInteractables);
        rightInteractor.GetValidTargets(tempGrabInteractables);
        grabInteractables.AddRange(tempGrabInteractables);

        foreach (var interactable in grabInteractables)
        {
            switch (color)
            {
                case "red":
                    interactable.transform.gameObject.GetComponent<CreatedObject>().setColour(Colour.red);
                    break;
                case "yellow":
                    interactable.transform.gameObject.GetComponent<CreatedObject>().setColour(Colour.yellow);
                    break;
                case "green":
                    interactable.transform.gameObject.GetComponent<CreatedObject>().setColour(Colour.green);
                    break;
                case "blue":
                    interactable.transform.gameObject.GetComponent<CreatedObject>().setColour(Colour.blue);
                    break;
            }
        }
    }

}
