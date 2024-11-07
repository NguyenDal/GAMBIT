using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraColorBlindEffect : MonoBehaviour
{
    public enum ColorBlindMode
    {
        Normal = 0,
        Protanopia = 1,
        Deuteranopia = 2,
        Tritanopia = 3
    }

    public ColorBlindMode colorblindMode = ColorBlindMode.Normal;

    private Material colorblindMaterial;

    void Start()
    {
        // Load the colorblind shader
        Shader shader = Shader.Find("Hidden/Wilberforce/Colorblind");
        if (shader == null)
        {
            Debug.LogError("Colorblind shader not found.");
            return;
        }
        colorblindMaterial = new Material(shader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (colorblindMaterial != null)
        {
            colorblindMaterial.SetInt("type", (int)colorblindMode);
            Graphics.Blit(src, dest, colorblindMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
