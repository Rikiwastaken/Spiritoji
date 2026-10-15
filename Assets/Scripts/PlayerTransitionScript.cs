using TMPro;
using UnityEngine;

public class PlayerTransitionScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTextMeshProUGUI;

    [SerializeField] private TextMeshProUGUI ContinueButtonTMP;

    [SerializeField] private GameObject PlayerSelectionScreen;

    // Function that puts the correct text on the correct places
    public void Initialization()
    {
        NameTextMeshProUGUI.text = GameManagerScript.instance.GetCurrentPlayerName();
        ContinueButtonTMP.text = GameManagerScript.instance.GetCurrentPlayerName() + " has the phone";
    }

    public void ToNextScreen()
    {
        PlayerSelectionScreen.SetActive(true);
        PlayerSelectionScreen.GetComponent<PlayerGuessScript>().Initialization();
        gameObject.SetActive(false);
    }
}
