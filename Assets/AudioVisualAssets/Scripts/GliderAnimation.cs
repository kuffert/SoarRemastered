using UnityEngine;
using System.Collections.Generic;

public class GliderAnimation : MonoBehaviour {
    public bool multiframe;
    private List<Sprite> spriteCycle;
    private int framesPerSecond = 10;

    void Start()
    {
        //spriteCycle = SpriteAssets.spriteAssets.allGliders[UserData.userData.getGliderSkinIndex()];
    }

    void Update()
    {
        if (multiframe)
        {
            animateMultiframe();
        }
    }

    public void setMultiFrameSprites(List<Sprite> sprites)
    {
        spriteCycle = sprites;
    }
    

    public void animateMultiframe()
    {
        int length = spriteCycle.Count;
        int i = Mathf.FloorToInt(Time.time * framesPerSecond) % length;
       
        GetComponent<SpriteRenderer>().sprite = spriteCycle[i];
    }
}
