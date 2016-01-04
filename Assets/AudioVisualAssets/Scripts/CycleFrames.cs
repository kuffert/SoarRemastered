using UnityEngine;
using System.Collections.Generic;

public class CycleFrames : MonoBehaviour {

    public List<Sprite> sprites;
    public int framesPerSecond;
	
	// Update is called once per frame
	void Update () {
        int i = Mathf.FloorToInt(Time.time * framesPerSecond) % sprites.Count;
        GetComponent<SpriteRenderer>().sprite = sprites[i];
        GetComponent<SpriteRenderer>().transform.localScale = new Vector3(2f, 2f, 1f);
	}
}
