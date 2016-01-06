using UnityEngine;
using System.Collections;

/// <summary>
/// Rescales the glider according to screen dimensions.
/// </summary>
public class RescaleGlider : MonoBehaviour {

    public float widthScale;
    public float heightScale;

    float orthographicScreenHeight;
    float orthographicScreenWidth;

	// Use this for initialization
	void Start () {
        orthographicScreenHeight = Camera.main.orthographicSize * 2;
        orthographicScreenWidth = orthographicScreenHeight * Screen.width / Screen.height;
        float spriteUnitWidth = Tools.calculateSpriteUnitWidth(GetComponent<SpriteRenderer>().sprite);
        float spriteUnitHeight = Tools.calculateSpriteUnitHeight(GetComponent<SpriteRenderer>().sprite);

        transform.localScale = new Vector3(orthographicScreenWidth / spriteUnitWidth / widthScale, orthographicScreenHeight / spriteUnitHeight / heightScale);
    }

}
