using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// Glider Achievement is an abstracted wrapper class that is inherited by the different potential achievements
/// that players can unlock. Each unlock has a different requirement, and a different skin reward. 
/// </summary>
[Serializable]
public abstract class GliderAchievement {
    private bool unlocked;
    public string flavorText;
    public int skinIndex;

    /// <summary>
    /// Checks if an achievement has been unlocked. Returns false if default or if a new achievement was not earned.
    /// </summary>
    /// <returns></returns>
    public abstract bool checkUnlockRequirements();

    /// <summary>
    /// Returns whether the skin has been unlocked or not.
    /// </summary>
    /// <returns></returns>
    public bool isUnlocked()
    {
        return unlocked;
    }

    /// <summary>
    /// The Default Achievement is the standard glider that is unlocked from the start. It has no requirements
    /// </summary>
    [Serializable]
    public class DefaultAchievement : GliderAchievement
    {
        public DefaultAchievement()
        {
            unlocked = true;
            flavorText = "Default Glider";
            skinIndex = 0;
        }

        public override bool checkUnlockRequirements()
        {
            unlocked = true;
            return false;
        }
    }

    /// <summary>
    /// The first achievement is a glider earned by achieving a score above a particular threshold.
    /// </summary>
    [Serializable]
    public class ScoreAchievementOne : GliderAchievement
    {
        public ScoreAchievementOne()
        {
            unlocked = false;
            flavorText = "Earn a score of 50";
            skinIndex = 1;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 50 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }


}
