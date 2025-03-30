using UnityEngine;

public class SpriteCombiner : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers; // Assign your sprites in the Inspector

    public void CombineSprites()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            Debug.LogError("No sprites assigned!");
            return;
        }

        // Get size of the first sprite (assumes all are the same size)
        Texture2D firstTexture = spriteRenderers[0].sprite.texture;
        if (!firstTexture.isReadable)
        {
            Debug.LogError("Texture is not readable! Enable Read/Write in Import Settings.");
            return;
        }

        int width = firstTexture.width;
        int height = firstTexture.height;

        // Create a new transparent texture
        Texture2D combinedTexture = new Texture2D(width, height);
        combinedTexture.filterMode = FilterMode.Point; // Pixel-perfect
        combinedTexture.wrapMode = TextureWrapMode.Clamp;

        // Start with a transparent base
        Color[] basePixels = new Color[width * height];
        for (int i = 0; i < basePixels.Length; i++) basePixels[i] = new Color(0, 0, 0, 0);
        combinedTexture.SetPixels(basePixels);

        // Overlay each sprite texture
        foreach (var renderer in spriteRenderers)
        {
            Texture2D spriteTexture = renderer.sprite.texture;
            Color[] spritePixels = spriteTexture.GetPixels();

            for (int i = 0; i < spritePixels.Length; i++)
            {
                // Blend colors (if alpha > 0, place pixel)
                if (spritePixels[i].a > 0)
                {
                    basePixels[i] = spritePixels[i];
                }
            }
        }

        // Apply changes
        combinedTexture.SetPixels(basePixels);
        combinedTexture.Apply();

        // Create a new sprite from the combined texture
        Sprite combinedSprite = Sprite.Create(
            combinedTexture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f) // Pivot point
        );

        // Assign the new sprite to a SpriteRenderer
        SpriteRenderer combinedRenderer = gameObject.AddComponent<SpriteRenderer>();
        combinedRenderer.sprite = combinedSprite;

        // Disable the original sprite renderers
        foreach (var renderer in spriteRenderers)
        {
            renderer.enabled = false;
        }
    }
}
