
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManagerScript;

public class ScoreScreen : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> NameTMPList;
    [SerializeField] private List<TextMeshProUGUI> ScoreTMPList;
    [SerializeField] private TextMeshProUGUI SpiritText;
    [SerializeField] private TextMeshProUGUI DeathText;

    [SerializeField] private GameObject SpiritDesignationMenu;

    public void Initialization()
    {
        List<PlayerData> PlayerDatalist = GameManagerScript.instance.AllPlayerData.OrderByDescending(player => player.PlayerScore).ToList();
        SpiritText.text = GameManagerScript.instance.GetSpiritName() + " died by:";
        DeathText.text = GameManagerScript.instance.GetSelectedSituation();
        for (int i = 0; i < PlayerDatalist.Count; i++)
        {
            NameTMPList[i].transform.parent.gameObject.SetActive(true);
            NameTMPList[i].gameObject.SetActive(true);
            NameTMPList[i].text = PlayerDatalist[i].PlayerName;
            ScoreTMPList[i].gameObject.SetActive(true);
            ScoreTMPList[i].text = PlayerDatalist[i].PlayerScore + "";
        }
        for (int i = PlayerDatalist.Count; i < NameTMPList.Count; i++)
        {
            NameTMPList[i].transform.parent.gameObject.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Replay()
    {
        MusicManager.instance.StopMusic();
        GameManagerScript.instance.ChooseSpirit();
        GameManagerScript.instance.ResetPlayed();
        SpiritDesignationMenu.SetActive(true);
        gameObject.SetActive(false);
        SpiritDesignationMenu.GetComponent<SpiritDesignationScreenScript>().Initialization();
    }
}
