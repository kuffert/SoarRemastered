using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Contains sprites utilized in code throughout the 
/// application.
/// </summary>
public class SpriteAssets : MonoBehaviour {

    public static SpriteAssets spriteAssets;

    public Sprite coin;
    public Sprite leftCliff;
    public Sprite rightCliff;

    public Sprite emptyCharge;
    public Sprite charge;

    public List<Sprite> lockedGliderFrames;
    public List<Sprite> gliderDefaultFrames;
    public List<Sprite> gliderOneFrames;
    public List<Sprite> gliderTwoFrames;
    public List<Sprite> gliderThreeFrames;
    public List<Sprite> gliderFourFrames;
    public List<Sprite> gliderFiveFrames;
    public List<Sprite> gliderSixFrames;
    public List<Sprite> gliderSevenFrames;
    public List<Sprite> gliderEightFrames;
    public List<Sprite> gliderNineFrames;
    public List<Sprite> gliderTenFrames;
    public List<Sprite> gliderElevenFrames;
            
    public List<Sprite> chargedAnimationSprites;
    public List<Sprite> clouds;
    public List<Sprite> deathAnimationSprites;
    public List<List<Sprite>> allGliders;
    

    public void Awake()
    {
        allGliders = new List<List<Sprite>>();
        allGliders.Add(gliderDefaultFrames);
        allGliders.Add(gliderOneFrames);
        allGliders.Add(gliderTwoFrames);
        allGliders.Add(gliderThreeFrames);
        allGliders.Add(gliderFourFrames);
        allGliders.Add(gliderFiveFrames);
        allGliders.Add(gliderSixFrames);
        allGliders.Add(gliderSevenFrames);
        allGliders.Add(gliderEightFrames);
        allGliders.Add(gliderNineFrames);
        allGliders.Add(gliderTenFrames);
        allGliders.Add(gliderElevenFrames);
        spriteAssets = this;
    }
}
