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
    public GameObject scoresList;
    public GameObject soundDisabledText;
    public GameObject musicDisabledText;

    private bool showScores = false;
    private bool showOptions = false;

    void Start () {
        UserData.userData.Load();
        AudioManager.playMusic(GetComponent<AudioSource>());
        titleText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .90f, 10f));
        startText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .45f, 10f));
        scoresText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .35f, 10f));
        optionsText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .25f, 10f));
        scoresList.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .7f, 10f));
        musicDisabledText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .8f, 10f));
        soundDisabledText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .7f, 10f));
        titleText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        startText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        optionsText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresList.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        musicDisabledText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        soundDisabledText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
    }

    void Update () {
        showScoresList();
        showOptionsList();
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
                Application.LoadLevel("GameScene");
            }

            if (scoresText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showScores)
                {
                    showScores = true;
                    showOptions = false;
                    scoresText.GetComponent<TextMesh>().text = "Back";
                    optionsText.GetComponent<TextMesh>().text = "Options";

                }

                else
                {
                    showScores = false;
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                    optionsText.GetComponent<TextMesh>().text = "Options";
                }
            }

            if (optionsText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
            {
                if (!showOptions)
                {
                    showOptions = true;
                    showScores = false;
                    optionsText.GetComponent<TextMesh>().text = "Back";
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                }

                else
                {
                    showOptions = false;
                    optionsText.GetComponent<TextMesh>().text = "Options";
                    scoresText.GetComponent<TextMesh>().text = "Scores";
                }
            }

            if (soundDisabledText.GetComponent<Collider>().Raycast(ray, out hit, 100.0F) && showOptions)
            {
                UserData.userData.setSoundDisabled(!UserData.userData.getSoundDisabled());
                UserData.userData.Save();
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
            }
        }
    }
}
