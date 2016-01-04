using UnityEngine;
using System.Collections;

/// <summary>
/// Gives the glider an idle animation.
/// </summary>
public class GliderIdle : MonoBehaviour {

    public bool enable;
    public float horizontalSwayDistance;
    private float horizontalSwayCurrent;
    public float horizontalSwaySpeed;
    public float verticalSwayDistance;
    private float verticalSwayCurrent;
    public float verticalSwaySpeed;

    void Update()
    {
        if (enable)
        {
            applyHorizontalIdle();
            applyVerticalIdle();
        }
    }

    /// <summary>
    /// Idles the glider horizontally.
    /// </summary>
    void applyHorizontalIdle()
    {
        if (horizontalSwayCurrent > horizontalSwayDistance || horizontalSwayCurrent < -horizontalSwayDistance)
        {
            horizontalSwaySpeed *= -1f;
        }
        transform.Translate(Vector3.right * Time.deltaTime * horizontalSwaySpeed);
        horizontalSwayCurrent += horizontalSwaySpeed;
    }

    /// <summary>
    /// Idles the glider vertically.
    /// </summary>
    void applyVerticalIdle()
    {
        if (verticalSwayCurrent > verticalSwayDistance || verticalSwayCurrent < -verticalSwayDistance)
        {
            verticalSwaySpeed *= -1f;
        }
        transform.Translate(Vector3.up * Time.deltaTime * verticalSwaySpeed);
        verticalSwayCurrent += verticalSwaySpeed;
    }
}
