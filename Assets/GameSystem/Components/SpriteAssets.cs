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

    public Sprite scoreCategorySprite;
    public Sprite selectedScoreCategorySprite;
    public Sprite boostCategorySprite;
    public Sprite selectedBoostCategorySprite;
    public Sprite cumulativeCategorySprite;
    public Sprite selectedCumulativeCategorySprite;

    public Sprite gliderSelector;

    public List<Sprite> lockedGliderFrames;
    public List<Sprite> defaultFrames;
    public List<Sprite> scoreGliderOneFrames;
    public List<Sprite> scoreGliderTwoFrames;
    public List<Sprite> scoreGliderThreeFrames;
    public List<Sprite> scoreGliderFourFrames;
    public List<Sprite> scoreGliderFiveFrames;
    public List<Sprite> scoreGliderSixFrames;
    public List<Sprite> boostGliderOneFrames;
    public List<Sprite> boostGliderTwoFrames;
    public List<Sprite> boostGliderThreeFrames;
    public List<Sprite> boostGliderFourFrames;
    public List<Sprite> cumulativeGliderOneFrames;
    public List<Sprite> cumulativeGliderTwoFrames;
    public List<Sprite> cumulativeGliderThreeFrames;
    public List<Sprite> cumulativeGliderFourFrames;
    public List<Sprite> cumulativeGliderFiveFrames;
            
    public List<Sprite> chargedAnimationSprites;
    public List<Sprite> clouds;
    public List<Sprite> deathAnimationSprites;
    public List<List<Sprite>> scoreGliders;
    public List<List<Sprite>> boostGliders;
    public List<List<Sprite>> cumulativeGliders;
    

    public void Awake()
    {
        scoreGliders = new List<List<Sprite>>();
        boostGliders = new List<List<Sprite>>();
        cumulativeGliders = new List<List<Sprite>> ();
        scoreGliders.Add(defaultFrames);
        scoreGliders.Add(scoreGliderOneFrames);
        scoreGliders.Add(scoreGliderTwoFrames);
        scoreGliders.Add(scoreGliderThreeFrames);
        scoreGliders.Add(scoreGliderFourFrames);
        scoreGliders.Add(scoreGliderFiveFrames);
        scoreGliders.Add(scoreGliderSixFrames);

        boostGliders.Add(boostGliderOneFrames);
        boostGliders.Add(boostGliderTwoFrames);
        boostGliders.Add(boostGliderThreeFrames);
        boostGliders.Add(boostGliderFourFrames);

        cumulativeGliders.Add(cumulativeGliderOneFrames);
        cumulativeGliders.Add(cumulativeGliderTwoFrames);
        cumulativeGliders.Add(cumulativeGliderThreeFrames);
        cumulativeGliders.Add(cumulativeGliderFourFrames);
        cumulativeGliders.Add(cumulativeGliderFiveFrames);
        spriteAssets = this;
    }
}
