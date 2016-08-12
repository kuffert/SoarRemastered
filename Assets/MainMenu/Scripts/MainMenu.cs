using UnityEngine;
using System.Collections;

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
    public GameObject scoresList;
    public GameObject creditsList;
    public GameObject soundDisabledText;
    public GameObject musicDisabledText;
    public AudioSource menuSelectSound;
    public AudioSource menuBackSound;
    public AudioSource musicSelectSound;
    public AudioSource startGameSound;

    private bool showScores = false;
    private bool showOptions = false;
    private bool showCredits = false;
    private bool beginGameReady = false;

    void Start () {
        UserData.userData.Load();
        AudioManager.playMusic(GetComponent<AudioSource>());
        titleText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .90f, 10f));
        startText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .45f, 10f));
        scoresText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .35f, 10f));
        optionsText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .25f, 10f));
        scoresList.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        musicDisabledText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .8f, 10f));
        soundDisabledText.transform.position = Tools.viewToWorldVector(new Vector3(.5f, .7f, 10f));
        titleText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        startText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        optionsText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        creditsText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresList.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        musicDisabledText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        soundDisabledText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        creditsList.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
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
            creditsList.GetComponent<TextMesh>().text = "Freesound Credits:\nFins\nJalastram\nDland\nBertrof\nPlasterbrain\nTheMusicalNomad";
        }

        else
        {
            creditsList.GetComponent<TextMesh>().text = "";
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
                    showOptions = false;
                    showCredits = false;
                    scoresText.GetComponent<TextMesh>().text = "Back";
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                }

                else
                {
                    AudioManager.playSound(menuBackSound);
                    showScores = false;
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                }
            }

            if (optionsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showOptions)
                {
                    AudioManager.playSound(menuSelectSound);
                    showOptions = true;
                    showScores = false;
                    showCredits = false;
                    optionsText.GetComponent<TextMesh>().text = "Back";
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                }

                else
                {
                    AudioManager.playSound(menuBackSound);
                    showOptions = false;
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                }
            }

            if (creditsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showCredits)
                {
                    AudioManager.playSound(menuSelectSound);
                    showOptions = false;
                    showScores = false;
                    showCredits = true;
                    startText.GetComponent<TextMesh>().text = "";
                    optionsText.GetComponent<TextMesh>().text = "";
                    scoresText.GetComponent<TextMesh>().text = "";
                    creditsText.GetComponent<TextMesh>().text = "Back";
                }
                else
                {
                    AudioManager.playSound(menuBackSound);
                    showCredits = false;
                    startText.GetComponent<TextMesh>().text = "Start";
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    creditsText.GetComponent<TextMesh>().text = "Credits";
                }
            }

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
