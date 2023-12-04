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

    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;

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
        /*
        switch (objectType)
        {
            case "cube":
                createdObject = "CreatedCube";
                break;
            case "sphere":
                createdObject = spherePrefab;
                break;
            case "cylinder":
                createdObject = cylinderPrefab;
                break;
        }*/

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
                    interactable.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.35f, 0.35f, 1.0f));
                    break;
                case "yellow":
                    interactable.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.75f, 0.35f, 1.0f));
                    break;
                case "green":
                    interactable.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 1.0f, 0.5f, 1.0f));
                    break;
                case "blue":
                    interactable.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.35f, 0.75f, 1.0f, 1.0f));
                    break;
            }
        }
    }

}
