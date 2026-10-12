
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpiritSentenceAndEmojiScreen : MonoBehaviour
{

    [SerializeField] private Button FirstSentenceButton;
    [SerializeField] private Button SecondSentenceButton;
    [SerializeField] private Button ThirdSentenceButton;

    [SerializeField] private Color SelectedColor;
    [SerializeField] private List<Image> EmojiImageList;

    [SerializeField] private GameObject TransitionScreen;


    public void Initialization()
    {


        ResetColors(FirstSentenceButton);
        ResetColors(SecondSentenceButton);
        ResetColors(ThirdSentenceButton);
        List<string> situations = GameManagerScript.instance.GetPossibleSituations();
        FirstSentenceButton.GetComponentInChildren<TextMeshProUGUI>().text = situations[0];
        SecondSentenceButton.GetComponentInChildren<TextMeshProUGUI>().text = situations[1];
        ThirdSentenceButton.GetComponentInChildren<TextMeshProUGUI>().text = situations[2];
        UpdateInsertedEmojis();
    }


    public void SelectImage(int ID)
    {
        ResetColors(FirstSentenceButton);
        ResetColors(SecondSentenceButton);
        ResetColors(ThirdSentenceButton);
        ColorBlock colors;
        switch (ID)
        {
            case 0:
                colors = FirstSentenceButton.colors;
                colors.normalColor = CloneColor(SelectedColor);
                colors.selectedColor = CloneColor(SelectedColor);
                FirstSentenceButton.colors = colors;
                break;
            case 1:
                colors = SecondSentenceButton.colors;
                colors.normalColor = CloneColor(SelectedColor);
                colors.selectedColor = CloneColor(SelectedColor);
                SecondSentenceButton.colors = colors;
                break;
            case 2:
                colors = ThirdSentenceButton.colors;
                colors.normalColor = CloneColor(SelectedColor);
                colors.selectedColor = CloneColor(SelectedColor);
                ThirdSentenceButton.colors = colors;
                break;
        }
        GameManagerScript.instance.SetSelectedSituationID(ID);
    }

    private void ResetColors(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = CloneColor(Color.white);
        colors.selectedColor = CloneColor(Color.white);
        button.colors = colors;
    }

    private Color CloneColor(Color color)
    {
        return new Color(color.r, color.g, color.b, 1f);
    }

    public void Continue()
    {
        if (GameManagerScript.instance.GetSelectedSituationID() >= 0 && GameManagerScript.instance.EmojiIDs.Count > 0)
        {
            GameManagerScript.instance.SelectPlayer();
            TransitionScreen.GetComponent<PlayerTransitionScript>().Initialization();
            gameObject.SetActive(false);
            TransitionScreen.SetActive(true);
        }
    }

    public void UpdateInsertedEmojis()
    {
        for (int i = 0; i < EmojiImageList.Count; i++)
        {
            if (i < GameManagerScript.instance.EmojiIDs.Count)
            {
                EmojiImageList[i].gameObject.SetActive(true);
                EmojiImageList[i].sprite = EmojiManager.instance.EmojiSprites[GameManagerScript.instance.EmojiIDs[i]];
            }
            else
            {
                EmojiImageList[i].gameObject.SetActive(false);
            }

        }
    }
}
