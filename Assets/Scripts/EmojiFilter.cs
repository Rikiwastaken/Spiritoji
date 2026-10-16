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

    // Ne garde que les emojis, 4 maximum. Tout le texte est supprimé.
    public static string Filter(string s, int max = 4)
    {
        if (string.IsNullOrEmpty(s)) return s;

        var sb = new StringBuilder();
        int count = 0;
        int i = 0;

        while (i < s.Length && count < max)
        {
            int start = i;
            int cp = Read(s, i, out int len);
            i += len;

            if (!IsEmojiBase(cp)) continue; // texte ignoré

            // Drapeau = 2 regional indicators
            if (cp >= 0x1F1E6 && cp <= 0x1F1FF && i < s.Length)
            {
                int n = Read(s, i, out int nl);
                if (n >= 0x1F1E6 && n <= 0x1F1FF) i += nl;
            }

            // Absorbe modificateurs, sélecteurs, séquences ZWJ
            while (i < s.Length)
            {
                int n = Read(s, i, out int nl);
                if (n == 0xFE0F || n == 0x20E3 || (n >= 0x1F3FB && n <= 0x1F3FF))
                {
                    i += nl;
                }
                else if (n == 0x200D && i + nl < s.Length)
                {
                    i += nl;
                    Read(s, i, out int el);
                    i += el;
                }
                else break;
            }

            sb.Append(s, start, i - start);
            count++;
        }
        return sb.ToString();
    }

    static int Read(string s, int i, out int len)
    {
        if (char.IsHighSurrogate(s[i]) && i + 1 < s.Length && char.IsLowSurrogate(s[i + 1]))
        {
            len = 2;
            return char.ConvertToUtf32(s[i], s[i + 1]);
        }
        len = 1;
        return s[i];
    }

    static bool IsEmojiBase(int cp) =>
        (cp >= 0x1F300 && cp <= 0x1FAFF) ||
        (cp >= 0x1F1E6 && cp <= 0x1F1FF) ||
        (cp >= 0x2600 && cp <= 0x27BF) ||
        (cp >= 0x231A && cp <= 0x23FF) ||
        cp == 0x2B50 || cp == 0x2B55 || cp == 0x2764;
}
