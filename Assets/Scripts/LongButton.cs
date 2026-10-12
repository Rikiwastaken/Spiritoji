using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LongButton : MonoBehaviour
{


    [SerializeField] private float timetoSelect = 0.5f;
    private float TimeWhenSelectionDone = -1;

    [SerializeField] private UnityEvent ButtonEvent;

    private Button button;

    private float Buffer = 0;
    [SerializeField] private Image FillingImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
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
            ButtonEvent.Invoke();
        }
        else if (TimeWhenSelectionDone != -1)
        {
            float ratio = 1f - ((TimeWhenSelectionDone - Time.time) / timetoSelect);
            FillingImage.fillAmount = ratio;
        }
        else
        {
            FillingImage.fillAmount = 0;
        }
    }

    public void buttonPressed()
    {
        TimeWhenSelectionDone = Time.time + timetoSelect;
    }

    public void buttonlifted()
    {
        TimeWhenSelectionDone = -1;
    }
}
