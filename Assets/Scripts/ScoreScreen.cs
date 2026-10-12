
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static GameManagerScript;

public class ScoreScreen : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> NameTMPList;
    [SerializeField] private List<TextMeshProUGUI> ScoreTMPList;

    [SerializeField] private GameObject SpiritDesignationMenu;

    public void Initialization()
    {
        List<PlayerData> PlayerDatalist = GameManagerScript.instance.AllPlayerData.OrderByDescending(player => player.PlayerScore).ToList();
        for (int i = 0; i < PlayerDatalist.Count; i++)
        {
            NameTMPList[i].gameObject.SetActive(true);
            NameTMPList[i].text = PlayerDatalist[i].PlayerName;
            ScoreTMPList[i].gameObject.SetActive(true);
            ScoreTMPList[i].text = PlayerDatalist[i].PlayerScore + "";
        }
        for (int i = PlayerDatalist.Count; i < NameTMPList.Count; i++)
        {
            NameTMPList[i].gameObject.SetActive(false);
            ScoreTMPList[i].gameObject.SetActive(false);
        }
    }


    public void Replay()
    {
        GameManagerScript.instance.ChooseSpirit();
        GameManagerScript.instance.ResetPlayed();
        SpiritDesignationMenu.SetActive(true);
        gameObject.SetActive(false);
        SpiritDesignationMenu.GetComponent<SpiritDesignationScreenScript>().Initialization();
    }
}
