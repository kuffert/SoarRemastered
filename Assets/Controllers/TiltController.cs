using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Handles input for glider tilt control.
/// </summary>
public class TiltController : MonoBehaviour
{
    public GameObject gliderSprite;
    public float movespeed = 5f;
    private float leftBound;
    private float rightBound;
    private float currentRot;
    private float maxRot = 80;
    private float steadyThreshold = .05f;
    private float steadyRotThreshold = 10f;
    
    void Start()
    {
        float gliderWidth = SpriteAssets.spriteAssets.gliderDefaultFrames[0].bounds.size.x * transform.localScale.x / 2;
        leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + gliderWidth;
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - gliderWidth;
        
    }

    void Update()
    {
        if (GameSystem.PAUSE) { return; }
        //float tilt = CrossPlatformInputManager.GetAxis("Horizontal");
        float tilt = Input.acceleration.normalized.x;
        if (!checkSteadyThreshold(tilt))
        {
            return;
        }
        float rot = tilt * maxRot - gliderSprite.transform.rotation.y;
        rot = Mathf.Abs(rot) > steadyRotThreshold ? rot : 0;
        float xPos = transform.position.x;
        if (tilt > 0 && xPos < rightBound)
        {
            transform.Translate((Vector3.right * Time.deltaTime * 7f * tilt));
        }
        if (tilt < 0 && xPos > leftBound)
        {
            transform.Translate((Vector3.right * Time.deltaTime  * 7f * tilt));
        }

        if (!GameSystem.INVULNERABLE && Mathf.Abs(tilt) > steadyThreshold)
        {
            gliderSprite.transform.Rotate(new Vector3(0, rot, 0));
        }
    }

    /// <summary>
    /// Checks if the steady threshold has been breached, allowing tilting to occur.
    /// </summary>
    /// <param name="tilt"></param>
    /// <returns></returns>
    private bool checkSteadyThreshold(float tilt)
    {
        return -steadyThreshold > tilt || tilt > steadyThreshold;
    }
}
