using UnityEngine;
using System.Collections;

public class BoostBar : MonoBehaviour {

    public Sprite boostbarSprite;
    private float startingXScale = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (GameSystem.INVULNERABLE)
        {
            GetComponent<SpriteRenderer>().sprite = boostbarSprite;

            float scaleRatio = GameSystem.retrieveRemainingInvulnTimeRatio() * startingXScale;

            transform.localScale = new Vector3(scaleRatio, .5f, 1f);
            
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = null;
            transform.localScale = new Vector3(startingXScale, .5f, 1f);
        }
	}
}
