using UnityEngine;

public class Transparency : MonoBehaviour
{
    public float transparency = 0.5f; // Adjust transparency (0 is fully transparent, 1 is fully opaque)

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Clone material to avoid modifying shared materials
            renderer.material = new Material(renderer.material);

            Color color = renderer.material.color;
            color.a = transparency;
            renderer.material.color = color;

            // Ensure transparency is correctly rendered
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            // Adjust render queue to ensure proper transparency sorting
            renderer.material.renderQueue = 3000;
        }
    }
}
