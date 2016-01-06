using UnityEngine;
using System.Collections;

/// <summary>
/// Tools offers a variety of facilitating functionality, used to
/// compute Collidable spawn locations and sizes.
/// </summary>
public static class Tools {

    /// <summary>
    /// Constructs a Vector3 with a random X spawn location.
    /// </summary>
    /// <returns></returns>
    public static Vector3 calculateRandomXVector()
    {
        float xLoc = Random.Range(1, 9) / 10f;
        return Camera.main.ViewportToWorldPoint(new Vector3(xLoc, 1.1f, 10f));
    }

    /// <summary>
    /// Constructs a Vector3 with a random Y scale.
    /// </summary>
    /// <returns></returns>
    public static Vector3 calculateRandomXScale(float minX, float maxX)
    {
        float xScale = Random.Range(minX*10f, maxX*10f) / 10f;
        return new Vector3(xScale, 1f, 1f);
    }

    /// <summary>
    /// Constructs a Vector3 with a Left-Side spawn location.
    /// </summary>
    /// <returns></returns>
    public static Vector3 calculateLeftSpawnVector(GameObject cliff)
    {
        float xLoc = calculateSpriteOffsetX(cliff);
        Vector3 viewportVector = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1.1f, 10f));
        return viewportVector + new Vector3(xLoc, 0f, 0f);
    }

    /// <summary>
    /// Constructs a Vector3 with a Right-Side spawn location.
    /// </summary>
    /// <returns></returns>
    public static Vector3 calculateRightSpawnVector(GameObject cliff)
    {
        float xLoc = calculateSpriteOffsetX(cliff);
        Vector3 viewportVector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1.1f, 10f));
        return viewportVector - new Vector3(xLoc, 0f, 0f);
    }

    /// <summary>
    /// Constructs a worldpoint vector3 from a viewport vector.
    /// </summary>
    /// <param name="viewportVector">The viewport vector to be converted.</param>
    /// <returns></returns>
    public static Vector3 calculateWorldLocationFromViewportVector(Vector3 viewportVector)
    {
        return Camera.main.ViewportToWorldPoint(viewportVector);
    }

    /// <summary>
    /// Constructs a vector3 of the sprite's offset.
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public static float calculateSpriteOffsetX(GameObject cliff)
    {
        return cliff.GetComponent<SpriteRenderer>().sprite.bounds.size.x * cliff.GetComponent<SpriteRenderer>().transform.localScale.x / 2f;
    }

    /// <summary>
    /// Increases the difficulty of a particular game component.
    /// </summary>
    /// <param name="component">Component being effected.</param>
    /// <param name="adjustment">Level of difficulty increase.</param>
    /// <param name="threshold">The limit of the difficulty increase.</param>
    /// <returns></returns>
    public static float adjustDifficultyComponent(float component, float adjustment, float threshold)
    {
        if (component >= threshold)
        {
            return component -= adjustment;
        }

        else return component;
    }
    
    // This calculates the Sprites "Unit" width.
    public static float calculateSpriteUnitWidth(Sprite sprite)
    {
        return sprite.textureRect.width / sprite.pixelsPerUnit;
    }
    
    // This calculates the Sprites "Unit" height.
    public static float calculateSpriteUnitHeight(Sprite sprite)
    {
        return sprite.textureRect.height / sprite.pixelsPerUnit;
    }
}
