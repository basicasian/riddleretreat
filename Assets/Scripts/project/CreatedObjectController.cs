using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CreatedObjectController : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        leftInteractor = leftController.GetComponent<XRDirectInteractor>();
        rightInteractor = rightController.GetComponent<XRDirectInteractor>();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createObject(string objectType)
    {
        GameObject createdObject = cubePrefab;
        switch (objectType)
        {
            case "cube":
                createdObject = cubePrefab;
                break;
            case "sphere":
                createdObject = spherePrefab;
                break;
            case "cylinder":
                createdObject = cylinderPrefab;
                break;
        }

        Instantiate(createdObject, new Vector3(0, 0, 0), Quaternion.identity);
    }


    public void increaseSize()
    {
        leftInteractor.GetValidTargets(grabInteractables);
        rightInteractor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {
            interactable.transform.gameObject.transform.localScale *= scaleFactor;
            interactable.transform.gameObject.GetComponent<Rigidbody>().mass *= scaleFactor;
        }
    }

    public void reduceSize()
    {
        leftInteractor.GetValidTargets(grabInteractables);
        rightInteractor.GetValidTargets(grabInteractables);

        foreach (var interactable in grabInteractables)
        {
            interactable.transform.gameObject.transform.localScale *= -scaleFactor;
            interactable.transform.gameObject.GetComponent<Rigidbody>().mass *= -scaleFactor;
        }
    }

}
