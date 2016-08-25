using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Collections.Generic;

/// <summary>
/// UserData contains the data from a play session,
/// i.e. anything that needs to be saved locally. This includes
/// score, audio preferences, and other options. This class uses
/// the singleton design pattern, allowing it to exist between
/// scene transitions, maintaining user's data.
/// </summary>
public class UserData : MonoBehaviour {
    public static UserData userData;

    private Score[] highScores;
    private bool soundDisabled;
    private bool musicDisabled;
    private int gliderSkinIndex;
    private int cumulativeChargesCollected;
    private int cumulativeCliffsPassed;

    private List<GliderAchievement> achievementGroupOne;

    void Awake()
    {
        if (userData == null)
        {
            DontDestroyOnLoad(gameObject);
            Load();
            if (highScores == null)
            {
                highScores = new Score[5];
                highScores[0] = new Score();
                highScores[1] = new Score();
                highScores[2] = new Score();
                highScores[3] = new Score();
                highScores[4] = new Score();
            }
            
            if (achievementGroupOne == null)
            {
                GliderAchievement defaultGlider = new GliderAchievement.DefaultAchievement();
                GliderAchievement scoreAchievementOne = new GliderAchievement.ScoreAchievementOne();
                GliderAchievement scoreAchievementTwo = new GliderAchievement.ScoreAchievementTwo();
                GliderAchievement scoreAchievementThree = new GliderAchievement.ScoreAchievementThree();
                GliderAchievement scoreAchievementFour = new GliderAchievement.ScoreAchievementFour();
                GliderAchievement scoreAchievementFive = new GliderAchievement.ScoreAchievementFive();
                GliderAchievement chargeAchievementOne = new GliderAchievement.chargeAchievementOne();
                GliderAchievement chargeAchievementTwo = new GliderAchievement.chargeAchievementTwo();
                GliderAchievement chargeAchievementThree = new GliderAchievement.chargeAchievementThree();
                GliderAchievement specialAchievementOne = new GliderAchievement.specialAcheivementOne();
                GliderAchievement specialAchievementTwo = new GliderAchievement.specialAchievementTwo();
                GliderAchievement cumulativeAchievementOne = new GliderAchievement.CumulativeAchievementOne();
                achievementGroupOne = new List<GliderAchievement>();
                achievementGroupOne.Add(defaultGlider);
                achievementGroupOne.Add(scoreAchievementOne);
                achievementGroupOne.Add(scoreAchievementTwo);
                achievementGroupOne.Add(scoreAchievementThree);
                achievementGroupOne.Add(scoreAchievementFour);
                achievementGroupOne.Add(scoreAchievementFive);
                achievementGroupOne.Add(chargeAchievementOne);
                achievementGroupOne.Add(chargeAchievementTwo);
                achievementGroupOne.Add(chargeAchievementThree);
                achievementGroupOne.Add(specialAchievementOne);
                achievementGroupOne.Add(specialAchievementTwo);
                achievementGroupOne.Add(cumulativeAchievementOne);
            }
            userData = this;
        }

        else if (userData != this)
        {
            Destroy(gameObject);    
        }
    }

    public Score[] getHighScores() { return highScores; }
    public bool getSoundDisabled() { return soundDisabled; }
    public bool getMusicDisabled() { return musicDisabled; }
    public int getGliderSkinIndex() { return gliderSkinIndex;  }
    public int getCumulativeChargesCollected() { return cumulativeChargesCollected; }
    public int getCumulativeCliffsPassed() { return cumulativeCliffsPassed; }

    public List<GliderAchievement> getAchievementGroupOne() { return achievementGroupOne; }

    public void setHighScores(Score[] highScores) { this.highScores = highScores; }
    public void setSoundDisabled(bool soundDisabled) { this.soundDisabled = soundDisabled; }
    public void setMusicDisabled(bool musicDisabled) { this.musicDisabled = musicDisabled; }
    public void setGliderSkinIndex(int gliderSkinIndex) { this.gliderSkinIndex = gliderSkinIndex; }
    public void setCUmulativeChargesCollected(int cumulativeChargesCollected) { this.cumulativeChargesCollected = cumulativeChargesCollected; }
    public void setCumulativeCliffsPassed(int cumulativeCliffsPassed) { this.cumulativeCliffsPassed = cumulativeCliffsPassed; }

    public void setAchievementGroupOne(List<GliderAchievement> achievements) { this.achievementGroupOne = achievements; }

    /// <summary>
    /// Increases the user's total cliffs passed.
    /// </summary>
    /// <param name="additionalCliffsPassed"></param>
    public void updateCumulativeCliffsPassed(int additionalCliffsPassed)
    {
        cumulativeCliffsPassed += additionalCliffsPassed;
        userData.Save();
    }

    /// <summary>
    /// Increases the user's total charges collected
    /// </summary>
    /// <param name="additionalChargesCollected"></param>
    public void updateCumulativeChargesCollected(int additionalChargesCollected)
    {
        cumulativeChargesCollected += additionalChargesCollected;
        userData.Save();
    }


    /// <summary>
    /// Return a single string of all high scores.
    /// </summary>
    /// <returns></returns>
    public string getHighScoresAsText()
    {
        string scores = "";
        int rank = 1;
        foreach (Score score in highScores)
        {
            scores += rank + ": " + score.initials + " " + score.score + " " + score.commendation + "\n";
            rank++;
        }
        return scores.Substring(0, scores.Length - 1);
    }

    /// <summary>
    /// Handles the addition of a new score. Places it according to
    /// its rank amongst other high scores.
    /// </summary>
    /// <param name="score"></param>
    public void addNewScore(int score, string initials, bool chargeUsed)
    {
        int shiftIndex = findInitialShiftIndex(score);
        if (shiftIndex != -1)
        {
            Score newScore = new Score(score, initials, chargeUsed ? "" : "[NB]");
            shiftScoresBelowIndex(newScore, shiftIndex);
        }
    }

    /// <summary>
    /// Determines where to initially shift scores, depending
    /// on the ranking of the passed score.
    /// </summary>
    /// <param name="score">The new score.</param>
    private int findInitialShiftIndex(int score)
    {
        int initialShiftIndex = -1;
        for (int i = highScores.Length - 1; i >= 0; i--)
        {
            Debug.Log(highScores.Length);
            if (score >= ((Score)highScores.GetValue(i)).score)
            {
                initialShiftIndex = i;
            }
        }
        return initialShiftIndex;
    }
    
    /// <summary>
    /// Starting at the given index, shifts all scores down.
    /// </summary>
    /// <param name="score">The new score value.</param>
    /// <param name="index">The initial index to begin shifting.</param>
    private void shiftScoresBelowIndex(Score score, int index)
    {
        if (index >= highScores.Length || index < 0)
        {
            Debug.LogError("Index out of bounds.");
            return;
        }

        Score shiftedScore;
        Score previousScore = score;
        for (int i = index; i < highScores.Length; i++)
        {
            shiftedScore = (Score)highScores.GetValue(i);
            highScores.SetValue(previousScore, i);
            previousScore = shiftedScore;
        }
    }

    /// <summary>
    /// Upon game over, check if any achievements have been earned.
    /// </summary>
    public bool checkAllAchievements()
    {
        bool finalOutcome = false;
        foreach(GliderAchievement achievement in achievementGroupOne)
        {
            bool outcome = achievement.checkUnlockRequirements();
            finalOutcome = finalOutcome ? finalOutcome : outcome;
        }
        return finalOutcome;
    }

    private void ADMINONLYUNLOCKALL()
    {
        foreach (GliderAchievement achievement in achievementGroupOne)
        {
            achievement.ADMINONLYUNLOCKACHIEVEMENT();
        }
    }

    /// <summary>
    /// Saves data from the current play session locally.
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SoarMetadata.dat");
        LocalData data = new LocalData();
        data.saveLocalData(this);
        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Loads local data into the current play session.
    /// </summary>
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SoarMetadata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SoarMetadata.dat", FileMode.Open);
            LocalData data = (LocalData)bf.Deserialize(file);
            file.Close();
            data.loadLocalData(this);
        }
    }

    /// <summary>
    /// Local data storage class. Maintains data between appplication loads.
    /// </summary>
    [Serializable]
    private class LocalData
    {
        private Score[] highScores;
        private bool soundDisabled;
        private bool musicDisabled;
        private int gliderSkinIndex;
        private int cumulativeChargesCollected;
        private int cumulativeCliffsPassed;

        private List<GliderAchievement> achievementGroupOne;

        public Score[] getHighScores() { return highScores; }
        public bool getSoundDisabled() { return soundDisabled; }
        public bool getMusicDisabled() { return musicDisabled; }
        public int getGliderSkinIndex() { return gliderSkinIndex; }
        public int getCumulativeChargesCollected() { return cumulativeChargesCollected; }
        public int getCumulativeCliffsPassed() { return cumulativeCliffsPassed; }

        public List<GliderAchievement> getAchievementGroupOne() { return achievementGroupOne; }

        public void setHighScores(Score[] highScores) { this.highScores = highScores; }
        public void setSoundDisabled(bool soundDisabled) { this.soundDisabled = soundDisabled; }
        public void setMusicDisabled(bool musicDisabled) { this.musicDisabled = musicDisabled; }
        public void setGliderSkinIndex(int gliderSkinIndex) { this.gliderSkinIndex = gliderSkinIndex; }
        public void setCUmulativeChargesCollected(int cumulativeChargesCollected) { this.cumulativeChargesCollected = cumulativeChargesCollected; }
        public void setCumulativeCliffsPassed(int cumulativeCliffsPassed) { this.cumulativeCliffsPassed = cumulativeCliffsPassed; }

        public void setAchievementGroupOne(List<GliderAchievement> achievements) { this.achievementGroupOne = achievements; }

        /// <summary>
        /// Copies the user's data to the local storage class.
        /// </summary>
        /// <param name="userData"></param>
        public void saveLocalData(UserData userData)
        {
            setHighScores(userData.getHighScores());
            setSoundDisabled(userData.getSoundDisabled());
            setMusicDisabled(userData.getMusicDisabled());
            setGliderSkinIndex(userData.getGliderSkinIndex());
            setCUmulativeChargesCollected(userData.getCumulativeChargesCollected());
            setCumulativeCliffsPassed(userData.getCumulativeCliffsPassed());

            setAchievementGroupOne(userData.getAchievementGroupOne());
        }

        /// <summary>
        /// Copies the local stored data to the user's active data.
        /// </summary>
        /// <param name="userData"></param>
        public void loadLocalData(UserData userData)
        {
            userData.setHighScores(getHighScores());
            userData.setSoundDisabled(getSoundDisabled());
            userData.setMusicDisabled(getMusicDisabled());
            userData.setGliderSkinIndex(getGliderSkinIndex());
            userData.setCUmulativeChargesCollected(getCumulativeChargesCollected());
            userData.setCumulativeCliffsPassed(getCumulativeCliffsPassed());

            userData.setAchievementGroupOne(getAchievementGroupOne());
        }
    }
}
