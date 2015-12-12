using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameSystem handles all information pertaining to a single game instance. 
/// </summary>
public class GameSystem : MonoBehaviour {
    public GameObject player;
    public GameObject scoreText;
    public GameObject restartText;
    public GameObject mainMenuText;
    public int maxCollidables;
    public int thresholdRange;
    public int initialThreshold;
    public float initialSpawnRate;
    public float maxSpawnRate;
    public float minYCliffScale;
    public float maxYCliffScale;
    public Vector3 initialSpeed;
    public Vector3 maxSpeed;

    private int currentThreshhold;
    private float currentSpawnRate;
    private Vector3 currentSpeed;
    private int score;
    private bool gameOver;
    private List<Collidable> collidables;

    #region Startup And Update
    void Awake ()
    {
        collidables = new List<Collidable>();
    }

	void Start ()
    {
        gameOver = false;
        currentSpawnRate = initialSpawnRate;
        currentSpeed = initialSpeed;
        currentThreshhold = initialThreshold;
	}
	
	void Update ()
    {
        if (gameOver)
        {
            displayGameOverTexts();
            delegateNavigationFromTouch();
        }
	}

    #endregion Startup And Update

    #region Accesssors

    /// <summary>
    /// Removes the given collidable from the Game System's private
    /// list of collidables.
    /// </summary>
    /// <param name="collidable">The Collidable to be removed.</param>
    public void removeCollidable(Collidable collidable)
    {
        collidables.Remove(collidable);
    }

    /// <summary>
    /// Adds the given collidable to the Game System's private list
    /// of collidables.
    /// </summary>
    /// <param name="collidable">The Collidable to be added.</param>
    public void addCollidable(Collidable collidable)
    {
        collidables.Add(collidable);
    }

    /// <summary>
    /// Increases the score of the current game instance.
    /// </summary>
    public void increaseScore()
    {
        score++;
    }

    /// <summary>
    /// Sets gameOver to true, ending the game.
    /// </summary>
    public void endGame()
    {
        gameOver = true;
    }

    /// <summary>
    /// Retrieves the current threshold.
    /// </summary>
    /// <returns></returns>
    public int threshold()
    {
        return currentThreshhold;
    }

    #endregion Accessors

    #region Collidable Management

    /// <summary>
    /// Moves all collidable objects by the current speed.
    /// </summary>
    public void moveCollidables()
    {
        foreach(Collidable collidable in collidables)
        {
            collidable.move(currentSpeed);
        }
    }

    /// <summary>
    /// Destroys any collidables that have gone out of bounds.
    /// </summary>
    public void removeOutOfBoundsCollidables()
    {
        foreach(Collidable collidable in collidables)
        {
            if (collidable.outOfBounds())
            {
                Destroy(collidable.collidableGameObject);
                collidables.Remove(collidable);
            }
        }
    }

    #endregion Collidable Management

}
