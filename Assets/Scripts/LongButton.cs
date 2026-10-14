using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LongButton : MonoBehaviour
{


    [SerializeField] private bool requireLongPress = false;
    [SerializeField] private float timetoSelect = 0.5f;
    private float TimeWhenSelectionDone = -1;

    [SerializeField] private UnityEvent ButtonEvent;

    private Button button;

    private float Buffer = 0;
    [SerializeField] private Image FillingImage;
    [SerializeField] private Material Fillingmat;
    [SerializeField] private int LongSFXToPlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        Material newmat = new Material(Fillingmat);
        FillingImage.material = newmat;
        FillingImage.material.SetFloat("_DissolvePercent", 1f);
        if (!requireLongPress)
            button.onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        if (Time.time < Buffer)
            return;
        if (!requireLongPress)
        {
            MusicManager.instance.InstantSoundEffect();
        }
        ButtonEvent.Invoke();
    }

    private void OnEnable()
    {
        Buffer = Time.time + timetoSelect;

    }

    private void Update()
    {
        if (Time.time < Buffer)
        {
            TimeWhenSelectionDone = -1;
            return;
        }
        if (TimeWhenSelectionDone < Time.time && TimeWhenSelectionDone != -1)
        {
            TimeWhenSelectionDone = -1;
            FillingImage.material.SetFloat("_DissolvePercent", 0f);
            MusicManager.instance.LongButtonSoundEffect(LongSFXToPlay);
            ButtonEvent.Invoke();
        }
        else if (TimeWhenSelectionDone != -1)
        {
            float ratio = ((TimeWhenSelectionDone - Time.time) / timetoSelect);
            Debug.Log(ratio);
            FillingImage.material.SetFloat("_DissolvePercent", ratio);
        }
        else
        {
            FillingImage.material.SetFloat("_DissolvePercent", 1f);
        }
    }

    public void buttonPressed()
    {
        if (!requireLongPress)
            return;
        TimeWhenSelectionDone = Time.time + timetoSelect;
    }

    public void buttonlifted()
    {
        TimeWhenSelectionDone = -1;
    }
}
