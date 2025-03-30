using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace for SceneManager

public class ButtonClickHandler : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("fishyscene"); // Corrected this line
        Debug.Log("Button was clicked!");
        if (SceneManager.GetActiveScene().name == "fishyscene") // Corrected this line
        {
            Debug.Log("MainGame scene is active!");
        }
    }
}