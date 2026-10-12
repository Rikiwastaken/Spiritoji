
using System.Collections.Generic;
using UnityEngine;

public class EmojiKeyboard : MonoBehaviour
{
    private List<EmojiKeyboardKey> Keys;
    private EmojiKeyboardKey DeleteKey;

    [SerializeField] private int currentPageIndex;
    [SerializeField] private Sprite DeletSprite;
    private List<Sprite> Emojis;
    [SerializeField] private SpiritSentenceAndEmojiScreen _SpiritSentenceAndEmojiScreen;

    void Start()
    {
        Emojis = EmojiManager.instance.EmojiSprites;
        Keys = new List<EmojiKeyboardKey>();
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<EmojiKeyboardKey>())
            {
                Keys.Add(child.GetComponent<EmojiKeyboardKey>());
            }
        }
        DeleteKey = transform.GetChild(transform.childCount - 1).GetComponent<EmojiKeyboardKey>();
        DeleteKey.SetID(-1);
        DeleteKey.SetEmoji(DeletSprite);
        ChangeButtonSprites();
    }


    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ChangeButtonSprites();
        }
    }

    public void NextPage()
    {
        if (currentPageIndex * Keys.Count < Emojis.Count)
        {
            currentPageIndex++;
            ChangeButtonSprites();
        }
    }

    public void ChangeButtonSprites()
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            int ID = i + (currentPageIndex) * Keys.Count;
            if (ID < Emojis.Count)
            {
                Keys[i].SetID(ID);
                Keys[i].SetEmoji(Emojis[ID]);
            }
        }
    }

    public void KeyPress(int ID)
    {
        GameManagerScript.instance.ChangeEmojiList(ID);
        _SpiritSentenceAndEmojiScreen.UpdateInsertedEmojis();
    }

}
