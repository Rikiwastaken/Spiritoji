
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpiritGuessScreen : MonoBehaviour
{

    [SerializeField] private GameObject ResultGuessScreen;

    [SerializeField] private List<Image> EmojiImages;
    [SerializeField] private List<Button> PlayerGuessesButtons;

    private List<Button> SelectedButtons = new List<Button>();

    [SerializeField] private Color selectedColor;

    private List<int> CorrespondanceButtonToPlayerID = new List<int>();

    public void Initialization()
    {
        SelectedButtons = new List<Button>();
        List<int> EmojiIDs = GameManagerScript.instance.EmojiIDs;
        for (int i = 0; i < EmojiIDs.Count; i++)
        {
            EmojiImages[i].gameObject.SetActive(true);
            EmojiImages[i].sprite = EmojiManager.instance.EmojiSprites[EmojiIDs[i]];
        }
        for (int i = EmojiIDs.Count; i < EmojiImages.Count; i++)
        {
            EmojiImages[i].gameObject.SetActive(false);
        }

        CorrespondanceButtonToPlayerID = new List<int>();
        List<GameManagerScript.PlayerGuess> Guesses = GameManagerScript.instance.PlayerGuesses;
        for (int i = 0; i < Guesses.Count; i++)
        {
            PlayerGuessesButtons[i].gameObject.SetActive(true);
            PlayerGuessesButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = Guesses[i].PlayerGuessString;
            CorrespondanceButtonToPlayerID.Add(Guesses[i].PlayerID);
        }
        for (int i = Guesses.Count; i < PlayerGuessesButtons.Count; i++)
        {
            PlayerGuessesButtons[i].gameObject.SetActive(false);
        }

    }

    public void SelectButton(int ID)
    {
        Button currentbutton = PlayerGuessesButtons[ID];
        if (SelectedButtons.Contains(currentbutton))
        {
            SelectedButtons.Remove(currentbutton);
            currentbutton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            SelectedButtons.Add(currentbutton);
            currentbutton.GetComponent<Image>().color = selectedColor;
        }
    }

    private void DistributeScore()
    {
        List<GameManagerScript.PlayerData> AllPlayerData = GameManagerScript.instance.AllPlayerData;
        int numberofwinners = 0;
        for (int i = 0; i < PlayerGuessesButtons.Count; i++)
        {
            Button CurrentButton = PlayerGuessesButtons[i];
            if (SelectedButtons.Contains(CurrentButton))
            {
                numberofwinners++;
                GameManagerScript.PlayerData winner = AllPlayerData[CorrespondanceButtonToPlayerID[i]];
                winner.PlayerScore++;
            }
        }


        GameManagerScript.PlayerData Spirit = AllPlayerData[GameManagerScript.instance.SpiritID];
        int newspiritscore = Spirit.PlayerScore + Mathf.Min(2, numberofwinners);
        Spirit.PlayerScore = newspiritscore;

    }
    public void Continue()
    {
        DistributeScore();
        MusicManager.instance.StartResultsMusic();
        gameObject.SetActive(false);
        ResultGuessScreen.gameObject.SetActive(true);
        ResultGuessScreen.GetComponent<ScoreScreen>().Initialization();
    }
}
