using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Handles input for glider tilt control.
/// </summary>
public class TiltController : MonoBehaviour
{
    public GameObject gliderSprite;
    public float movespeed = 55555f;
    private float leftBound;
    private float rightBound;
    private float currentRot;
    private float maxRot = 50f;

    void Start()
    {
        float gliderWidth = SpriteAssets.spriteAssets.gliderDefault.bounds.size.x * transform.localScale.x / 2;
        leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + gliderWidth;
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - gliderWidth;
        
    }

    void Update()
    {
        if (GameSystem.PAUSE) { return; }
        //float tilt = CrossPlatformInputManager.GetAxis("Horizontal");
        float tilt = Input.acceleration.x;
        float rot = tilt * maxRot - gliderSprite.transform.rotation.y;
        float xPos = transform.position.x;
        
        if (tilt > 0 && xPos < rightBound)
        {
            transform.Translate((Vector3.right * Time.deltaTime * tilt) * movespeed);
        }
        if (tilt < 0 && xPos > leftBound)
        {
            transform.Translate((Vector3.right * Time.deltaTime * tilt) * movespeed);
        }

        if (!GameSystem.INVULNERABLE)
        {
            gliderSprite.transform.Rotate(new Vector3(0, rot, 0));
        }
    }
}
