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
    public bool multiframe = false;

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

    public void ADMINONLYUNLOCKACHIEVEMENT()
    {
        unlocked = true;
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
    /// The fifth achievement is a glider earned by achieving a score above an extremely high threshold
    /// </summary>
    [Serializable]
    public class ScoreAchievementFive : GliderAchievement
    {
        public ScoreAchievementFive()
        {
            unlocked = false;
            flavorText = "Earn a score of 200";
            skinIndex = 5;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 200 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// Achievement to score over an absurdly high threshold.
    /// </summary>
    [Serializable]
    public class ScoreAchievementSix : GliderAchievement
    {
        public ScoreAchievementSix()
        {
            unlocked = false;
            flavorText = "Earn a score of 250";
            skinIndex = 6;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 250 && !unlocked)
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
    public class boostAchievementOne : GliderAchievement
    {
        public boostAchievementOne()
        {
            unlocked = false;
            flavorText = "score a 75 with max boost charges";
            skinIndex = 0;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 75 && GameSystem.chargesMaxed() && !unlocked)
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
    public class boostAchievementTwo : GliderAchievement
    {
        public boostAchievementTwo()
        {
            unlocked = false;
            flavorText = "score a 100 with max boost charges";
            skinIndex = 1;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 100 && GameSystem.chargesMaxed() && !unlocked)
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
    public class boostAchievementThree : GliderAchievement
    {
        public boostAchievementThree()
        {
            unlocked = false;
            flavorText = "score a 125 with max boost charges";
            skinIndex = 2;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 125 && GameSystem.chargesMaxed() && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// Achievement to earn 150 and end with full boost charges.
    /// </summary>
    [Serializable]
    public class boostAchievementFour : GliderAchievement
    {
        public boostAchievementFour()
        {
            unlocked = false;
            flavorText = "Score a 150 with max boost charges";
            skinIndex = 3;
        }

        public override bool checkUnlockRequirements()
        {
            if (GameSystem.getScore() >= 150 && GameSystem.chargesMaxed() && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// The first special achiement is tied to Blondeterouge youtube channel.
    /// </summary>
    [Serializable]
    public class CumulativeAchievementOne : GliderAchievement
    {
        public CumulativeAchievementOne()
        {
            unlocked = false;
            flavorText = "Pass a total of 5000 cliffs";
            skinIndex = 0;
        }

        public override bool checkUnlockRequirements()
        {
            if (UserData.userData.getCumulativeCliffsPassed() > 5000 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false; 
        }
    }

    /// <summary>
    /// The second special achievement is tied to Diesel Smith.
    /// </summary>
    [Serializable]
    public class CumulativeAchievementTwo : GliderAchievement
    {
        public CumulativeAchievementTwo()
        {
            unlocked = false;
            flavorText = "Collect a total of 300 charges";
            skinIndex = 1;
        }

        public override bool checkUnlockRequirements()
        {
            if (UserData.userData.getCumulativeChargesCollected() > 300 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// Achievement to collect ten other glider skins.
    /// </summary>
    [Serializable]
    public class CumulativeAchievementThree : GliderAchievement
    {
        public CumulativeAchievementThree()
        {
            unlocked = false;
            flavorText = "Unlock ten other Gliders";
            skinIndex = 2;
            multiframe = true;
        }

        public override bool checkUnlockRequirements()
        {
            int totalUnlocks = 0;
            foreach (GliderAchievement achievement in UserData.userData.getscoreAchievements())
            {
                totalUnlocks += achievement.isUnlocked() ? 1 : 0;
            }
            foreach (GliderAchievement achievement in UserData.userData.getBoostAchievements())
            { 
                totalUnlocks += achievement.isUnlocked() ? 1 : 0;
            }
            foreach (GliderAchievement achievement in UserData.userData.getCumulativeAchievements())
            {
                totalUnlocks += achievement.isUnlocked() ? 1 : 0;
            }

            unlocked = totalUnlocks >= 10 && !unlocked;
            return unlocked;
        }
    }

    /// <summary>
    /// Achievement to collect 600 charges.
    /// </summary>
    [Serializable]
    public class CumulativeAchievementFour : GliderAchievement
    {
        public CumulativeAchievementFour()
        {
            unlocked = false;
            flavorText = "Collect a total of 600 charges";
            skinIndex = 3;
        }

        public override bool checkUnlockRequirements()
        {
            if (UserData.userData.getCumulativeChargesCollected() > 600 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }

    /// <summary>
    /// Achievement to pass 10000 cliffs.
    /// </summary>
    [Serializable]
    public class CumulativeAchievementFive : GliderAchievement
    {
        public CumulativeAchievementFive()
        {
            unlocked = false;
            flavorText = "Pass a total of 10000 cliffs";
            skinIndex = 4;
        }

        public override bool checkUnlockRequirements()
        {
            if (UserData.userData.getCumulativeCliffsPassed() > 10000 && !unlocked)
            {
                unlocked = true;
                return unlocked;
            }
            return false;
        }
    }
}



