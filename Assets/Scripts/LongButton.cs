using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LongButton : MonoBehaviour
{


    [SerializeField] private float timetoSelect = 0.5f;
    private float TimeWhenSelectionDone = -1;

    [SerializeField] private UnityEvent ButtonEvent;

    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (TimeWhenSelectionDone < Time.time && TimeWhenSelectionDone != -1)
        {
            ButtonEvent.Invoke();
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
