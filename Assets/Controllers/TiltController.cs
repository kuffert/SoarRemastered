using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Handles input for glider tilt control.
/// </summary>
public class TiltController : MonoBehaviour
{
    public float movespeed = 5f;
    private float leftBound;
    private float rightBound;

    void Start()
    {
        float gliderWidth = this.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        leftBound = -3f + gliderWidth;
        rightBound = 3f - gliderWidth;
    }

    void Update()
    {
        if (GameSystem.PAUSE) { return; }
        float tilt = CrossPlatformInputManager.GetAxis("Horizontal");
        float xPos = transform.position.x;
        if (tilt > 0 && xPos < rightBound)
        {
            transform.Translate(Vector3.right * Time.deltaTime * movespeed * tilt);
        }
        if (tilt < 0 && xPos > leftBound)
        {
            transform.Translate(Vector3.right * Time.deltaTime * movespeed * tilt);
        }
    }
}
