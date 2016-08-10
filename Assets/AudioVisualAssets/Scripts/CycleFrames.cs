using UnityEngine;
using System.Collections.Generic;

public class CycleFrames : MonoBehaviour {

    public List<Sprite> sprites;
    public int framesPerSecond;
    public bool loop;
    private int nonloopIndex;
	
    void Start()
    {
        nonloopIndex = 0;
    }
    
	void Update () {
        int length = sprites.Count;
        int i = Mathf.FloorToInt(Time.time * framesPerSecond) % length;
        if (loop)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[i];
        }

        else if (nonloopIndex < length)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[nonloopIndex];
            nonloopIndex++;
        }
	}
}
