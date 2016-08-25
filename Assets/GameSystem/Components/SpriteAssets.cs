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
    public List<Sprite> defaultFrames;
    public List<Sprite> scoreGliderOneFrames;
    public List<Sprite> scoreGliderTwoFrames;
    public List<Sprite> scoreGliderThreeFrames;
    public List<Sprite> scoreGliderFourFrames;
    public List<Sprite> scoreGliderFiveFrames;
    public List<Sprite> chargeGliderOneFrames;
    public List<Sprite> chargeGliderTwoFrames;
    public List<Sprite> chargeGliderThreeFrames;
    public List<Sprite> specialGliderOneFrames;
    public List<Sprite> specialGliderTwoFrames;
    public List<Sprite> cumulativeGliderOneFrames;
            
    public List<Sprite> chargedAnimationSprites;
    public List<Sprite> clouds;
    public List<Sprite> deathAnimationSprites;
    public List<List<Sprite>> allGliders;
    

    public void Awake()
    {
        allGliders = new List<List<Sprite>>();
        allGliders.Add(defaultFrames);
        allGliders.Add(scoreGliderOneFrames);
        allGliders.Add(scoreGliderTwoFrames);
        allGliders.Add(scoreGliderThreeFrames);
        allGliders.Add(scoreGliderFourFrames);
        allGliders.Add(scoreGliderFiveFrames);
        allGliders.Add(chargeGliderOneFrames);
        allGliders.Add(chargeGliderTwoFrames);
        allGliders.Add(chargeGliderThreeFrames);
        allGliders.Add(specialGliderOneFrames);
        allGliders.Add(specialGliderTwoFrames);
        allGliders.Add(cumulativeGliderOneFrames);
        spriteAssets = this;
    }
}
