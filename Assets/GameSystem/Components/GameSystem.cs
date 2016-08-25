using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameSystem handles all information pertaining to a single game instance. 
/// </summary>
public class GameSystem : MonoBehaviour {

    public static bool PAUSE = false;
    public static bool INVULNERABLE = false;
    public bool spawnCoins;

    public GameObject player;
    public GameObject playerSprite;
    public GameObject scoreText;
    public GameObject finalScoreText;
    public GameObject restartText;
    public GameObject mainMenuText;
    public GameObject leftCliffText;
    public GameObject rightCliffText;
    public GameObject alertText;
    public GameObject userInputText;

    private TouchScreenKeyboard keyboard;

    public AudioSource cliffPassedSound;
    public AudioSource coinPickupSound;
    public AudioSource gameOverSound;
    public AudioSource achievementSound;
    public AudioSource chargeSound;
    public AudioSource chargePickupSound;
    public AudioSource chargeFailSound;
    public AudioSource restartSound;
    public AudioSource menuReturnSound;

    public int maxDifficultyScore;

    public int maxCharges;
    public int maxCollidables;
    public int lastCliffSpawned;

    public float invulnDuration;
    public float thresholdRange;
    public float initialThreshold;
    public float chargeThreshold;
    public float minThreshold;

    public float initialSpawnRate;
    public float maxSpawnRate;

    public float minXCliffScale;
    public float maxXCliffScale;
    public float minCliffGap;
    public float maxCliffGap;

    public float gapBetweenCharges;

    public Vector3 initialSpeed;
    public Vector3 maxSpeed;

    private static int availableCharges;
    private static int maximumCharges;
    private bool chargeUsed;
    public int cliffsPassed;
    public int chargesCollected;
    private static bool specialAchievementOneCheck;
    private static float invulnDurationTime;
    public static float remainingInvulnTime;
    private float currentThreshhold;
    private float currentSpawnRate;
    private float timePassed;
    private float currentCliffGap;
    private float storedSpawnRate;
    private float thresholdDifficultyIncrease;
    private float spawnRateDifficultyIncrease;
    private float cliffGapDifficultyIncrease;
    private float speedDifficultyIncrease;
    private float chargeSpriteWidth;
    private Vector3 currentSpeed;
    private Vector3 storedSpeed;
    private static int score;
    private bool gameOver;
    private bool chargeSpawnedLast;
    private bool restartGameReady;
    private bool spawnImmediately;
    private List<Collidable> collidables;
    private List<GameObject> charges;
    private string initialsInput = "AAA";
    private bool saved = false;

    #region Startup And Update
    void Awake ()
    {
        // Places all text at screen-fitting positions.
        scoreText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .95f, 10f));
        finalScoreText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .9f, 10f));
        restartText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .45f, 10f));
        mainMenuText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .35f, 10f));
        leftCliffText.transform.position = Tools.viewToWorldVector(new Vector3(.1f, .075f, 10f));
        rightCliffText.transform.position = Tools.viewToWorldVector(new Vector3(.9f, .075f, 10f));
        alertText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .8f, 10f));
        userInputText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .6f, 10f));
        scoreText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        finalScoreText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        restartText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        mainMenuText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        leftCliffText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        rightCliffText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        alertText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        userInputText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        
        // Places the player at a screen-fitting position.
        player.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .15f, 10f));
    }

	void Start ()
    {
        GliderAnimation.doAnimation = true;
        GliderAnimation.setFrameCycle(SpriteAssets.spriteAssets.allGliders[UserData.userData.getGliderSkinIndex()]);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        UserData.userData.Load();
        AudioManager.playMusic(GetComponent<AudioSource>());

        collidables = new List<Collidable>();
        charges = new List<GameObject>();
        populateCharges();
        maximumCharges = maxCharges;
        specialAchievementOneCheck = false;
        cliffsPassed = 0;
        chargesCollected = 0;

        gameOver = false;
        PAUSE = false;
        INVULNERABLE = false;
        chargeSpawnedLast = false;
        restartGameReady = false;
        spawnImmediately = false;
        chargeUsed = false;

        availableCharges = maxCharges;
        currentSpawnRate = initialSpawnRate;
        currentSpeed = initialSpeed; 
        currentThreshhold = initialThreshold;
        currentCliffGap = maxCliffGap;
        invulnDurationTime = invulnDuration;

        score = 0;  
        timePassed = 0f;

        storedSpawnRate = initialSpawnRate;
        storedSpeed = initialSpeed;

        float leftBound = Tools.viewToWorldPointX(0f);
        chargeSpriteWidth = Tools.worldToViewPointX(leftBound + SpriteAssets.spriteAssets.charge.bounds.size.x);

        thresholdDifficultyIncrease = (initialThreshold - minThreshold) / maxDifficultyScore * 5;
        spawnRateDifficultyIncrease = (initialSpawnRate - maxSpawnRate) / maxDifficultyScore * 5;
        cliffGapDifficultyIncrease = (maxCliffGap - minCliffGap) / maxDifficultyScore * 5;
        speedDifficultyIncrease = (maxSpeed.y - initialSpeed.y) / maxDifficultyScore * 5;

        updateScore();
	}
	
	void Update ()
    {
        if (gameOver)
        {
            restartGameWhenAudioComplete();
            delegateNavigationFromTouch();
            if ( keyboard != null && keyboard.active)
            {
                keyboard.text = keyboard.text.Length > 3? keyboard.text.Substring(0, 3) : keyboard.text;
                initialsInput = keyboard.text.Length > 3? keyboard.text.Substring(0, 3) : keyboard.text;
                userInputText.GetComponent<TextMesh>().text = initialsInput;
            }

            if (keyboard != null && keyboard.done && !saved)
            {
                initialsInput = keyboard.text;
                userInputText.GetComponent<TextMesh>().text = initialsInput;
                UserData.userData.addNewScore(score, initialsInput, chargeUsed);
                UserData.userData.Save();
                saved = true;
            }
            return;
        }
        updateScore();
        spawnCollidables();
        moveCollidables();
        moveCharges();
        removeOutOfBoundsCollidables();
        checkCollisions();
        useChargeFromTouch();
        expireCharge();
    }

    #endregion Startup And Update

    #region Accesssors and Public Functionality

    /// <summary>
    /// Retrieve the score of the game.
    /// </summary>
    /// <returns></returns>
    public static int getScore()
    {
        return score;
    }

    /// <summary>
    /// Retrieve the special achievement one check.
    /// </summary>
    /// <returns></returns>
    public static bool checkSpecialAchievementOne()
    {
        return specialAchievementOneCheck;
    }

    /// <summary>
    /// Retrieves the remaining invuln time
    /// </summary>
    /// <returns></returns>
    public static float retrieveRemainingInvulnTimeRatio()
    {
        return (remainingInvulnTime - Time.timeSinceLevelLoad) / invulnDurationTime;
       
    }

    /// <summary>
    /// Returns true if the player currently has their charges maxed
    /// </summary>
    /// <returns></returns>
    public static bool chargesMaxed()
    {
        return availableCharges == maximumCharges;
    }

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
        increaseDifficulty();
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
    public float threshold()
    {
        return currentThreshhold;
    }

    /// <summary>
    /// Obtaining a charge replenishes the players charges, if 
    /// the current amount is less than the maximum.
    /// </summary>
    public void pickupCharge()
    {
        if (availableCharges < maxCharges)
        {
            popAlert("+1 Boost");
            availableCharges += 1;
            charges[availableCharges - 1].GetComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.charge;
        }

        else
        {
            popAlert("Charges Maxed: +5");
            for (int i = 0; i < 5; i++)
            {
                increaseScore();
            }
        }
    }

    /// <summary>
    /// Sets the chargeSpawnedLast boolean to a new value. True
    /// if the last object spawned was a charge, false otherwise.
    /// </summary>
    /// <param name="spawnedLast"></param>
    public void setChargeSpawnedLast(bool spawnedLast)
    {
        chargeSpawnedLast = spawnedLast;
    }

    /// <summary>
    /// Retrieves the chargeSpawnedLast boolean.
    /// </summary>
    /// <returns></returns>
    public bool getChargeSpawnedLast()
    {
        return chargeSpawnedLast;
    }

    /// <summary>
    /// Enables the end of game. This displays and adds interactability with 
    /// navigational buttons, and sets the state of the game to ended.
    /// </summary>
    public void enableGameOver()
    {
        gameOver = true;
        PAUSE = true;
        scoreText.GetComponent<TextMesh>().text = "Game Over";
        finalScoreText.GetComponent<TextMesh>().text = "Final Score:" + score;
        UserData.userData.updateCumulativeCliffsPassed(cliffsPassed);
        UserData.userData.updateCumulativeChargesCollected(chargesCollected);
        bool anyAchievementsEarned = UserData.userData.checkAllAchievements();
        if (anyAchievementsEarned)
        {
            scoreText.GetComponent<TextMesh>().fontSize = 100;
            scoreText.GetComponent<TextMesh>().text = "New Glider Unlocked!";
            AudioManager.playSound(achievementSound);

        }
        else
        {

            AudioManager.playSound(gameOverSound);
        }
        finalScoreText.AddComponent<BoxCollider>();
        restartText.GetComponent<TextMesh>().text = "Restart";
        restartText.AddComponent<BoxCollider>();
        mainMenuText.GetComponent<TextMesh>().text = "Main Menu";
        mainMenuText.AddComponent<BoxCollider>();
        BackgroundClouds.updateDoMoveClouds(false);
        playerSprite.AddComponent<CycleFrames>().sprites = SpriteAssets.spriteAssets.deathAnimationSprites;
        playerSprite.GetComponent<CycleFrames>().framesPerSecond = 6;
        playerSprite.GetComponent<CycleFrames>().loop = false;
        GliderAnimation.doAnimation = false;
        wipeCharges();
        
        if (((Score)UserData.userData.getHighScores().GetValue(4)).score < score)
        {
            keyboard = TouchScreenKeyboard.Open(initialsInput, TouchScreenKeyboardType.Default);
        }
    }

    #endregion Accessors and Public Functionality

    #region Game System Functionality

    /// <summary>
    /// Restarts the game once the audio has stopped playing.
    /// </summary>
    private void restartGameWhenAudioComplete()
    {
        if (restartGameReady && !restartSound.isPlaying)
        {
            Application.LoadLevel("GameScene");
        }
    }

    /// <summary>
    /// Displays the player's current score.
    /// </summary>
    private void updateScore()
    {
        scoreText.GetComponent<TextMesh>().text = "Score:" + score;
    }

    /// <summary>
    /// Increase the game difficulty based on the user's score.
    /// </summary>
    private void increaseDifficulty()
    {
        if (score % 5 == 0 && score != 0)
        {
            currentSpawnRate = Tools.adjustDifficultyComponent(currentSpawnRate, spawnRateDifficultyIncrease, maxSpawnRate);
            currentCliffGap = Tools.adjustDifficultyComponent(currentCliffGap, cliffGapDifficultyIncrease, minCliffGap);
            currentThreshhold = Tools.adjustDifficultyComponent(currentThreshhold, thresholdDifficultyIncrease, minThreshold);

            if (currentSpeed.y > maxSpeed.y)
            {
                currentSpeed += new Vector3(0, speedDifficultyIncrease, 0);
            }

            /*
            Debug.Log(currentSpawnRate);
            Debug.Log(currentCliffGap);
            Debug.Log(currentThreshhold);
            Debug.Log(currentSpeed.y);
            */
        }
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

    /// <summary>
    /// Pop up the alert text with an alert message.
    /// </summary>
    /// <param name="alertMessage">The displayed message</param>
    private void popAlert(string alertMessage)
    {
        alertText.GetComponent<TextMesh>().text = alertMessage;
        alertText.GetComponent<TextFadeOut>().fade = true;
    }

    #endregion Game System Functionality

    #region Charge System Functionality

    /// <summary>
    /// Populates the initial list of charges.
    /// </summary>
    private void populateCharges()
    {
        for (int i = 0; i < maxCharges; i++)
        {
            GameObject charge = new GameObject();
            charge.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.charge;
            charge.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
            charge.transform.localScale = new Vector3(.5f, .5f, 1f);
            charges.Add(charge);
        }   
    }

    /// <summary>
    /// Consumes a charge, granting invulnerability and a speed boost.
    /// </summary>
    private void consumeCharge()
    {
        if (availableCharges > 0)
        {
            chargeUsed = true;
            AudioManager.playSound(chargeSound);
            popAlert("Boost Activated");
            INVULNERABLE = true;
            remainingInvulnTime = Time.timeSinceLevelLoad + invulnDuration;
            charges[availableCharges - 1].GetComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.emptyCharge;
            availableCharges = (availableCharges <= 1) ? 0 : availableCharges - 1;
            storedSpawnRate = currentSpawnRate;
            storedSpeed = currentSpeed; currentSpeed *= 2f;
            cliffsPassed = 0;
        }
        else
        {
            AudioManager.playSound(chargeFailSound);
            popAlert("No Boost Charges!");
        }
    }

    /// <summary>
    /// Retrieves the remaining time on the invuln timer.
    /// </summary>
    /// <returns></returns>
    public float getRemainingInvulnTime()
    {
        float remainingTime = remainingInvulnTime - Time.timeSinceLevelLoad;
        return remainingTime <= 0 ? 0 : remainingTime;
    }

    /// <summary>
    /// Increases the remaining invulnerability time.
    /// </summary>
    /// <param name="increase"></param>
    public void increaseRemainingInvulnTime(float increase)
    {
        remainingInvulnTime += increase;
    }

    /// <summary>
    /// If a charge is currently in effect and its timer is active, maintain invunerability.
    /// Otherwise, disable it. 
    /// </summary>
    private void expireCharge()
    {
        if (INVULNERABLE && Time.timeSinceLevelLoad > remainingInvulnTime) 
        {
            INVULNERABLE = false;
            currentSpawnRate = storedSpawnRate;
            currentSpeed = storedSpeed;
            Vector3 position = player.transform.position;
            position.z = 0f;
            player.transform.position = position;
        }
    }

    /// <summary>
    /// Move all charges.
    /// </summary>
    private void moveCharges()
    {
        float totalChargeWidth = calculateTotalChargeWidth();
        float initialPlacement = Tools.worldToViewPointX(player.transform.position.x) - .5f * totalChargeWidth;
        float yLoc = Tools.worldToViewPointY(player.transform.position.y) - .055f;
        int chargeNumber = 0;
        foreach(GameObject charge in charges)
        {
            float xLoc = initialPlacement + (chargeNumber * gapBetweenCharges) + (chargeNumber * chargeSpriteWidth) + (.5f * chargeSpriteWidth);
            charge.transform.position = Tools.viewToWorldVector(new Vector3(xLoc, yLoc, 10f));
            chargeNumber++;
        }
    }

    /// <summary>
    /// Calculates the width of all charges total.
    /// </summary>
    /// <returns></returns>
    private float calculateTotalChargeWidth()
    {
        return (maxCharges - 1) * gapBetweenCharges + (maxCharges * chargeSpriteWidth);
    }


    /// <summary>
    /// Used to wipe player charges on death and destroy the sprite images.
    /// </summary>
    private void wipeCharges()
    {
        foreach(GameObject charge in charges)
        {
            Destroy(charge);
        }
        charges.Clear();
    }

    #endregion Charge System Functionality

    #region Achievement Functionality


    #endregion Achievement Functionality

    #region Collidable Management

    /// <summary>
    /// Spawns new collidables according to Game System constrains.
    /// </summary>
    private void spawnCollidables()
    {
        if (INVULNERABLE && !spawnImmediately)
        {
            spawnImmediately = true;
            timePassed += getRemainingInvulnTime();
        }

        if (!INVULNERABLE && spawnImmediately)
        {
            timePassed = Time.timeSinceLevelLoad;
            timePassed += currentSpawnRate;
            Collidable.generateCollidable(this);
            spawnImmediately = false;
        }

        if (collidables.Count <= maxCollidables && Time.timeSinceLevelLoad >= timePassed && !spawnImmediately)
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
                collidables[i].increaseScoreIfCliffPassed(this);
                Destroy(collidables[i].collidableGameObject);
                collidables.Remove(collidables[i]);
                i--;
            }
        }
    }

    #endregion Collidable Management

    #region User Input Management

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
                AudioManager.playSound(restartSound);
                restartGameReady = true;
            }

            if (mainMenuText.GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                AudioManager.playSound(menuReturnSound);
                Application.LoadLevel("MainMenu");
            }
        }
    }

    /// <summary>
    /// When a user taps the screen, a charge should be consumed.
    /// </summary>
    private void useChargeFromTouch()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeSinceLevelLoad >= 1)
        {
            if (PAUSE)
            {
                return;
            }

            else if (INVULNERABLE)
            {
                AudioManager.playSound(chargeFailSound);
            }

            else
            {
                consumeCharge();
            }
        }
    }

    #endregion User Input Management

}
    