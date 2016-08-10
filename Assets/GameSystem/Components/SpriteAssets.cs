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

    public Sprite glider;

    public Sprite creditsImage;

    public List<Sprite> chargedAnimationSprites;
    public List<Sprite> clouds;
    public List<Sprite> deathAnimationSprites;

    public void Awake()
    {
        spriteAssets = this;
    }
}
