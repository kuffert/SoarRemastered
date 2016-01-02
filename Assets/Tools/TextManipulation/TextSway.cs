using UnityEngine;
using System.Collections;

/// <summary>
/// Causes the text object to sway up and down. 
/// </summary>
public class TextSway : MonoBehaviour {

    public float swayDistance;
    private float swayCurrent;
    public float swaySpeed;

    void Start()
    {
        GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
    }
	
	void Update () {
        textSway();
    }
    
    /// <summary>
    /// Sways the text object up and down.
    /// </summary>
    private void textSway()
    {
        if (swayCurrent > swayDistance || swayCurrent < -swayDistance)
        {
            swaySpeed *= -1f;
        }
        transform.Translate(Vector3.up * Time.deltaTime * swaySpeed);
        swayCurrent += swaySpeed;
    }
}
