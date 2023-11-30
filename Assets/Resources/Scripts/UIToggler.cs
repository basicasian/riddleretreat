using UnityEngine;
using UnityEngine.UI;

public class UIToggler : MonoBehaviour
{
    public GameObject secondUI;

    void Start()
    {
        // Make sure the second UI is initially hidden
        secondUI.SetActive(false);
    }

    public void ToggleSecondUI()
    {
        // Toggle the visibility of the second UI
        secondUI.SetActive(!secondUI.activeSelf);
    }
}
