using UnityEngine;

/// <summary>
/// A Collidable is an object with which a player can collide.
/// The effect of a collision is dependent on the subclass type.
/// </summary>
public abstract class Collidable {

    public GameObject collidableGameObject;

    /// <summary>
    /// Generates a collidable depending on the current difficulty threshold.
    /// </summary>
    /// <param name="gameSystem">The affected Game System.</param>
    public static void generateCollidable(GameSystem gameSystem)
    {
        float thresholdRange = gameSystem.thresholdRange;
        float threshold = gameSystem.threshold();
        float yMin = gameSystem.minXCliffScale;
        float yMax = gameSystem.maxXCliffScale;

        bool spawnCoin = Random.Range(0, thresholdRange) < threshold;

        if (spawnCoin && gameSystem.spawnCoins)
        {
            Collidable newCoin = new Coin();
            gameSystem.addCollidable(newCoin);
            return;
        }

        bool spawnDoubleCliffs = Random.Range(0, thresholdRange) >= threshold;
        if (spawnDoubleCliffs)
        {
            Vector3 leftCliffScale = Tools.calculateRandomXScale(yMin, yMax);
            Vector3 rightCliffScale = new Vector3(2f - (gameSystem.getCurrentCliffGap()) - leftCliffScale.x, 1f, 1f);
            gameSystem.addCollidable(new LeftCliff(leftCliffScale));
            gameSystem.addCollidable(new RightCliff(rightCliffScale));
            return;
        }

        int cliffSelector = Random.Range(0, 2);
        Vector3 cliffScale = Tools.calculateRandomXScale(yMin, yMax);
        switch (cliffSelector)
        {
            case 0:
                gameSystem.addCollidable(new LeftCliff(cliffScale));
                break;

            case 1:
                gameSystem.addCollidable(new RightCliff(cliffScale));
                break;
        }
    }

    /// <summary>
    /// Upon collision, apply the collidable's effect to the game world.
    /// </summary>
    /// <param name="gameSystem">
    /// The affected Game System.
    /// </param>
    public abstract void applyEffect(GameSystem gameSystem, int indexOfCollidable);

    /// <summary>
    /// Determines if this Collidable has gone out of bounds.
    /// </summary>
    /// <returns></returns>
    public bool outOfBounds()
    {
        Vector3 positionAsViewPoint = Camera.main.WorldToViewportPoint(collidableGameObject.transform.position);
        return positionAsViewPoint.y < -.1f;
    }

    /// <summary>
    /// Moves the collidable a given distance.
    /// </summary>
    /// <param name="distance">Distance to be moved.</param>
    public void move(Vector3 distance)
    {
        collidableGameObject.transform.position += distance;
    }

    /// <summary>
    /// Increases score of a cliff is passed. This function has no effect
    /// if a coin is passed.
    /// </summary>
    /// <param name="gameSystem"></param>
    public abstract void increaseScoreIfCliffPassed(GameSystem gameSystem);
}

#region Coin Subclass
/// <summary>
/// A Coin collidable generates score for the player upon collision.
/// </summary>
public class Coin : Collidable
{
    public Coin ()
    {
        collidableGameObject = new GameObject();
        collidableGameObject.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.coin;
        collidableGameObject.transform.position = Tools.calculateRandomXVector();
        collidableGameObject.AddComponent<BoxCollider>().size = new Vector3(.3f, .5f, 1f);
        collidableGameObject.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.COLLIDABLELAYER;
    }

    /// <summary>
    /// Upon coin collision, raises the score and then removes
    /// the coin from the game world.
    /// </summary>
    /// <param name="gameSystem">The affected Game System.</param>
    override
    public void applyEffect(GameSystem gameSystem, int indexOfCollidable)
    {
        gameSystem.coinPickupSound.Play();
        gameSystem.removeCollidable(this);
        MonoBehaviour.Destroy(collidableGameObject);
        indexOfCollidable--;
        gameSystem.increaseScore();
    }

    override
    public void increaseScoreIfCliffPassed(GameSystem gameSystem)
    {
        return;
    }
}

#endregion Coin Subclass

#region Left Cliff Subclass
/// <summary>
/// A LeftCliff collidable causes the game to end upon collision. It shares the same
/// effect as a RightCliff, but has a different range of spawn locations and image.
/// </summary>
public class LeftCliff : Collidable
{
    public LeftCliff(Vector3 scale)
    {
        collidableGameObject = new GameObject();
        collidableGameObject.transform.localScale = scale;
        collidableGameObject.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.leftCliff;
        collidableGameObject.transform.position = Tools.calculateLeftSpawnVector(collidableGameObject);
        BoxCollider boxCollider = collidableGameObject.AddComponent<BoxCollider>();
        Vector3 boxColliderSize = collidableGameObject.GetComponent<BoxCollider>().size;
        boxCollider.size = new Vector3(boxColliderSize.x, .7f, 1f);
        boxCollider.center = new Vector3(0f, -.05f, 0f);
        collidableGameObject.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.COLLIDABLELAYER;

    }

    /// <summary>
    /// Collision with a cliff causes the game to end.
    /// </summary>
    /// <param name="gameSystem">The affected Game System.</param>
    override
    public void applyEffect(GameSystem gameSystem, int indexOfCollidable)
    {
        gameSystem.enableGameOver();
    }

    /// <summary>
    /// Increases the score of the current game instance if a cliff has been passed.
    /// </summary>
    /// <param name="gameSystem">The affected game system.</param>
    override
    public void increaseScoreIfCliffPassed(GameSystem gameSystem)
    {
        gameSystem.leftCliffText.GetComponent<CliffPassedFeedback>().fade = true;
        gameSystem.cliffPassedSound.Play();
        gameSystem.increaseScore();
    }
}

#endregion Left Cliff Subclass

#region Right Cliff Subclass
/// <summary>
/// A RightCLiff collidable causes the game to end upon collision. It shares the same
/// effect as a LeftCliff, but has a different range of spawn locations and image.
/// </summary>
public class RightCliff : Collidable
{
    public RightCliff(Vector3 scale)
    {
        collidableGameObject = new GameObject();
        collidableGameObject.transform.localScale = scale;
        collidableGameObject.AddComponent<SpriteRenderer>().sprite = SpriteAssets.spriteAssets.rightCliff;
        collidableGameObject.transform.position = Tools.calculateRightSpawnVector(collidableGameObject);
        BoxCollider boxCollider = collidableGameObject.AddComponent<BoxCollider>();
        Vector3 boxColliderSize = collidableGameObject.GetComponent<BoxCollider>().size;
        boxCollider.size = new Vector3(boxColliderSize.x, .7f, 1f);
        boxCollider.center = new Vector3(0f, -.05f, 0f);
        collidableGameObject.GetComponent<SpriteRenderer>().sortingOrder = SortingLayers.COLLIDABLELAYER;
    }

    /// <summary>
    /// Collision with a cliff causes the game to end.
    /// </summary>
    /// <param name="gameSystem">The affected Game System.</param>
    override
    public void applyEffect(GameSystem gameSystem, int indexOfCollidable)
    {
        gameSystem.enableGameOver();
    }

    /// <summary>
    /// Increases the score of the current game instance if a cliff has been passed.
    /// </summary>
    /// <param name="gameSystem">The affected game system.</param>
    override
    public void increaseScoreIfCliffPassed(GameSystem gameSystem)
    {
        gameSystem.rightCliffText.GetComponent<CliffPassedFeedback>().fade = true;
        gameSystem.cliffPassedSound.Play();
        gameSystem.increaseScore();
    }
}

#endregion Right Cliff Subclass
