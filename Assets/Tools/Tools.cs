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
    public static Vector3 calculateRandomYScale(float minY, float maxY)
    {
        float yScale = Random.Range(minY*10f, maxY*10f) / 10f;
        return new Vector3(1f, yScale, 1f);
    }

    /// <summary>
    /// Constructs a Vector3 with a random Left-Side spawn location.
    /// </summary>
    /// <returns></returns>
    public static Vector3 calculateLeftSpawnVector()
    {
        float xLoc = Random.Range(-5, 5) / 100f;
        return Camera.main.ViewportToWorldPoint(new Vector3(xLoc, 1.1f, 10f));
    }

    /// <summary>
    /// Constructs a Vector3 with a random Right-Side spawn location.
    /// </summary>
    /// <returns></returns>
    public static Vector3 calculateRightSpawnVector()
    {
        float xLoc = Random.Range(95, 105) / 100f;
        return Camera.main.ViewportToWorldPoint(new Vector3(xLoc, 1.1f, 10f));
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
}
