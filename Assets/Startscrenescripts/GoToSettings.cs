using UnityEngine;
using UnityEngine.UI;

public class GoToSettings : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    void Awake()
    {
        if (settingsPanel == null)
        {
            settingsPanel = GameObject.Find("/Canvas/SettingsPanel"); // Adjust path if needed
            if (settingsPanel == null)
            {
                Debug.LogError("Settings Panel could not be found in the scene!");
            }
        }
    }

    void Start()
    {
        Debug.Log("Settings Panel at Start: " + (settingsPanel != null ? settingsPanel.name : "NULL"));
        SetPanelVisibility(false);
    }

    public void OnButtonClick()
    {
        if (settingsPanel == null)
        {
            Debug.LogError("Settings Panel is NULL when button clicked!");
            return;
        }

        Debug.Log("Settings Panel Found: " + settingsPanel.name);
        SetPanelVisibility(true);
    }

    private void SetPanelVisibility(bool isVisible)
    {
        CanvasGroup canvasGroup = settingsPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("No CanvasGroup found on Settings Panel! Assign one in Inspector.");
            return;
        }
        canvasGroup.alpha = isVisible ? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }
}
