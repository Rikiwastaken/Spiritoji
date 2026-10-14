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
    [SerializeField] private float SpawnButtonSpacing = 40;
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

    public void RemoveName(int ID)
    {
        GameObject GOtoRemove = SpawnedGO[ID];
        SpawnedGO.RemoveAt(ID);
        Destroy(GOtoRemove);
        ReplaceButtons();
    }


    public void SpawnButton()
    {
        if (SpawnedGO.Count >= MaxNames)
        {
            return;
        }
        GameObject newobject = GameObject.Instantiate(CharacterInputTextPrefabs);

        int alreadySpawnedNames = SpawnedGO.Count;
        newobject.transform.SetParent(transform);

        newobject.GetComponentInChildren<NameButtonScript>().Initialize(alreadySpawnedNames, this);
        SpawnedGO.Add(newobject);

        ReplaceButtons();
    }

    private void ReplaceButtons()
    {
        for (int i = 0; i < SpawnedGO.Count; i++)
        {
            SpawnedGO[i].transform.localPosition = GetFieldPosition(SpawnedGO[i], i);
            SpawnedGO[i].GetComponentInChildren<NameButtonScript>().Initialize(i, this);
        }

        if (SpawnedGO.Count == 0)
        {
            SpawnNameButton.transform.localPosition = BaseSpawnpos;
            return;
        }

        GameObject lastField = SpawnedGO[SpawnedGO.Count - 1];
        RectTransform lastRect = lastField.GetComponent<RectTransform>();
        float lastFieldBottom = lastField.transform.localPosition.y + lastRect.rect.yMin * lastRect.localScale.y;
        float buttonTopOffset = SpawnNameButton.rect.yMax * SpawnNameButton.localScale.y;
        SpawnNameButton.transform.localPosition = new Vector2(BaseSpawnpos.x, lastFieldBottom - SpawnButtonSpacing - buttonTopOffset);
    }

    private Vector2 GetFieldPosition(GameObject field, int index)
    {
        RectTransform root = field.GetComponent<RectTransform>();
        float min = root.rect.xMin;
        float max = root.rect.xMax;
        foreach (RectTransform child in root)
        {
            min = Mathf.Min(min, child.localPosition.x + child.rect.xMin);
            max = Mathf.Max(max, child.localPosition.x + child.rect.xMax);
        }
        float centerOffset = -(min + max) / 2f * root.localScale.x;
        return BaseSpawnpos + new Vector2(centerOffset, -index * SpawnOffset);
    }

    public void Continue()
    {

        GetNamesFromInputFields();
        if (GameManagerScript.instance.AllPlayerData.Count >= 2)
        {
            MusicManager.instance.StopMusic();
            GameManagerScript.instance.ChooseSpirit();
            SpiritDesignationMenu.SetActive(true);
            gameObject.SetActive(false);
            SpiritDesignationMenu.GetComponent<SpiritDesignationScreenScript>().Initialization();
        }

    }
}
