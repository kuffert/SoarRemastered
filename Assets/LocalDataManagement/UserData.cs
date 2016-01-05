using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

/// <summary>
/// UserData contains the data from a play session,
/// i.e. anything that needs to be saved locally. This includes
/// score, audio preferences, and other options. This class uses
/// the singleton design pattern, allowing it to exist between
/// scene transitions, maintaining user's data.
/// </summary>
public class UserData : MonoBehaviour {
    public static UserData userData;

    private int[] highScores;
    private bool soundDisabled;
    private bool musicDisabled;

    void Awake()
    {
        if (userData == null)
        {
            DontDestroyOnLoad(gameObject);
            Load();
            highScores = highScores == null ? new int[5] : highScores;
            userData = this;
        }

        else if (userData != this)
        {
            Destroy(gameObject);
        }
    }

    public int[] getHighScores() { return highScores; }

    public bool getSoundDisabled() { return soundDisabled; }
    public bool getMusicDisabled() { return musicDisabled; }

    public void setHighScores(int[] highScores) { this.highScores = highScores; }
    public void setSoundDisabled(bool soundDisabled) { this.soundDisabled = soundDisabled; }
    public void setMusicDisabled(bool musicDisabled) { this.musicDisabled = musicDisabled; }

    /// <summary>
    /// Return a single string of all high scores.
    /// </summary>
    /// <returns></returns>
    public string getHighScoresAsText()
    {
        string scores = "";
        int rank = 1;
        foreach (int score in highScores)
        {
            scores += rank + ": " + score + "\n";
            rank++;
        }
        return scores.Substring(0, scores.Length - 1);
    }

    /// <summary>
    /// Handles the addition of a new score. Places it according to
    /// its rank amongst other high scores.
    /// </summary>
    /// <param name="score"></param>
    public void addNewScore(int newScore)
    {
        int shiftIndex = findInitialShiftIndex(newScore);
        if (shiftIndex != -1)
        {
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
            if (score >= (int)highScores.GetValue(i))
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
    private void shiftScoresBelowIndex(int score, int index)
    {
        if (index >= highScores.Length || index < 0)
        {
            Debug.LogError("Index out of bounds.");
            return;
        }

        int shiftedScore;
        int previousScore = score;
        for (int i = index; i < highScores.Length; i++)
        {
            shiftedScore = (int)highScores.GetValue(i);
            highScores.SetValue(previousScore, i);
            previousScore = shiftedScore;
        }
    }

    /// <summary>
    /// Saves data from the current play session locally.
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/localData.dat");
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
        if (File.Exists(Application.persistentDataPath + "/localData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/localData.dat", FileMode.Open);
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
        private int[] highScores;
        private bool soundDisabled;
        private bool musicDisabled;

        public int[] getHighScores() { return highScores; }
        public bool getSoundDisabled() { return soundDisabled; }
        public bool getMusicDisabled() { return musicDisabled; }

        public void setHighScores(int[] highScores) { this.highScores = highScores; }
        public void setSoundDisabled(bool soundDisabled) { this.soundDisabled = soundDisabled; }
        public void setMusicDisabled(bool musicDisabled) { this.musicDisabled = musicDisabled; }

        /// <summary>
        /// Copies the user's data to the local storage class.
        /// </summary>
        /// <param name="userData"></param>
        public void saveLocalData(UserData userData)
        {
            setHighScores(userData.getHighScores());
            setSoundDisabled(userData.getSoundDisabled());
            setMusicDisabled(userData.getMusicDisabled());
        }

        /// <summary>
        /// Copies the local stored data to the user's active data.
        /// </summary>
        /// <param name="userData"></param>
        public void loadLocalData(UserData userData)
        {
            int[] highScores = getHighScores();
            userData.setHighScores(highScores == null ? new int[5]: highScores);
            userData.setSoundDisabled(getSoundDisabled());
            userData.setMusicDisabled(getMusicDisabled());
        }
    }
}
