using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (settingsPanel == null)
        {
            Debug.LogError("Settings Panel is not assigned in the Inspector!");
        }
    }

    public void OnButtonClick()
    {
        if (settingsPanel != null)
        {
            SetPanelVisibility(false);
        }
    }

    private void SetPanelVisibility(bool isVisible)
    {
        CanvasGroup canvasGroup = settingsPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = settingsPanel.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = isVisible ? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }
}