using UnityEngine;
using System.Collections.Generic;

public class GliderAnimation : MonoBehaviour {
    public static bool doAnimation;
    public static List<Sprite> spriteCycle;
    private int framesPerSecond = 10;

    void Start()
    {
        doAnimation = true;
        spriteCycle = UserData.userData.getSpriteFramesByEnum()[UserData.userData.getGliderSkinIndex()];
    }

    void Update()
    {
        if (!doAnimation) { return; }
        if (spriteCycle.Count > 1)
        {
            animateMultiframe();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = spriteCycle[0];
        }

    }

    public static void setFrameCycle(List<Sprite> sprites)
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
