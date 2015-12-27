using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameSystem handles all information pertaining to a single game instance. 
/// </summary>
public class GameSystem : MonoBehaviour {
    public GameObject player;
    public GameObject scoreText;
    public GameObject finalScoreText;
    public GameObject restartText;
    public GameObject mainMenuText;
    public int maxCollidables;
    public int thresholdRange;
    public int initialThreshold;
    public float initialSpawnRate;
    public float maxSpawnRate;
    public float minXCliffScale;
    public float maxXCliffScale;
    public float minCliffGap;
    public float maxCliffGap;
    public Vector3 initialSpeed;
    public Vector3 maxSpeed;

    private int currentThreshhold;
    private float currentSpawnRate;
    private float timePassed;
    private float currentCliffGap;
    private Vector3 currentSpeed;
    private int score;
    private bool gameOver;
    private List<Collidable> collidables;

    #region Startup And Update
    void Awake ()
    {
        collidables = new List<Collidable>();

        // Places all text at screen-fitting positions.
        scoreText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .95f, 10f));
        finalScoreText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .9f, 10f));
        restartText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .45f, 10f));
        mainMenuText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .35f, 10f));
        scoreText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        finalScoreText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        restartText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        mainMenuText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
    
        // Places the player at a screen-fitting position.
        player.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .075f, 10f));
    }

	void Start ()
    {
        gameOver = false;
        currentSpawnRate = initialSpawnRate;
        currentSpeed = initialSpeed;
        currentThreshhold = initialThreshold;
        currentCliffGap = maxCliffGap;
        score = 0;
        updateScore();
	}
	
	void Update ()
    {
        if (gameOver)
        {
            delegateNavigationFromTouch();
            return;
        }
        updateScore();
        spawnCollidables();
        moveCollidables();
        removeOutOfBoundsCollidables();
        checkCollisions();
        increaseDifficulty();
    }

    #endregion Startup And Update

    #region Accesssors and Public Functionality

    /// <summary>
    /// Retrieves the current gap between cliffs.
    /// </summary>
    /// <returns></returns>
    public float getCurrentCliffGap()
    {
        return currentCliffGap;
    }

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

    /// <summary>
    /// Enables the end of game. This displays and adds interactability with 
    /// navigational buttons, and sets the state of the game to ended.
    /// </summary>
    public void enableGameOver()
    {
        gameOver = true;
        //currentSpeed = new Vector3(0f, 0f, 0f);
        scoreText.GetComponent<TextMesh>().text = "Game Over";
        finalScoreText.GetComponent<TextMesh>().text = "Final Score: " + score;
        finalScoreText.AddComponent<BoxCollider>();
        restartText.GetComponent<TextMesh>().text = "Restart";
        restartText.AddComponent<BoxCollider>();
        mainMenuText.GetComponent<TextMesh>().text = "Main Menu:";
        mainMenuText.AddComponent<BoxCollider>();
    }

    #endregion Accessors and Public Functionality

    #region Game System Functionality

    /// <summary>
    /// Displays the player's current score.
    /// </summary>
    private void updateScore()
    {
        scoreText.GetComponent<TextMesh>().text = "Score: " + score;
    }

    /// <summary>
    /// Increase the game difficulty based on the user's score
    /// </summary>
    private void increaseDifficulty()
    {
        //TODO: Increase difficulty based on score
    }

    /// <summary>
    /// Detects if there are any collisions between player and collidables. If there
    /// is, apply the effect of that collidable.
    /// </summary>
    private void checkCollisions()
    {
        Bounds playerBounds = player.GetComponent<BoxCollider>().bounds;

        for (int i = 0; i < collidables.Count; i++)
        {
            if (collidables[i].collidableGameObject.GetComponent<BoxCollider>().bounds.Intersects(playerBounds))
            {
                collidables[i].applyEffect(this, i);
            }
        }
    }

    #endregion Game System Functionality

    #region Collidable Management

    /// <summary>
    /// Spawns new collidables according to Game System constrains.
    /// </summary>
    private void spawnCollidables()
    {
        if (collidables.Count <= maxCollidables && Time.time >= timePassed)
        {
            timePassed += currentSpawnRate;
            Collidable.generateCollidable(this);
        }
    }


    /// <summary>
    /// Moves all collidable objects by the current speed.
    /// </summary>
    private void moveCollidables()
    {   
        foreach(Collidable collidable in collidables)
        {
            collidable.move(currentSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Destroys any collidables that have gone out of bounds.
    /// </summary>
    private void removeOutOfBoundsCollidables()
    {
        for (int i = 0; i < collidables.Count; i++)
        {
            if (collidables[i].outOfBounds())
            {
                Destroy(collidables[i].collidableGameObject);
                collidables.Remove(collidables[i]);
                i--;
            }
        }
    }

    #endregion Collidable Management

    #region Endgame

    /// <summary>
    /// Detects user input and navigates according to the button they've pressed.
    /// </summary>
    private void delegateNavigationFromTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (restartText.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                // Need to save here
                Application.LoadLevel("GameScene");
            }

            if (mainMenuText.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                // Need to save here
                Application.LoadLevel("MainMenu");
            }
        }
    }

    #endregion Endgame

}
