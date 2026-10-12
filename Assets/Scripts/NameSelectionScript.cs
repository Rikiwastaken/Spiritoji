using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameSelectionScript : MonoBehaviour
{

    [SerializeField] private GameObject CharacterInputTextPrefabs;

    [SerializeField] private RectTransform SpawnNameButton;


    private List<GameObject> SpawnedGO = new List<GameObject>();

    private Vector2 BaseSpawnpos;

    [SerializeField] private int SpawnOffset;
    [SerializeField] private int MaxNames;

    [SerializeField] private GameObject SpiritDesignationMenu;


    private void Start()
    {
        BaseSpawnpos = SpawnNameButton.transform.localPosition;
    }

    private void GetNamesFromInputFields()
    {
        List<string> names = new List<string>();
        foreach (GameObject go in SpawnedGO)
        {
            TMP_InputField inputfield = go.GetComponent<TMP_InputField>();
            if (inputfield && inputfield.text != "")
            {
                names.Add(inputfield.text);
            }
        }
        List<GameManagerScript.PlayerData> newAllPlayerData = new List<GameManagerScript.PlayerData>();
        for (int i = 0; i < names.Count; i++)
        {
            GameManagerScript.PlayerData newPlayerData = new GameManagerScript.PlayerData();
            newPlayerData.PlayerID = i;
            newPlayerData.PlayerName = names[i];
            newPlayerData.PlayerScore = 0;
            newAllPlayerData.Add(newPlayerData);
        }
        GameManagerScript.instance.AllPlayerData = newAllPlayerData;
    }


    // Creates a new input field for names
    public void SpawnButton()
    {
        if (SpawnedGO.Count >= MaxNames)
        {
            return;
        }
        GameObject newobject = GameObject.Instantiate(CharacterInputTextPrefabs);

        int alreadySpawnedNames = SpawnedGO.Count;
        newobject.transform.SetParent(transform);

        newobject.transform.localPosition = BaseSpawnpos - new Vector2(0, alreadySpawnedNames * SpawnOffset);

        SpawnNameButton.transform.localPosition = BaseSpawnpos - new Vector2(0, (alreadySpawnedNames + 1) * SpawnOffset);

        SpawnedGO.Add(newobject);

        TMP_InputField inputfield = newobject.GetComponent<TMP_InputField>();

    }

    public void Continue()
    {
        GetNamesFromInputFields();
        GameManagerScript.instance.ChooseSpirit();
        SpiritDesignationMenu.SetActive(true);
        gameObject.SetActive(false);
        SpiritDesignationMenu.GetComponent<SpiritDesignationScreenScript>().Initialization();
    }
}
