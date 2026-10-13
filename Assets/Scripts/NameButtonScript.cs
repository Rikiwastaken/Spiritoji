using UnityEngine;

public class NameButtonScript : MonoBehaviour
{

    public NameSelectionScript _NameSelectionScript;

    public int ID;
    public void DeleteName()
    {
        _NameSelectionScript.RemoveName(ID);
    }

    public void Initialize(int newID, NameSelectionScript NameSelectionScript)
    {
        ID = newID;
        _NameSelectionScript = NameSelectionScript;
    }
}
