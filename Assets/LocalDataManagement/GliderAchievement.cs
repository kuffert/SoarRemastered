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
    /// The first achievement is a glider earned by achieving a score above a low threshold.
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

    /// <summary>
    /// The second achievement is a glider earned by achieving a score above a medium threshold.
    /// </summary>
    [Serializable]
    public class ScoreAchievementTwo : GliderAchievement
    {
        public ScoreAchievementTwo()
        {
            unlocked = false;
            flavorText = "Earn a score of 75";
            skinIndex = 2;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 75 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// The third achievement is a glider earned by achieving a score above a high threshold.
    /// </summary>
    [Serializable]
    public class ScoreAchievementThree : GliderAchievement
    {
        public ScoreAchievementThree()
        {
            unlocked = false;
            flavorText = "Earn a score of 100";
            skinIndex = 3;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 100 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// The fourth achievement is a glider earned by achieving a score above a very high threshold.
    /// </summary>
    [Serializable]
    public class ScoreAchievementFour : GliderAchievement
    {
        public ScoreAchievementFour()
        {
            unlocked = false;
            flavorText = "Earn a score of 150";
            skinIndex = 4;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 150 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// The fifth achievement is a glider earned by achieving a score above a medium threshold with maximum boost charges.
    /// </summary>
    [Serializable]
    public class chargeAchievementOne : GliderAchievement
    {
        public chargeAchievementOne()
        {
            unlocked = false;
            flavorText = "score a 75 with max charges";
            skinIndex = 5;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 75 && GameSystem.getAvailableCharges() == 3 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// The sixth achievement is a glider earned by achieving a score above a medium threshold with maximum boost charges.
    /// </summary>
    [Serializable]
    public class chargeAchievementTwo : GliderAchievement
    {
        public chargeAchievementTwo()
        {
            unlocked = false;
            flavorText = "score a 100 with max charges";
            skinIndex = 6;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 100 && GameSystem.getAvailableCharges() == 3 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// The seventh achievement is a glider earned by achieving a score above a high threshold with maximum boost charges. 
    /// </summary>
    [Serializable]
    public class chargeAchievementThree : GliderAchievement
    {
        public chargeAchievementThree()
        {
            unlocked = false;
            flavorText = "score a 125 with max charges";
            skinIndex = 7;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 125 && GameSystem.getAvailableCharges() == 3 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }
}
