using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls the main menu, including game start, scores,
/// and options.
/// </summary>
public class MainMenu : MonoBehaviour {

    public GameObject titleText;
    public GameObject startText;
    public GameObject scoresText;
    public GameObject optionsText;
    public GameObject creditsText;
    public GameObject gliderSkinsText;
    public GameObject scoresList;
    public GameObject creditsList;
    public GameObject soundDisabledText;
    public GameObject musicDisabledText;
    public GameObject glider;
    public GameObject gliderDescriptionText;
    public GameObject chargesCollectedText;
    public GameObject cliffsPassedText;
    public GameObject categoryLabel;
    public GameObject gliderSelector;
    public AudioSource menuSelectSound;
    public AudioSource menuBackSound;
    public AudioSource musicSelectSound;
    public AudioSource startGameSound;

    private bool showScores = false;
    private bool showOptions = false;
    private bool showCredits = false;
    private bool beginGameReady = false;
    private bool showGliderSkins = false;
    private List<GameObject> gliderButtons;
    private List<GameObject> achievementCategories;
    private E_AchievementType selectedCategory = E_AchievementType.Score;

    void Start () {
        UserData.userData.Load();
        AudioManager.playMusic(GetComponent<AudioSource>());
        GliderAnimation.setFrameCycle(UserData.userData.getSpriteFramesByEnum()[UserData.userData.getGliderSkinIndex()]);
        titleText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .90f, 10f));
        startText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .45f, 10f));
        scoresText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .35f, 10f));
        optionsText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .25f, 10f));
        gliderSkinsText.transform.position = Tools.viewToWorldVector(new Vector3(.25f, .1f, 10f));
        gliderDescriptionText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .65f, 10f));
        creditsText.transform.position = Tools.viewToWorldVector(new Vector3(.75f, .1f, 10f));
        scoresList.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        musicDisabledText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .8f, 10f));
        soundDisabledText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        chargesCollectedText.transform.position = Tools.viewToWorldVector(new Vector3(.25f, .45f, 10f));
        cliffsPassedText.transform.position = Tools.viewToWorldVector(new Vector3(.75f, .45f, 10f));
        categoryLabel.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        titleText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        startText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        optionsText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        creditsText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        gliderSkinsText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        gliderDescriptionText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresList.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        musicDisabledText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        soundDisabledText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        creditsList.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        chargesCollectedText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        cliffsPassedText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        categoryLabel.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        gliderButtons = new List<GameObject>();
        achievementCategories = new List<GameObject>();
    }

    void Update () {
        beginGameAfterAudioComplete();
        showScoresList();
        showOptionsList();
        showCreditsImage();
        delegateNavigationFromTouch();
	}

    /// <summary>
    /// Shows the list of scores if showScores is enabled.
    /// </summary>
    private void showScoresList()
    {
        if (showScores)
        {
            scoresList.GetComponent<TextMesh>().text = UserData.userData.getHighScoresAsText();
        }
        else
        {
            scoresList.GetComponent<TextMesh>().text = "";
        }
    }

    /// <summary>
    /// Show the list of options if showOptions is enabled.
    /// </summary>
    private void showOptionsList()
    {
        if (showOptions)
        {
            soundDisabledText.GetComponent<TextMesh>().text = UserData.userData.getSoundDisabled() ? "Sounds Off" : "Sounds On";
            musicDisabledText.GetComponent<TextMesh>().text = UserData.userData.getMusicDisabled()? "Music Off" : "Music On";
        }
        else
        {
            soundDisabledText.GetComponent<TextMesh>().text = "";
            musicDisabledText.GetComponent<TextMesh>().text = "";
        }
    }

    /// <summary>
    /// Show the credits image if the button has been selected.
    /// </summary>
    private void showCreditsImage()
    {
        if (showCredits)
        {
            creditsList.GetComponent<TextMesh>().text = "Freesound Credits:\nFins\nJalastram\nDland\nBertrof\nPlasterbrain\nTheMusicalNomad\nLittleRobotSoundFactory";
        }

        else
        {
            creditsList.GetComponent<TextMesh>().text = "";
        }
    }


    /// <summary>
    /// Shows the selectable categories
    /// </summary>
    public void showAchievementCategories()
    {
        GameObject scoreCat = new GameObject();
        GameObject boostCat = new GameObject();
        GameObject cumulativeCat = new GameObject();

        scoreCat.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.scoreCategorySprite;
        boostCat.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.boostCategorySprite;
        cumulativeCat.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.cumulativeCategorySprite;

        scoreCat.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        boostCat.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        cumulativeCat.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.TEXTLAYER;

        scoreCat.AddComponent<BoxCollider>();
        boostCat.AddComponent<BoxCollider>();
        cumulativeCat.AddComponent<BoxCollider>();

        scoreCat.transform.position = Tools.viewToWorldVector(new Vector3(.25f, .8f, 10f));
        boostCat.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .8f, 10f));
        cumulativeCat.transform.position = Tools.viewToWorldVector(new Vector3(.75f, .8f, 10f));

        achievementCategories.Add(scoreCat);
        achievementCategories.Add(boostCat);
        achievementCategories.Add(cumulativeCat);

        categoryLabel.GetComponent<TextMesh>().text = selectedCategory.ToString() + " Achievements";
        setSelectedCategorySprite();
    }

    /// <summary>
    /// Show the locked and unlocked glider skins.
    /// </summary>
    private void showAchievements()
    {
        List<GliderAchievement> achievements = getAchievementsFromEnum();
        List<List<Sprite>> gliderSprites = getSpritesFromEnum();
        float xLoc = .18f;
        float yLoc = .5f;
        UserData.userData.Load();
        for (int i = 0; i < achievements.Count; i++)
        {
            GameObject achievementSprite = new GameObject();
            if (achievements[i].isUnlocked())
            {
                achievementSprite.AddComponent<SpriteRenderer>().sprite = gliderSprites[achievements[i].skinIndex][0];
            }
            else
            {
                achievementSprite.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.lockedGliderFrames[0];
            }
            achievementSprite.AddComponent<BoxCollider>();
            achievementSprite.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
            achievementSprite.transform.position = Tools.viewToWorldVector(new Vector3(xLoc, yLoc, 10.0f));
            xLoc = xLoc >= .8f ? .18f : xLoc + .22f;
            yLoc = i >= 3 ? .3f : .5f;
            gliderButtons.Add(achievementSprite);
        }
    }

    /// <summary>
    /// Retrieves the list of achievements associated with the achievement type enum.
    /// </summary>
    /// <returns></returns>
    public List<GliderAchievement> getAchievementsFromEnum()
    {
        switch (selectedCategory)
        {
            case E_AchievementType.Score:
                return (UserData.userData.getscoreAchievements());
                
            case E_AchievementType.Boost:
                return (UserData.userData.getBoostAchievements());
                
            case E_AchievementType.Cumulative:
                return (UserData.userData.getCumulativeAchievements());
                
            default:
                Debug.Log("shit");
                return (UserData.userData.getscoreAchievements());     
        }
    }

    /// <summary>
    /// Retrieves the list of sprite frames based on the category enum.
    /// </summary>
    /// <returns></returns>
    public List<List<Sprite>> getSpritesFromEnum()
    {
        switch (selectedCategory)
        {
            case E_AchievementType.Score:
                return (SpriteAssets.spriteAssets.scoreGliders);

            case E_AchievementType.Boost:
                return (SpriteAssets.spriteAssets.boostGliders);

            case E_AchievementType.Cumulative:
                return (SpriteAssets.spriteAssets.cumulativeGliders);

            default:
                return (SpriteAssets.spriteAssets.scoreGliders);
        }
    }

    /// <summary>
    /// Returns the selected version of the category image.
    /// </summary>
    /// <returns></returns>
    public void setSelectedCategorySprite()
    {
        switch (selectedCategory)
        {
            case E_AchievementType.Score:
                achievementCategories[0].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.selectedScoreCategorySprite);
                achievementCategories[1].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.boostCategorySprite);
                achievementCategories[2].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.cumulativeCategorySprite);
                break;

            case E_AchievementType.Boost:
                achievementCategories[0].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.scoreCategorySprite);
                achievementCategories[1].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.selectedBoostCategorySprite);
                achievementCategories[2].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.cumulativeCategorySprite);
                break;

            case E_AchievementType.Cumulative:
                achievementCategories[0].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.scoreCategorySprite);
                achievementCategories[1].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.boostCategorySprite);
                achievementCategories[2].GetComponent<SpriteRenderer>().sprite = (SpriteAssets.spriteAssets.selectedCumulativeCategorySprite);
                break;
        }
    }

    /// <summary>
    /// Unshow the locked and unlocked glider skins.
    /// </summary>
    private void unshowAchievements()
    {
        gliderSelector.GetComponent<SpriteRenderer>().sprite = null;
        foreach (GameObject achievement in gliderButtons)
        {
            Destroy(achievement);
        }
        gliderButtons.Clear();
    }

    /// <summary>
    /// unshow the category buttons.
    /// </summary>
    private void unshowCategories()
    {
        foreach(GameObject category in achievementCategories)
        {
            Destroy(category);
        }
        categoryLabel.GetComponent<TextMesh>().text = "";
        achievementCategories.Clear();
    }

    /// <summary>
    /// Updates the currently selected category
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="hit"></param>
    private void updateSelectedCategory(Ray ray, RaycastHit hit)
    {
        for (int i = 0; i < achievementCategories.Count; i++)
        {
            if (achievementCategories[i].GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                selectedCategory = (E_AchievementType)i;
                gliderDescriptionText.GetComponent<TextMesh>().text = "";
                categoryLabel.GetComponent<TextMesh>().text = selectedCategory.ToString() + " Achievements";
                gliderSelector.GetComponent<SpriteRenderer>().sprite = null;
                setSelectedCategorySprite();
                unshowAchievements();
                showAchievements();
            }
        }
    }

    /// <summary>
    /// Updates the currently selected skin.
    /// </summary>
    private void updateSelectedSkin(Ray ray, RaycastHit hit)
    {
        List<GliderAchievement> achievements = getAchievementsFromEnum();
        List<List<Sprite>> sprites = getSpritesFromEnum();
        
        for (int i = 0; i < gliderButtons.Count; i++)
        {
            if (gliderButtons[i].GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                gliderDescriptionText.GetComponent<TextMesh>().text = achievements[i].flavorText;
                gliderSelector.GetComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.gliderSelector;
                gliderSelector.transform.position = gliderButtons[i].transform.position;
                if (achievements[i].isUnlocked())
                {
                    UserData.userData.setGliderSkinIndex(i);
                    UserData.userData.setGliderEnum(selectedCategory);
                    GliderAnimation.setFrameCycle(sprites[achievements[i].skinIndex]);
                    UserData.userData.Save();
                }
            }
        }
    }

    /// <summary>
    /// Allows the game to start once the start audio has completed
    /// </summary>
    private void beginGameAfterAudioComplete()
    {
        if (beginGameReady && !startGameSound.isPlaying)
        {
            Application.LoadLevel("GameScene");
        }
    }

    /// <summary>
    /// Determines where to navigate depending on the button touched. 
    /// </summary>
    private void delegateNavigationFromTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (startText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                
                AudioManager.playSound(startGameSound);
                beginGameReady = true;
            }

            if (scoresText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showScores)
                {
                    AudioManager.playSound(menuSelectSound);
                    showScores = true;
                    scoresText.GetComponent<TextMesh>().text = "Back";
                    chargesCollectedText.GetComponent<TextMesh>().text = "Charges\nCollected\n" + UserData.userData.getCumulativeChargesCollected();
                    cliffsPassedText.GetComponent<TextMesh>().text = "Cliffs\nPassed\n" + UserData.userData.getCumulativeCliffsPassed();
                    startText.GetComponent<TextMesh>().text = "";
                    startText.GetComponent<BoxCollider>().enabled = false;
                    optionsText.GetComponent<TextMesh>().text = "";
                    optionsText.GetComponent<BoxCollider>().enabled = false;
                    creditsText.GetComponent<TextMesh>().text = "";
                    creditsText.GetComponent<BoxCollider>().enabled = false;
                    gliderSkinsText.GetComponent<TextMesh>().text = "";
                    gliderSkinsText.GetComponent<BoxCollider>().enabled = false;
                }

                else
                {
                    AudioManager.playSound(menuBackSound);
                    showScores = false;
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    chargesCollectedText.GetComponent<TextMesh>().text = "";
                    cliffsPassedText.GetComponent<TextMesh>().text = "";
                    startText.GetComponent<TextMesh>().text = "Start";
                    startText.GetComponent<BoxCollider>().enabled = true;
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    optionsText.GetComponent<BoxCollider>().enabled = true;
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                    creditsText.GetComponent<BoxCollider>().enabled = true;
                    gliderSkinsText.GetComponent<TextMesh>().text = "Gliders";
                    gliderSkinsText.GetComponent<BoxCollider>().enabled = true;
                }
            }

            if (optionsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showOptions)
                {
                    AudioManager.playSound(menuSelectSound);
                    showOptions = true;
                    optionsText.GetComponent<TextMesh>().text = "Back";
                    startText.GetComponent<TextMesh>().text = "";
                    startText.GetComponent<BoxCollider>().enabled = false;
                    scoresText.GetComponent<TextMesh>().text = "";
                    scoresText.GetComponent<BoxCollider>().enabled = false;
                    creditsText.GetComponent<TextMesh>().text = "";
                    creditsText.GetComponent<BoxCollider>().enabled = false;
                    gliderSkinsText.GetComponent<TextMesh>().text = "";
                    gliderSkinsText.GetComponent<BoxCollider>().enabled = false;
                }

                else
                {
                    AudioManager.playSound(menuBackSound);
                    showOptions = false;
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    startText.GetComponent<TextMesh>().text = "Start";
                    startText.GetComponent<BoxCollider>().enabled = true;
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    scoresText.GetComponent<BoxCollider>().enabled = true;
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                    creditsText.GetComponent<BoxCollider>().enabled = true;
                    gliderSkinsText.GetComponent<TextMesh>().text = "Gliders";
                    gliderSkinsText.GetComponent<BoxCollider>().enabled = true;
                }
            }

            if (creditsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showCredits)
                {
                    AudioManager.playSound(menuSelectSound);
                    showCredits = true;
                    creditsText.GetComponent<TextMesh>().text = "Back";
                    startText.GetComponent<TextMesh>().text = "";
                    startText.GetComponent<BoxCollider>().enabled = false;
                    optionsText.GetComponent<TextMesh>().text = "";
                    optionsText.GetComponent<BoxCollider>().enabled = false;
                    scoresText.GetComponent<TextMesh>().text = "";
                    scoresText.GetComponent<BoxCollider>().enabled = false;
                    gliderSkinsText.GetComponent<TextMesh>().text = "";
                    gliderSkinsText.GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    AudioManager.playSound(menuBackSound);
                    showCredits = false;
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                    startText.GetComponent<TextMesh>().text = "Start";
                    startText.GetComponent<BoxCollider>().enabled = true;
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    optionsText.GetComponent<BoxCollider>().enabled = true;
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    scoresText.GetComponent<BoxCollider>().enabled = true;
                    gliderSkinsText.GetComponent<TextMesh>().text = "Gliders";
                    gliderSkinsText.GetComponent<BoxCollider>().enabled = true;
                }
            }

            if (gliderSkinsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showGliderSkins)
                {
                    AudioManager.playSound(menuSelectSound);
                    showGliderSkins = true;
                    gliderSkinsText.GetComponent<TextMesh>().text = "back";
                    startText.GetComponent<TextMesh>().text = "";
                    startText.GetComponent<BoxCollider>().enabled = false;
                    optionsText.GetComponent<TextMesh>().text = "";
                    optionsText.GetComponent<BoxCollider>().enabled = false;
                    scoresText.GetComponent<TextMesh>().text = "";
                    scoresText.GetComponent<BoxCollider>().enabled = false;
                    creditsText.GetComponent<TextMesh>().text = "";
                    creditsText.GetComponent<BoxCollider>().enabled = false;
                    showAchievementCategories();
                    showAchievements();
                }

                else
                {
                    AudioManager.playSound(menuBackSound);
                    showGliderSkins = false;
                    gliderDescriptionText.GetComponent<TextMesh>().text = "";
                    gliderSkinsText.GetComponent<TextMesh>().text = "Gliders";
                    startText.GetComponent<TextMesh>().text = "Start";
                    startText.GetComponent<BoxCollider>().enabled = true;
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    optionsText.GetComponent<BoxCollider>().enabled = true;
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    scoresText.GetComponent<BoxCollider>().enabled = true;
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                    creditsText.GetComponent<BoxCollider>().enabled = true;
                    unshowAchievements();
                    unshowCategories();
                }
            }

            updateSelectedCategory(ray, hit);
            updateSelectedSkin(ray, hit);

            if (soundDisabledText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F) && showOptions)
            {
                UserData.userData.setSoundDisabled(!UserData.userData.getSoundDisabled());
                UserData.userData.Save();
                AudioManager.playSound(musicSelectSound);
            }

            if (musicDisabledText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F) && showOptions)
            {
                UserData.userData.setMusicDisabled(!UserData.userData.getMusicDisabled());
                UserData.userData.Save();
                AudioSource backgroundMusic = GetComponent<AudioSource>();
                if (UserData.userData.getMusicDisabled())
                {
                    AudioManager.stopAudio(backgroundMusic);
                }
                else
                {
                    AudioManager.playMusic(backgroundMusic);
                }
                AudioManager.playSound(musicSelectSound);
            }
        }
    }
}
