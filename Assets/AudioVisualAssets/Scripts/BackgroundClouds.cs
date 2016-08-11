using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Displays and moves the clouds in the background.
/// </summary>
public class BackgroundClouds : MonoBehaviour {

    public int numberOfClouds;
    List<Sprite> cloudSprites;
    List<GameObject> cloudObjects;
    static bool doMoveClouds;

	// Use this for initialization
	void Start () {
        cloudSprites = SpriteAssets.spriteAssets.clouds;
        cloudObjects = new List<GameObject>();
        spawnInitialClouds();
        doMoveClouds = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (doMoveClouds)
        {
            removeOutOfBoundsAndRespawn();
            moveClouds();
        }
	}

    /// <summary>
    /// Spawns the initial clouds when the level starts.
    /// </summary>
    private void spawnInitialClouds() {
        
        for (int i = 0; i < numberOfClouds; i++)
        {
            GameObject newCloud = generateNewCloud();
            float xval = newCloud.transform.position.x;
            float yval = Tools.calculateRandomYVector().y;
            newCloud.transform.position = new Vector3(xval, yval, 10f);
            cloudObjects.Add(newCloud);
        }
    }

    /// <summary>
    /// Spawns a new cloud in the background.
    /// </summary>
    private GameObject generateNewCloud()
    {
        int cloudIndex = Random.Range(0, cloudSprites.Count);
        GameObject cloud = new GameObject();
        cloud.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteComponent = cloud.GetComponent<SpriteRenderer>();
        spriteComponent.sprite = cloudSprites[cloudIndex];
        spriteComponent.sortingOrder = SortingLayers.CLOUDLAYER;
        cloud.transform.position = Tools.calculateRandomXVector();
        cloud.transform.localScale = Tools.calculateRandomYScale(.4f, .6f) + Tools.calculateRandomXScale(.4f, .6f);
        return cloud;
    }

    /// <summary>
    /// Move all clouds.
    /// </summary>
    private void moveClouds()
    {
        foreach (GameObject cloud in cloudObjects)
        {
            cloud.transform.position += new Vector3(0f, -2f * Time.deltaTime);
        }
    }

    /// <summary>
    /// Removes any out of bounds clouds and respawns new ones.
    /// </summary>
    private void removeOutOfBoundsAndRespawn()
    {
        for (int i = 0; i < numberOfClouds; i++)
        {
            if (cloudObjects[i].transform.position.y < Tools.viewToWorldPointY(-.3f))
            {
                Destroy(cloudObjects[i]);
                cloudObjects[i] = generateNewCloud();
            }
        }
    }

    /// <summary>
    /// Allows for the updating of doMoveCoulds from the GameSystem, to prevent their continued
    /// movement when the game is over.
    /// </summary>
    /// <param name="newDoMoveClouds">New value of doMoveClouds</param>
    public static void updateDoMoveClouds(bool newDoMoveClouds)
    {
        doMoveClouds = newDoMoveClouds;
    }
}
