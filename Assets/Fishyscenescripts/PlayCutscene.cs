using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    public Collider2D playerCollider; // Reference to the Player's Collider
    public Collider2D otherCollider; // Reference to the other Collider
    public Camera mainCamera; // Reference to the main camera
    public Camera cutsceneCamera; // Reference to the cutscene camera
    public Animator cutsceneAnimator; // Animator for the cutscene
    public string nextSceneName; // Name of the scene to load after the cutscene

    private bool isPlaying = false;

    void Start()
    {
        // Ensure the main camera is initially enabled and cutscene camera is disabled
        mainCamera.enabled = true;
        cutsceneCamera.enabled = false;
    }

    void Update()
    {
        if (!isPlaying && playerCollider.IsTouching(otherCollider))
        {
            StartCoroutine(StartCutscene());
            Debug.Log("Cutscene started");
        }
    }

    IEnumerator StartCutscene()
    {
        isPlaying = true;

        // Disable the main camera and enable the cutscene camera
        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

        // Play the animation
        cutsceneAnimator.SetTrigger("PlayCutscene");

        // Wait for the animation to complete
        yield return new WaitForSeconds(cutsceneAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
