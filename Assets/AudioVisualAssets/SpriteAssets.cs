using UnityEngine;
using System.Collections;

public class SpriteAssets : MonoBehaviour {

    public static SpriteAssets spriteAssets;

    public Sprite coin;
    public Sprite leftCliff;
    public Sprite rightCliff;

    public void Awake()
    {
        spriteAssets = this;
    }
}
