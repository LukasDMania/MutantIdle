using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FirstPlayTutorial : MonoBehaviour
{
    private class TutorialData
    {
        public Vector2 targetPos;
        public string text;
        public float intervalTime;
        public TutorialData(Vector2 targetPos, string text, float intervalTime)
        {
            this.targetPos = targetPos;
            this.text = text;
            this.intervalTime = intervalTime;
        }
    }

    private List<TutorialData> data;
    private int tutorialIndex = 0;
    public TextMeshProUGUI textMeshProUGUI;


    [SerializeField] private GameObject _tutorialContainer;
    private RectTransform _tutorialRectTransform;

    private string _hasPlayedKey = "HasPlayedBefore";

    private void Start()
    {
        data = AddTutorialData();
        _tutorialRectTransform = _tutorialContainer.GetComponent<RectTransform>();
        Debug.Log("LAST PLAYED KEY EXISTS: " + PlayerPrefs.HasKey("LastPlayed"));
        if (!PlayerPrefs.HasKey("LastPlayed"))
        {
            SaveSystemManager.Instance.DeleteSaveAndResetGame();
            if (_tutorialContainer != null)
            {
                _tutorialContainer.SetActive(true);
                ContinueTutorial();
            }
        }
        else
        {
            if (_tutorialContainer != null && _tutorialContainer.activeSelf)
            {
                _tutorialContainer.SetActive(false);
            }
        }
    }

    private List<TutorialData> AddTutorialData()
    {
        var x = new List<TutorialData>();
        x.Add(new TutorialData(new Vector2(0, 40), "Welcome to Mutant Idle!", 0f));
        x.Add(new TutorialData(new Vector2(0, 40), "The goal is to gather DNA to complete the monster science experiment", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "Here you mutate the experiment, body parts will generate DNA automatically", 2f));
        x.Add(new TutorialData(new Vector2(555, 445), "You can scroll down to see later body parts or click and drag with the mouse", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "CTRL + Click will upgrade the maximum times you can afford", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "SHIFT + Click will upgrade 10 times or as many under 10 you can afford", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "SPACE + Click will upgrade 25 times or as many under 10 you can afford", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "You can quick upgrade by pressing 1234567890F1F2F3F4F5 for generators 1-15 respectively", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "You can combine these hotkeys with CTRL, SHIFT and SPACE", 0f));
        x.Add(new TutorialData(new Vector2(555, 445), "Click anywhere along the side of the panel to close or open it again", 0f));
        x.Add(new TutorialData(new Vector2(-435, 435), "This top label displays your DNA. This is needed to buy and upgrade body parts", 2f));
        x.Add(new TutorialData(new Vector2(-435, 330), "This is the total amount of DNA you're generating per tick", 1f));
        x.Add(new TutorialData(new Vector2(-435, 230), "This says how many seconds per tick. Every tick all body parts will generate their DNA", 1f));

        x.Add(new TutorialData(new Vector2(-435, 120), "In this panel on click you will see various useful stats needed to unlock Achievements", 1f));
        x.Add(new TutorialData(new Vector2(-435, 59), "In this panel on click you will see all the achievements, grey means locked, white unlocked", 1f));
        x.Add(new TutorialData(new Vector2(-435, 59), "Achievements play a crucial role in the progression system (Tip: Aim for 50 EVO points)", 1f));
        x.Add(new TutorialData(new Vector2(-435, -6), "This button will prestige you, resetting all your progress and giving you the saved EVO points", 1f));
        x.Add(new TutorialData(new Vector2(-435, -6), "Prestiging is possible if you have at least 1 level of the 6th Body Part", 1f));


        x.Add(new TutorialData(new Vector2(-688, 293), "Global Multiplier. This multiplication gets added to any DNA generating source", 2f));
        x.Add(new TutorialData(new Vector2(-692, 202), "This is your evolution overview. Evolving will reset all your progress except for your evolution points...", 1f));
        x.Add(new TutorialData(new Vector2(-692, 202), "In return you get the Evolution points saved up under the top label \"Evo Points On Prestige\"", 0f));
        x.Add(new TutorialData(new Vector2(-692, 202), "Evolution Points will each add x0.1 to the global multiplier each.", 0f));
        x.Add(new TutorialData(new Vector2(-692, 202), "If you can't reach the end right away, try prestiging!", 0f));
        x.Add(new TutorialData(new Vector2(-32, -39), "Thanks for playing!", 3f));
        
        return x;
    }

    public void CloseTutorial()
    {
        if (_tutorialContainer != null)
        {
            _tutorialContainer.SetActive(false);
        }
    }




    public void ContinueTutorial() 
    {
        Debug.Log("LIST COUNT " + data.Count);
        Debug.Log("INDEX " + tutorialIndex);
        if (tutorialIndex < data.Count - 1)
        {
            _tutorialRectTransform.DOAnchorPos(data[tutorialIndex].targetPos, data[tutorialIndex].intervalTime)
                .OnComplete(() => {
                    textMeshProUGUI.text = data[tutorialIndex].text;
                    tutorialIndex++;
                });
        }
        else
        {
            CloseTutorial();
        }
    }
}

