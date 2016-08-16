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

    public Sprite lockedGlider;
    public Sprite gliderDefault;
    public Sprite gliderOneSkin;
    public Sprite gliderTwoSkin;
    public Sprite gliderThreeSkin;
    public Sprite gliderFourSkin;
    public Sprite gliderFiveSkin;
    public Sprite gliderSixSkin;
    public Sprite gliderSevenSkin;

    public Sprite creditsImage;

    public List<Sprite> chargedAnimationSprites;
    public List<Sprite> clouds;
    public List<Sprite> deathAnimationSprites;
    public List<Sprite> allGliders;

    public void Awake()
    {
        allGliders.Add(gliderDefault);
        allGliders.Add(gliderOneSkin);
        allGliders.Add(gliderTwoSkin);
        allGliders.Add(gliderThreeSkin);
        allGliders.Add(gliderFourSkin);
        allGliders.Add(gliderFiveSkin);
        allGliders.Add(gliderSixSkin);
        allGliders.Add(gliderSevenSkin);
        spriteAssets = this;
    }
}
