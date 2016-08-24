using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Contains data for highscores, including the score value, the initials of the obtainer, and any special commendations
/// </summary>
[Serializable]
public class Score {

    public int score;
    public string initials;
    public string commendation;

    public Score()
    {
        score = 0;
        initials = "";
        commendation = "";
    }

    public Score(int score, string initials, string commendation)
    {
        this.score = score;
        this.initials = initials;
        this.commendation = commendation;
    }
}
