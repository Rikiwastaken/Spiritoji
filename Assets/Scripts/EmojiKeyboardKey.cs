using UnityEngine;
using UnityEngine.UI;

public class EmojiKeyboardKey : MonoBehaviour
{
    [SerializeField] private Image EmojiSprite;

    public int ID;

    [SerializeField] private EmojiKeyboard KeyBoardManager;

    public void SetEmoji(Sprite sprite)
    {
        EmojiSprite.sprite = sprite;
    }

    public void SetID(int newID)
    {
        ID = newID;
    }

    public void PressKey()
    {
        KeyBoardManager.KeyPress(ID);
    }

}
