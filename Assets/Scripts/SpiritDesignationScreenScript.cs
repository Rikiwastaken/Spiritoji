using TMPro;
using UnityEngine;

public class SpiritDesignationScreenScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI NameTextMeshProUGUI;

    [SerializeField] private TextMeshProUGUI ContinueButtonTMP;

    [SerializeField] private GameObject SpiritSelectionScreen;
    [SerializeField] private GameObject SpiritGuessScreen;

    [SerializeField] private bool ToSpiritGuess;



    // Function that puts the correct text on the correct places
    public void Initialization()
    {

        if (ToSpiritGuess)
        {
            NameTextMeshProUGUI.text = GameManagerScript.instance.GetSpiritName() + " will now be the judge...";
            ContinueButtonTMP.text = GameManagerScript.instance.GetSpiritName() + " has the phone";
        }
        else
        {
            NameTextMeshProUGUI.text = GameManagerScript.instance.GetSpiritName();
            GameManagerScript.instance.ResetPlayed();
            ContinueButtonTMP.text = GameManagerScript.instance.GetSpiritName() + " has the phone";
            GameManagerScript.instance.SelectPossibleSituations();
        }


    }

    public void ToNextScreen()
    {
        if (ToSpiritGuess)
        {
            SpiritGuessScreen.SetActive(true);
            SpiritGuessScreen.GetComponent<SpiritGuessScreen>().Initialization();
        }
        else
        {
            SpiritSelectionScreen.SetActive(true);
            SpiritSelectionScreen.GetComponent<SpiritSentenceAndEmojiScreen>().Initialization();
        }
        gameObject.SetActive(false);

    }
}
