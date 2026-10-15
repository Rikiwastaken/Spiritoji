using System.Text;
using TMPro;
using UnityEngine;

public class EmojiFilter : MonoBehaviour
{

    [SerializeField] TMP_InputField field;

    void Awake() => field.onValueChanged.AddListener(OnChanged);

    void OnChanged(string s)
    {
        string clean = Filter(s);
        if (clean == s) return;
        field.SetTextWithoutNotify(clean);
        field.caretPosition = clean.Length;
        if (field.text != "")
        {
            GameManagerScript.instance.ChosenEmojis = field.text;
        }
    }

    public string Filter(string input)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            int len = 1, cp = input[i];
            if (char.IsHighSurrogate(input[i]) && i + 1 < input.Length && char.IsLowSurrogate(input[i + 1]))
            {
                cp = char.ConvertToUtf32(input[i], input[i + 1]);
                len = 2;
            }
            if (IsEmoji(cp)) sb.Append(input, i, len);
            i += len - 1;
        }
        return sb.ToString();
    }

    static bool IsEmoji(int cp) =>
        (cp >= 0x1F000 && cp <= 0x1FAFF) ||   // pictographs, flags, skin tones, symbols
        (cp >= 0x2600 && cp <= 0x27BF) ||   // misc symbols, dingbats
        (cp >= 0x2300 && cp <= 0x23FF) ||   // watch, hourglass, etc.
        (cp >= 0x2B00 && cp <= 0x2BFF) ||   // stars, arrows
        (cp >= 0xE0020 && cp <= 0xE007F) ||   // tag chars (subdivision flags)
        cp == 0x200D || cp == 0xFE0F || cp == 0x20E3 ||  // ZWJ, VS16, keycap
        cp == 0xA9 || cp == 0xAE || cp == 0x2122 ||
        cp == 0x203C || cp == 0x2049;
}
