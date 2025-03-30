using System.Collections;
using UnityEngine;

public class Dissapearscript : MonoBehaviour
{
    // Time to appear and disappear
    public float appearTime = 5.0f;
    public float disappearTime = 2.0f;

    // Reference to the GameObject that will appear/disappear
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the target object is active at the start
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }

        // Start the coroutine to toggle the object's visibility
        StartCoroutine(ToggleVisibility());
    }

    // Coroutine to toggle the object's visibility
    IEnumerator ToggleVisibility()
    {
        while (true)
        {
            // Make the target object appear
            if (targetObject != null)
            {
                targetObject.SetActive(true);
                Debug.Log("Object is now VISIBLE");
            }
            // Wait for the specified appear time
            yield return new WaitForSeconds(appearTime);

            // Make the target object disappear
            if (targetObject != null)
            {
                targetObject.SetActive(false);
                Debug.Log("Object is now INVISIBLE");
            }
            // Wait for the specified disappear time
            yield return new WaitForSeconds(disappearTime);
        }
    }
}