using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerScript : MonoBehaviour
{

    [Serializable]
    private class SituationsListWrapper
    {
        public List<string> SituationsList;
    }

    [Serializable]
    public struct PlayerGuess
    {
        public int PlayerID;
        public string PlayerGuessString;
    }

    [Serializable]
    public class PlayerData
    {
        public int PlayerID;
        public string PlayerName;
        public int PlayerScore;
    }

    public static GameManagerScript instance;

    [Header("Player Variables")]

    public List<PlayerData> AllPlayerData; // All Player Data

    public int SpiritID; // ID of the player chosen as a spirit

    public int PreviousSpirit = -1;

    public int CurrentPlayer;

    private List<int> AlreadyPlayed = new List<int>();

    public List<PlayerGuess> PlayerGuesses;


    [Header("Situations Variables")]
    [SerializeField] private TextAsset SituationJSON;

    [SerializeField] private List<string> SituationsList;

    [SerializeField] private int NumberOfPossibleSituations;



    [SerializeField] private List<int> PossibleSituationsID;
    [SerializeField] private int SelectedSituationsID;

    public List<int> EmojiIDs;
    [SerializeField] private int MaxEmojis;




    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadSituationsFromJSON();
    }

    public void ChooseSpirit()
    {
        int SafeguardCounter = 0;
        SpiritID = -1;
        while (SafeguardCounter < 100 && SpiritID == -1)
        {
            int NewSpiritID = Random.Range(0, AllPlayerData.Count);
            if (NewSpiritID != PreviousSpirit)
            {
                SpiritID = NewSpiritID;
            }
            SafeguardCounter++;
        }
    }

    public void ResetPlayed()
    {
        AlreadyPlayed = new List<int>();
        CurrentPlayer = -1;
        SelectedSituationsID = -1;
        PossibleSituationsID = new List<int>();
        EmojiIDs = new List<int>();
        PlayerGuesses = new List<PlayerGuess>();
    }

    // Selects a new player
    public string SelectPlayer()
    {
        List<int> remainingPlayers = new List<int>();

        for (int i = 0; i < AllPlayerData.Count; i++)
        {
            if (!AlreadyPlayed.Contains(i) && i != SpiritID)
            {
                remainingPlayers.Add(i);
            }
        }
        CurrentPlayer = -1;
        if (remainingPlayers.Count > 0)
        {
            CurrentPlayer = remainingPlayers[Random.Range(0, remainingPlayers.Count)];
        }
        if (CurrentPlayer != -1)
        {
            AlreadyPlayed.Add(CurrentPlayer);
            return AllPlayerData[CurrentPlayer].PlayerName;
        }
        return "";

    }

    // get name of selected player
    public string GetCurrentPlayerName()
    {
        if (CurrentPlayer == -1)
        {
            Debug.LogError("no selected player");
            return "NA";
        }
        return AllPlayerData[CurrentPlayer].PlayerName;
    }

    public string GetSpiritName()
    {
        if (SpiritID >= 0 && SpiritID < AllPlayerData.Count)
        {
            return AllPlayerData[SpiritID].PlayerName;
        }
        return "";
    }

    public void LoadSituationsFromJSON()
    {

        SituationsListWrapper wrapper = JsonUtility.FromJson<SituationsListWrapper>(SituationJSON.text);
        if (wrapper == null || wrapper.SituationsList == null)
        {
            Debug.LogError("JSON file format invalid. Needs { \"SituationsList\": [ ... ] }");
            return;
        }

        SituationsList = wrapper.SituationsList;
    }

    // Picks 3 random situations amongst all possible ones
    public void SelectPossibleSituations()
    {
        PossibleSituationsID = new List<int>();
        List<int> AllSituationIDs = new List<int>();
        for (int i = 0; i < SituationsList.Count; i++)
        {
            AllSituationIDs.Add(i);
        }

        for (int i = 0; i < NumberOfPossibleSituations; i++)
        {
            int PickedID = AllSituationIDs[Random.Range(0, AllSituationIDs.Count)];
            AllSituationIDs.Remove(PickedID);

            PossibleSituationsID.Add(PickedID);
        }
    }

    public List<string> GetPossibleSituations()
    {
        List<string> PossibleSituations = new List<string>();
        for (int ID = 0; ID < PossibleSituationsID.Count; ID++)
        {
            int SituationID = PossibleSituationsID[ID];
            PossibleSituations.Add(SituationsList[SituationID]);
        }
        return PossibleSituations;
    }

    public void SetSelectedSituationID(int ID)
    {
        SelectedSituationsID = PossibleSituationsID[ID];
    }

    public int GetSelectedSituationID()
    {
        return SelectedSituationsID;
    }

    public void ChangeEmojiList(int ID)
    {
        if (ID == -1)
        {
            EmojiIDs.RemoveAt(EmojiIDs.Count - 1);
        }
        else if (EmojiIDs.Count < MaxEmojis)
        {
            EmojiIDs.Add(ID);
        }
    }

}
