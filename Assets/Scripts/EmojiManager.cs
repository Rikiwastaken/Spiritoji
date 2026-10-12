using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EmojiManager : MonoBehaviour
{

    public static EmojiManager instance;

    public List<Sprite> EmojiSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }


#if UNITY_EDITOR
    [ContextMenu("GetEmojis")]
    public void LoadEmojis()
    {
        EmojiSprites.Clear();
        EmojiSprites.AddRange(Resources.LoadAll<Sprite>("Emojis"));

        Debug.Log($"{EmojiSprites.Count} emojis chargés.");
        EditorUtility.SetDirty(this);
    }
#endif
}
