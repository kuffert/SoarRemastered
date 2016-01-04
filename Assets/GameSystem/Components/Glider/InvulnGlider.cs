using UnityEngine;
using System.Collections;

/// <summary>
/// Makes the glider invulnerable when a charge is activated. Applies
/// a visual rotation to express invulnerability.
/// </summary>
public class InvulnGlider : MonoBehaviour {
    
    private Quaternion initialRotation;

    public int rotationSpeed;

    void Start()
    {
        initialRotation = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameSystem.INVULNERABLE && !GameSystem.PAUSE)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
        }
        else
        {
            reorient();
        }
	}

    /// <summary>
    /// Reorients the glider back to its normal positioning.
    /// </summary>
    private void reorient()
    {
        transform.rotation = initialRotation;
    }
}
