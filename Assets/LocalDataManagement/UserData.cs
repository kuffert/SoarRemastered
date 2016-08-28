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
    private E_AchievementType gliderEnum;
    private int cumulativeChargesCollected;
    private int cumulativeCliffsPassed;

    private GliderAchievement defaultGlider;
    private GliderAchievement scoreAchievementOne;
    private GliderAchievement scoreAchievementTwo;
    private GliderAchievement scoreAchievementThree;
    private GliderAchievement scoreAchievementFour;
    private GliderAchievement scoreAchievementFive;
    private GliderAchievement scoreAchievementSix;

    private GliderAchievement boostAchievementOne;
    private GliderAchievement boostAchievementTwo;
    private GliderAchievement boostAchievementThree;
    private GliderAchievement boostAchievementFour;

    private GliderAchievement cumulativeAchievementOne;
    private GliderAchievement cumulativeAchievementTwo;
    private GliderAchievement cumulativeAchievementThree;
    private GliderAchievement cumulativeAchievementFour;
    private GliderAchievement cumulativeAchievementFive;

    private List<GliderAchievement> scoreAchievements;
    private List<GliderAchievement> boostAchievements;
    private List<GliderAchievement> cumulativeAchievements;

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

            loadScoreAchievements();
            loadBoostAchievements();
            loadCumulativeAchievements();
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
    public E_AchievementType getGliderEnum() { return gliderEnum; }
    public int getCumulativeChargesCollected() { return cumulativeChargesCollected; }
    public int getCumulativeCliffsPassed() { return cumulativeCliffsPassed; }

    public GliderAchievement getScoreAchievementOne() { return scoreAchievementOne; }
    public GliderAchievement getScoreAchievementTwo() { return scoreAchievementTwo; }
    public GliderAchievement getScoreAchievementThree() { return scoreAchievementThree; }
    public GliderAchievement getScoreAchievementFour() { return scoreAchievementFour; }
    public GliderAchievement getScoreAchievementFive() { return scoreAchievementFive; }
    public GliderAchievement getScoreAchievementSix() { return scoreAchievementSix; }

    public GliderAchievement getBoostAchievementOne() { return boostAchievementOne; }
    public GliderAchievement getBoostAchievementTwo() { return boostAchievementTwo; }
    public GliderAchievement getBoostAchievementThree() { return boostAchievementThree; }
    public GliderAchievement getBoostAchievementFour() { return boostAchievementFour; }

    public GliderAchievement getCumulativeAchievementOne() { return cumulativeAchievementOne; }
    public GliderAchievement getCumulativeAchievementTwo() { return cumulativeAchievementTwo; }
    public GliderAchievement getCumulativeAchievementThree() { return cumulativeAchievementThree; }
    public GliderAchievement getCumulativeAchievementFour() { return cumulativeAchievementFour; }
    public GliderAchievement getCumulativeAchievementFive() { return cumulativeAchievementFive; }

    public List<GliderAchievement> getscoreAchievements() { return scoreAchievements; }
    public List<GliderAchievement> getBoostAchievements() { return boostAchievements; }
    public List<GliderAchievement> getCumulativeAchievements() { return cumulativeAchievements; }

    public void setHighScores(Score[] highScores) { this.highScores = highScores; }
    public void setSoundDisabled(bool soundDisabled) { this.soundDisabled = soundDisabled; }
    public void setMusicDisabled(bool musicDisabled) { this.musicDisabled = musicDisabled; }
    public void setGliderSkinIndex(int gliderSkinIndex) { this.gliderSkinIndex = gliderSkinIndex; }
    public void setGliderEnum(E_AchievementType gliderEnum) { this.gliderEnum = gliderEnum; }
    public void setCUmulativeChargesCollected(int cumulativeChargesCollected) { this.cumulativeChargesCollected = cumulativeChargesCollected; }
    public void setCumulativeCliffsPassed(int cumulativeCliffsPassed) { this.cumulativeCliffsPassed = cumulativeCliffsPassed; }

    public void setScoreAchievementOne(GliderAchievement scoreOne) { scoreAchievementOne = scoreOne; }
    public void setScoreAchievementTwo(GliderAchievement scoreTwo) { scoreAchievementTwo = scoreTwo; }
    public void setScoreAchievementThree(GliderAchievement scoreThree) { scoreAchievementThree = scoreThree; }
    public void setScoreAchievementFour(GliderAchievement scoreFour) { scoreAchievementFour = scoreFour; }
    public void setScoreAchievementFive(GliderAchievement scoreFive) { scoreAchievementFive = scoreFive; }
    public void setScoreAchievementSix(GliderAchievement scoreSix) { scoreAchievementSix = scoreSix; }

    public void setBoostAchievementOne(GliderAchievement boostOne) { boostAchievementOne = boostOne; }
    public void setBoostAchievementTwo(GliderAchievement boostTwo) { boostAchievementTwo = boostTwo; }
    public void setBoostAchievementThree(GliderAchievement boostThree) { boostAchievementThree = boostThree; }
    public void setBoostAchievementFour(GliderAchievement boostFour) { boostAchievementFour = boostFour; }

    public void setCumulativeAchievementOne(GliderAchievement cumulativeOne) { cumulativeAchievementOne = cumulativeOne; }
    public void setCumulativeAchievementTwo(GliderAchievement cumulativeTwo) { cumulativeAchievementTwo = cumulativeTwo; }
    public void setCumulativeAchievementThree(GliderAchievement cumulativeThree) { cumulativeAchievementThree = cumulativeThree; }
    public void setCumulativeAchievementFour(GliderAchievement cumulativeFour) { cumulativeAchievementFour = cumulativeFour; }
    public void setCumulativeAchievementFive(GliderAchievement cumulativeFive) { cumulativeAchievementFive = cumulativeFive; }

    /// <summary>
    /// Loads score achievements, or creates a new one if the achievement loaded as null.
    /// </summary>
    private void loadScoreAchievements()
    {
        defaultGlider = defaultGlider == null? new GliderAchievement.DefaultAchievement() : defaultGlider;
        scoreAchievementOne = scoreAchievementOne == null? new GliderAchievement.ScoreAchievementOne() : scoreAchievementOne;
        scoreAchievementTwo = scoreAchievementTwo == null? new GliderAchievement.ScoreAchievementTwo() : scoreAchievementTwo;
        scoreAchievementThree = scoreAchievementThree == null? new GliderAchievement.ScoreAchievementThree() : scoreAchievementThree;
        scoreAchievementFour = scoreAchievementFour == null? new GliderAchievement.ScoreAchievementFour() : scoreAchievementFour;
        scoreAchievementFive = scoreAchievementFive == null? new GliderAchievement.ScoreAchievementFive() : scoreAchievementFive;
        scoreAchievementSix = scoreAchievementSix == null? new GliderAchievement.ScoreAchievementSix() : scoreAchievementSix;
        scoreAchievements = new List<GliderAchievement>();
        scoreAchievements.Add(defaultGlider);
        scoreAchievements.Add(scoreAchievementOne);
        scoreAchievements.Add(scoreAchievementTwo);
        scoreAchievements.Add(scoreAchievementThree);
        scoreAchievements.Add(scoreAchievementFour);
        scoreAchievements.Add(scoreAchievementFive);
        scoreAchievements.Add(scoreAchievementSix);
    }

    /// <summary>
    /// Loads boost achievements, or creates a new one if the achievement loaded was null.
    /// </summary>
    private void loadBoostAchievements()
    {
        boostAchievementOne = boostAchievementOne == null? new GliderAchievement.boostAchievementOne() : boostAchievementOne;
        boostAchievementTwo = boostAchievementTwo == null? new GliderAchievement.boostAchievementTwo() : boostAchievementTwo;
        boostAchievementThree = boostAchievementThree == null? new GliderAchievement.boostAchievementThree() : boostAchievementThree;
        boostAchievementFour = boostAchievementFour == null? new GliderAchievement.boostAchievementFour(): boostAchievementFour;
        boostAchievements = new List<GliderAchievement>();
        boostAchievements.Add(boostAchievementOne);
        boostAchievements.Add(boostAchievementTwo);
        boostAchievements.Add(boostAchievementThree);
        boostAchievements.Add(boostAchievementFour);
    } 

    private void loadCumulativeAchievements()
    {
        cumulativeAchievementOne = cumulativeAchievementOne == null? new GliderAchievement.CumulativeAchievementOne() : cumulativeAchievementOne;
        cumulativeAchievementTwo = cumulativeAchievementTwo == null? new GliderAchievement.CumulativeAchievementTwo() : cumulativeAchievementTwo;
        cumulativeAchievementThree = cumulativeAchievementThree == null? new GliderAchievement.CumulativeAchievementThree() : cumulativeAchievementThree;
        cumulativeAchievementFour = cumulativeAchievementFour == null? new GliderAchievement.CumulativeAchievementFour() : cumulativeAchievementFour;
        cumulativeAchievementFive = cumulativeAchievementFive == null? new GliderAchievement.CumulativeAchievementFive() : cumulativeAchievementFive;
        cumulativeAchievements = new List<GliderAchievement>();
        cumulativeAchievements.Add(cumulativeAchievementOne);
        cumulativeAchievements.Add(cumulativeAchievementTwo);
        cumulativeAchievements.Add(cumulativeAchievementThree);
        cumulativeAchievements.Add(cumulativeAchievementFour);
        cumulativeAchievements.Add(cumulativeAchievementFive);
    }

    /// <summary>
    /// Retrieves the sprite list associated with the saved enum.
    /// </summary>
    /// <returns></returns>
    public List<List<Sprite>> getSpriteFramesByEnum()
    {
        switch (gliderEnum)
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
    public List<Sprite> checkAllAchievements()
    {
        List<Sprite> unlockedGliders = new List<Sprite>();
        foreach(GliderAchievement achievement in scoreAchievements)
        {
            bool outcome = achievement.checkUnlockRequirements();
            if (outcome)
            {
                unlockedGliders.Add(SpriteAssets.spriteAssets.scoreGliders[scoreAchievements.IndexOf(achievement)][0]);
            }
        }

        foreach(GliderAchievement achievement in boostAchievements)
        {
            bool outcome = achievement.checkUnlockRequirements();
            if (outcome)
            {
                unlockedGliders.Add(SpriteAssets.spriteAssets.boostGliders[boostAchievements.IndexOf(achievement)][0]);
            }
        }

        foreach(GliderAchievement achievement in cumulativeAchievements)
        {
            bool outcome = achievement.checkUnlockRequirements();
            if (outcome)
            {
                unlockedGliders.Add(SpriteAssets.spriteAssets.cumulativeGliders[cumulativeAchievements.IndexOf(achievement)][0]);
            }
        }
        return unlockedGliders;
    }

    private void ADMINONLYUNLOCKALL()
    {
        foreach (GliderAchievement achievement in scoreAchievements)
        {
            achievement.ADMINONLYUNLOCKACHIEVEMENT();
        }
        foreach(GliderAchievement achievement in boostAchievements)
        {
            achievement.ADMINONLYUNLOCKACHIEVEMENT();
        }
        foreach(GliderAchievement achievement in cumulativeAchievements)
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
            loadScoreAchievements();
            loadBoostAchievements();
            loadCumulativeAchievements();
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
        private E_AchievementType gliderEnum;
        private int cumulativeChargesCollected;
        private int cumulativeCliffsPassed;

        private GliderAchievement defaultGlider;
        private GliderAchievement scoreAchievementOne;
        private GliderAchievement scoreAchievementTwo;
        private GliderAchievement scoreAchievementThree;
        private GliderAchievement scoreAchievementFour;
        private GliderAchievement scoreAchievementFive;
        private GliderAchievement scoreAchievementSix;

        private GliderAchievement boostAchievementOne;
        private GliderAchievement boostAchievementTwo;
        private GliderAchievement boostAchievementThree;
        private GliderAchievement boostAchievementFour;

        private GliderAchievement cumulativeAchievementOne;
        private GliderAchievement cumulativeAchievementTwo;
        private GliderAchievement cumulativeAchievementThree;
        private GliderAchievement cumulativeAchievementFour;
        private GliderAchievement cumulativeAchievementFive;

        private List<GliderAchievement> scoreAchievements;
        private List<GliderAchievement> boostAchievements;
        private List<GliderAchievement> cumulativeAchievements;

        public Score[] getHighScores() { return highScores; }
        public bool getSoundDisabled() { return soundDisabled; }
        public bool getMusicDisabled() { return musicDisabled; }
        public int getGliderSkinIndex() { return gliderSkinIndex; }
        public E_AchievementType getGliderEnum() { return gliderEnum; }
        public int getCumulativeChargesCollected() { return cumulativeChargesCollected; }
        public int getCumulativeCliffsPassed() { return cumulativeCliffsPassed; }

        public GliderAchievement getScoreAchievementOne() { return scoreAchievementOne; }
        public GliderAchievement getScoreAchievementTwo() { return scoreAchievementTwo; }
        public GliderAchievement getScoreAchievementThree() { return scoreAchievementThree; }
        public GliderAchievement getScoreAchievementFour() { return scoreAchievementFour; }
        public GliderAchievement getScoreAchievementFive() { return scoreAchievementFive; }
        public GliderAchievement getScoreAchievementSix() { return scoreAchievementSix; }

        public GliderAchievement getBoostAchievementOne() { return boostAchievementOne; }
        public GliderAchievement getBoostAchievementTwo() { return boostAchievementTwo; }
        public GliderAchievement getBoostAchievementThree() { return boostAchievementThree; }
        public GliderAchievement getBoostAchievementFour() { return boostAchievementFour; }

        public GliderAchievement getCumulativeAchievementOne() { return cumulativeAchievementOne; }
        public GliderAchievement getCumulativeAchievementTwo() { return cumulativeAchievementTwo; }
        public GliderAchievement getCumulativeAchievementThree() { return cumulativeAchievementThree; }
        public GliderAchievement getCumulativeAchievementFour() { return cumulativeAchievementFour; }
        public GliderAchievement getCumulativeAchievementFive() { return cumulativeAchievementFive; }

        public void setHighScores(Score[] highScores) { this.highScores = highScores; }
        public void setSoundDisabled(bool soundDisabled) { this.soundDisabled = soundDisabled; }
        public void setMusicDisabled(bool musicDisabled) { this.musicDisabled = musicDisabled; }
        public void setGliderSkinIndex(int gliderSkinIndex) { this.gliderSkinIndex = gliderSkinIndex; }
        public void setGliderEnum(E_AchievementType gliderEnum) { this.gliderEnum = gliderEnum; }
        public void setCUmulativeChargesCollected(int cumulativeChargesCollected) { this.cumulativeChargesCollected = cumulativeChargesCollected; }
        public void setCumulativeCliffsPassed(int cumulativeCliffsPassed) { this.cumulativeCliffsPassed = cumulativeCliffsPassed; }

        public void setScoreAchievementOne(GliderAchievement scoreOne) { scoreAchievementOne = scoreOne; }
        public void setScoreAchievementTwo(GliderAchievement scoreTwo) { scoreAchievementTwo = scoreTwo; }
        public void setScoreAchievementThree(GliderAchievement scoreThree) { scoreAchievementThree = scoreThree; }
        public void setScoreAchievementFour(GliderAchievement scoreFour) { scoreAchievementFour = scoreFour; }
        public void setScoreAchievementFive(GliderAchievement scoreFive) { scoreAchievementFive = scoreFive; }
        public void setScoreAchievementSix(GliderAchievement scoreSix) { scoreAchievementSix = scoreSix; }

        public void setBoostAchievementOne(GliderAchievement boostOne) { boostAchievementOne = boostOne; }
        public void setBoostAchievementTwo(GliderAchievement boostTwo) { boostAchievementTwo = boostTwo; }
        public void setBoostAchievementThree(GliderAchievement boostThree) { boostAchievementThree = boostThree; }
        public void setBoostAchievementFour(GliderAchievement boostFour) { boostAchievementFour = boostFour; }

        public void setCumulativeAchievementOne(GliderAchievement cumulativeOne) { cumulativeAchievementOne = cumulativeOne; }
        public void setCumulativeAchievementTwo(GliderAchievement cumulativeTwo) { cumulativeAchievementTwo = cumulativeTwo; }
        public void setCumulativeAchievementThree(GliderAchievement cumulativeThree) { cumulativeAchievementThree = cumulativeThree; }
        public void setCumulativeAchievementFour(GliderAchievement cumulativeFour) { cumulativeAchievementFour = cumulativeFour; }
        public void setCumulativeAchievementFive(GliderAchievement cumulativeFive) { cumulativeAchievementFive = cumulativeFive; }

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
            setGliderEnum(userData.getGliderEnum());
            setCUmulativeChargesCollected(userData.getCumulativeChargesCollected());
            setCumulativeCliffsPassed(userData.getCumulativeCliffsPassed());

            setScoreAchievementOne(userData.getScoreAchievementOne());
            setScoreAchievementTwo(userData.getScoreAchievementTwo());
            setScoreAchievementThree(userData.getScoreAchievementThree());
            setScoreAchievementFour(userData.getScoreAchievementFour());
            setScoreAchievementFive(userData.getScoreAchievementFive());
            setScoreAchievementSix(userData.getScoreAchievementSix());

            setBoostAchievementOne(userData.getBoostAchievementOne());
            setBoostAchievementTwo(userData.getBoostAchievementTwo());
            setBoostAchievementThree(userData.getBoostAchievementThree());
            setBoostAchievementFour(userData.getBoostAchievementFour());

            setCumulativeAchievementOne(userData.getCumulativeAchievementOne());
            setCumulativeAchievementTwo(userData.getCumulativeAchievementTwo());
            setCumulativeAchievementThree(userData.getCumulativeAchievementThree());
            setCumulativeAchievementFour(userData.getCumulativeAchievementFour());
            setCumulativeAchievementFive(userData.getCumulativeAchievementFive());
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
            userData.setGliderEnum(getGliderEnum());
            userData.setCUmulativeChargesCollected(getCumulativeChargesCollected());
            userData.setCumulativeCliffsPassed(getCumulativeCliffsPassed());

            userData.setScoreAchievementOne(getScoreAchievementOne());
            userData.setScoreAchievementTwo(getScoreAchievementTwo());
            userData.setScoreAchievementThree(getScoreAchievementThree());
            userData.setScoreAchievementFour(getScoreAchievementFour());
            userData.setScoreAchievementFive(getScoreAchievementFive());
            userData.setScoreAchievementSix(getScoreAchievementSix());

            userData.setBoostAchievementOne(getBoostAchievementOne());
            userData.setBoostAchievementTwo(getBoostAchievementTwo());
            userData.setBoostAchievementThree(getBoostAchievementThree());
            userData.setBoostAchievementFour(getBoostAchievementFour());

            userData.setCumulativeAchievementOne(getCumulativeAchievementOne());
            userData.setCumulativeAchievementTwo(getCumulativeAchievementTwo());
            userData.setCumulativeAchievementThree(getCumulativeAchievementThree());
            userData.setCumulativeAchievementFour(getCumulativeAchievementFour());
            userData.setCumulativeAchievementFive(getCumulativeAchievementFive());
        }
    }
}
