using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import TextMesh Pro namespace

public class changeonchange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public bool hasallcomponents = false;
    public humanomoveo humanomoveo;

    public TextMeshProUGUI counterText; // Reference to the TextMesh Pro UI text
    private int totalComponents = 4; // Total number of components required

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
        hasallcomponents = false;

        UpdateCounterUI(); // Initialize the counter UI
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has all components
        if (humanomoveo.hasEngine && humanomoveo.haswheels && humanomoveo.hasgas && humanomoveo.hasshell)
        {
            hasallcomponents = true;
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            hasallcomponents = false;
            spriteRenderer.sprite = sprite1;
        }

        UpdateCounterUI(); // Update the counter UI every frame
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hasallcomponents)
            {
                Debug.Log("You have all the components!");
                SceneManager.LoadScene("Endtitles");
            }
            else
            {
                Debug.Log("You do not have all the components!");
            }
        }
    }

    void UpdateCounterUI()
    {
        // Count how many components the player has
        int collectedComponents = 0;
        if (humanomoveo.hasEngine) collectedComponents++;
        if (humanomoveo.haswheels) collectedComponents++;
        if (humanomoveo.hasgas) collectedComponents++;
        if (humanomoveo.hasshell) collectedComponents++;

        // Update the TextMesh Pro text
        counterText.text = $"{collectedComponents}/{totalComponents}";
    }
}