using UnityEngine;
using System.Collections;

/// <summary>
/// Causes the text object to fade its opacity in and out.
/// </summary>
public class TextFade : MonoBehaviour {

    bool fadeIn = false;
    bool fadeOut = false;
    float fadeSpeed = 0.01f;
    float minAlpha = 0.0f;
    float maxAlpha = 1.0f;
    Color color;

    void Start()
    {
        GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        color = GetComponent<TextMesh>().color;
    }

    void Update()
    {
        GetComponent<TextMesh>().color = color;
        if (fadeIn && !fadeOut)
            FadeIn();
        if (fadeOut && !fadeIn)
            FadeOut();
        if (color.a <= minAlpha)
        {
            fadeOut = false;
            fadeIn = true;
        }
        if (color.a >= maxAlpha)
        {
            fadeIn = false;
            fadeOut = true;
        }
    }

    /// <summary>
    /// Fades the text to become darker.
    /// </summary>
    void FadeIn()
    {
        color.a += fadeSpeed;
    }

    /// <summary>
    /// Fades the text to become lighter.
    /// </summary>
    void FadeOut()
    {
        color.a -= fadeSpeed;
    }
}
