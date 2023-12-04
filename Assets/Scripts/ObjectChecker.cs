using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    private bool tasksAchieved;
    public GameObject objectToBuild1;
    private bool object1Found;
    public GameObject objectToBuild2;
    private bool object2Found;
    public GameObject objectToBuild3;
    private bool object3Found;

    public GameObject checkerPlate;

    // Start is called before the first frame update
    void Start()
    {
        tasksAchieved = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Renderer objectRenderer = other.gameObject.GetComponent<Renderer>();
        if (objectToBuild1.GetComponent<CreatedObjectUpdate>().GetColour() == other.gameObject.GetComponent<CreatedObjectUpdate>().GetColour())
        {
            Debug.Log("PLACED ON");
        }


    }

    void CheckAllTasksAchieved()
    {
        // Check if all three tasks are achieved
        if (object1Found == true && object2Found == true && object3Found == true)
        {
            tasksAchieved = true;
            Debug.Log("All tasks achieved!");
        }
    }
}
