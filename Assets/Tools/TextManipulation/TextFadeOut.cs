using UnityEngine;
using System.Collections;

/// <summary>
/// Causes the text object to become visible and then slowly fade to invisible.
/// </summary>
public class TextFadeOut : MonoBehaviour {

    public bool fade = false;
    float fadeSpeed = 0.01f;
    float minAlpha = 0.0f;
    float maxAlpha = 1.0f;
    Color color;
    
    void Start()
    {
        GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        color = GetComponent<TextMesh>().color;
        color.a = minAlpha;
        GetComponent<TextMesh>().color = color;
    }
    
    void Update()
    {
        GetComponent<TextMesh>().color = color;
        if (fade)
        {
            color.a = maxAlpha;
            fade = false;
        }
        if (color.a > minAlpha)
        {
            fadeOut();
        }
    }

    /// <summary>
    /// Fades the text back to invisible.
    /// </summary>
    void fadeOut()
    {
        color.a -= fadeSpeed;
    }
}
