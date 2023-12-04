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

    public void createObject(string objectType)
    {
        string createdObject = "Prefabs/Project/" + objectType;

        PhotonNetwork.Instantiate(createdObject, startPosition, Quaternion.identity);

        //Instantiate(createdObject, startPosition, Quaternion.identity);
    }


    public void increaseSize()
    {
        leftInteractor.GetValidTargets(grabInteractables);
        rightInteractor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {
            interactable.transform.gameObject.transform.localScale *= (1.1f);
            interactable.transform.gameObject.GetComponent<Rigidbody>().mass *= (1.1f);
        }
    }

    public void reduceSize()
    {
        leftInteractor.GetValidTargets(grabInteractables);
        rightInteractor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {
            interactable.transform.gameObject.transform.localScale *= (1 - scaleFactor);
            interactable.transform.gameObject.GetComponent<Rigidbody>().mass *= (1 - scaleFactor);
        }
    }

    public void colorObject(string color)
    {
        leftInteractor.GetValidTargets(grabInteractables);
        rightInteractor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {
            switch (color)
            {
                case "red":
                    interactable.transform.gameObject.GetComponent<CreatedObjectUpdate>().setColour(Colour.red);
                    break;
                case "yellow":
                    interactable.transform.gameObject.GetComponent<CreatedObjectUpdate>().setColour(Colour.yellow);
                    break;
                case "green":
                    interactable.transform.gameObject.GetComponent<CreatedObjectUpdate>().setColour(Colour.green);
                    break;
                case "blue":
                    interactable.transform.gameObject.GetComponent<CreatedObjectUpdate>().setColour(Colour.blue);
                    break;
            }
        }
    }

}
