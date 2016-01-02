using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject titleText;
    public GameObject startText;
    public GameObject scoresText;

    void Start () {
        titleText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .95f, 10f));
        startText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .45f, 10f));
        scoresText.transform.position = Tools.calculateWorldLocationFromViewportVector(new Vector3(.5f, .35f, 10f));
        titleText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        startText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;
        scoresText.GetComponent<MeshRenderer>().sortingOrder = SortingLayers.TEXTLAYER;

    }
	
	void Update () {
        delegateNavigationFromTouch();
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

            //TODO: Scores
        }
    }
}
