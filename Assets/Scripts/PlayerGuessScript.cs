using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGuessScript : MonoBehaviour
{

    [SerializeField] private GameObject PlayerTransitionScreen;
    [SerializeField] private GameObject SpiritGuessTransitionScreen;
    [SerializeField] private TMP_InputField PlayerGuessField;

    [SerializeField] private List<Image> EmojiImages;
    [SerializeField] private List<TextMeshProUGUI> EmojiTMPs;

    public bool Buffer;

    public void Initialization()
    {
        PlayerGuessField.text = "";
        /*List<int> EmojiIDs = GameManagerScript.instance.EmojiIDs;
        for (int i = 0; i < EmojiIDs.Count; i++)
        {
            EmojiImages[i].gameObject.SetActive(true);
            EmojiImages[i].sprite = EmojiManager.instance.EmojiSprites[EmojiIDs[i]];
        }
        for (int i = EmojiIDs.Count; i < EmojiImages.Count; i++)
        {
            EmojiImages[i].gameObject.SetActive(false);
        }
        */
        string Emojis = GameManagerScript.instance.ChosenEmojis;
        for (int i = 0; i < Emojis.Length; i++)
        {
            EmojiTMPs[i].gameObject.SetActive(true);
            EmojiTMPs[i].text = Emojis[i] + "";
        }
        for (int i = Emojis.Length; i < EmojiTMPs.Count; i++)
        {
            EmojiTMPs[i].gameObject.SetActive(false);
        }
        Buffer = true;
    }

    public void Continue()
    {
        if (PlayerGuessField.text != "" && Buffer)
        {
            Buffer = false;
            GameManagerScript.PlayerGuess playerGuess = new GameManagerScript.PlayerGuess();
            playerGuess.PlayerGuessString = PlayerGuessField.text;
            playerGuess.PlayerID = GameManagerScript.instance.CurrentPlayer;
            GameManagerScript.instance.PlayerGuesses.Add(playerGuess);

            if (GameManagerScript.instance.SelectPlayer() == "")
            {
                SpiritGuessTransitionScreen.GetComponent<SpiritDesignationScreenScript>().Initialization();
                SpiritGuessTransitionScreen.gameObject.SetActive(true);
            }
            else
            {
                PlayerTransitionScreen.gameObject.SetActive(true);
                PlayerTransitionScreen.GetComponent<PlayerTransitionScript>().Initialization();
            }
            gameObject.SetActive(false);
        }

    }
}
