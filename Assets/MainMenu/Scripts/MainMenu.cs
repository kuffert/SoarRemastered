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

    void Start () {
        UserData.userData.Load();
        AudioManager.playMusic(GetComponent<AudioSource>());
        GliderAnimation.setFrameCycle(SpriteAssets.spriteAssets.allGliders[UserData.userData.getGliderSkinIndex()]);
        titleText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .90f, 10f));
        startText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .45f, 10f));
        scoresText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .35f, 10f));
        optionsText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .25f, 10f));
        gliderSkinsText.transform.position = Tools.viewToWorldVector(new Vector3(.25f, .1f, 10f));
        gliderDescriptionText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .8f, 10f));
        creditsText.transform.position = Tools.viewToWorldVector(new Vector3(.75f, .1f, 10f));
        scoresList.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        musicDisabledText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .8f, 10f));
        soundDisabledText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        chargesCollectedText.transform.position = Tools.viewToWorldVector(new Vector3(.25f, .45f, 10f));
        cliffsPassedText.transform.position = Tools.viewToWorldVector(new Vector3(.75f, .45f, 10f));
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
        gliderButtons = new List<GameObject>();
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
    /// Show the locked and unlocked glider skins.
    /// </summary>
    private void showAchievements()
    {
        float xLoc = .18f;
        float yLoc = .7f;
        UserData.userData.Load();
        for (int i = 0; i < UserData.userData.getAchievementGroupOne().Count; i++)
        {
            GameObject achievementSprite = new GameObject();
            if (UserData.userData.getAchievementGroupOne()[i].isUnlocked())
            {
                achievementSprite.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.allGliders[UserData.userData.getAchievementGroupOne()[i].skinIndex][0];
            }
            else
            {
                achievementSprite.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.lockedGliderFrames[0];
            }
            achievementSprite.AddComponent<BoxCollider>();
            achievementSprite.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
            achievementSprite.transform.position = Tools.viewToWorldVector(new Vector3(xLoc, yLoc, 10.0f));
            xLoc = xLoc >= .8f ? .18f : xLoc + .22f;
            yLoc = i >= 3 ? .5f : .7f;
            yLoc = i >= 7 ? .3f : yLoc;
            gliderButtons.Add(achievementSprite);
        }
    }

    /// <summary>
    /// Unshow the locked and unlocked glider skins.
    /// </summary>
    private void unshowAchievements()
    {
        foreach(GameObject achievement in gliderButtons)
        {
            Destroy(achievement);
        }
        gliderButtons.Clear();
    }

    /// <summary>
    /// Updates the currently selected skin.
    /// </summary>
    private void updateSelectedSkin(Ray ray, RaycastHit hit)
    {
        for(int i = 0; i < gliderButtons.Count; i++)
        {
            if (gliderButtons[i].GetComponent<Collider>().Raycast(ray, out hit, 100.0f))
            {
                gliderDescriptionText.GetComponent<TextMesh>().text = UserData.userData.getAchievementGroupOne()[i].flavorText;
                if (UserData.userData.getAchievementGroupOne()[i].isUnlocked())
                {
                    UserData.userData.setGliderSkinIndex(i);
                    GliderAnimation.setFrameCycle(SpriteAssets.spriteAssets.allGliders[UserData.userData.getAchievementGroupOne()[i].skinIndex]);
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
                }
            }

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
