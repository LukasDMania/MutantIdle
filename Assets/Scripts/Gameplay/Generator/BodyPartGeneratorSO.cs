using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BodyPartDataSO", menuName = "Scriptable Objects/BodyPartDataSO")]
public class BodyPartDataSO : ScriptableObject
{
    public string BodyPartName;
    public List<SpriteUpgrade> SpriteUpgrades = new List<SpriteUpgrade>();
    public Vector2 AttachmentPoint;
    public int SortingOrder;
    public BodyPartCategory BodyPartCategory;
    public bool isFlippable = true;

    public Sprite GetSpriteForLevel(int level)
    {
        if (SpriteUpgrades.Count == 0)
        {
            Debug.LogError($"No sprites set up for {name}");
            return null;
        }

        foreach (var upgrade in SpriteUpgrades.OrderByDescending(u => u.levelRequired))
        {
            if (upgrade.levelRequired <= level && upgrade.sprite != null)
            {
                return upgrade.sprite;
            }
        }
        return SpriteUpgrades[0].sprite;
    }
    public int GetNextUpgradeLevelForLevel(int level)
    {
        if (SpriteUpgrades.Count == 0)
        {
            Debug.LogError($"No sprites set up for {name}");
            return 0;
        }

        foreach (var upgrade in SpriteUpgrades.OrderBy(u => u.levelRequired))
        {
            if (upgrade.levelRequired >= level)
            {
                return upgrade.levelRequired;
            }
        }
        return -1;
    }

    public int GetPreviousUpgradeLevel(int level)
    {
        foreach (var upgrade in SpriteUpgrades.OrderByDescending(u => u.levelRequired))
        {
            if (upgrade.levelRequired <= level)
            {
                return upgrade.levelRequired;
            }
        }

        return SpriteUpgrades[0].levelRequired;
    }

}

[System.Serializable]
public class SpriteUpgrade
{
    public int levelRequired;
    public Sprite sprite;
}

public enum BodyPartCategory
{
    Head,
    Torso,
    Arms,
    Legs,
    Wings,
    Tail,

}
