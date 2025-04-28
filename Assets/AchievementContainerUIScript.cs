using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementContainerUIScript : MonoBehaviour
{
    public GameObject AchievementUIPrefab;
    public Transform ParentTransform;
    public GameObject MainContainer;

    public ScrollRect ScrollRect;

    public float TransparancyAmount;

    public bool UIIsOpen;


    public List<GameObject> AchievementPrefabs;
    void Start()
    {
        UIIsOpen = true;
        if (AchievementPrefabs == null)
        {
            AchievementPrefabs = new List<GameObject>();
        }

        AchievementPrefabs.Clear();

        foreach (var achievement in AchievementManager.Instance.AchievementList)
        {
            GameObject g = Instantiate(AchievementUIPrefab, ParentTransform);
            g.name = achievement.AchievementId;

            TextMeshProUGUI[] tmpros = g.GetComponentsInChildren<TextMeshProUGUI>();
            if (tmpros.Length == 2)
            {
                tmpros[0].text = achievement.AchievementDescription;
                tmpros[1].text = achievement.AchievementRewardText;
            }

            Image image = g.GetComponentInChildren<Image>();
            if (image != null)
            {
                Color newColor = image.color;
                newColor.a = achievement.AchievementUnlocked ? 1f : TransparancyAmount;
                image.color = newColor;
            }

            if (!AchievementPrefabs.Contains(AchievementUIPrefab))
            {
                AchievementPrefabs.Add(g);
            }
        }
        ScrollRect.verticalNormalizedPosition = 1f;
        CloseUI();
    }

    public void UpdateAchievementUI()
    {
        Dictionary<string, GameObject> prefabLookup = new Dictionary<string, GameObject>();
        foreach (var prefab in AchievementPrefabs)
        {
            prefabLookup[prefab.name] = prefab;
        }

        foreach (var achievement in AchievementManager.Instance.AchievementList)
        {
            if (prefabLookup.TryGetValue(achievement.AchievementId, out GameObject prefab))
            {
                Image image = prefab.GetComponentInChildren<Image>();
                Color newColor = image.color;
                newColor.a = achievement.AchievementUnlocked ? 1 : TransparancyAmount;
                image.color = newColor;
            }
        }
    }

    public void OpenUI() 
    {
        if (!UIIsOpen)
        {
            UpdateAchievementUI();
            MainContainer.SetActive(true);
            UIIsOpen = true;
        }
    }
    public void CloseUI()
    {
        if (UIIsOpen)
        {
            UpdateAchievementUI();
            MainContainer.SetActive(false);
            UIIsOpen = false;
        }
    }
    public void ToggleUI() 
    {
       
        if (UIIsOpen)
            CloseUI();
        else
            OpenUI();
    }
}
