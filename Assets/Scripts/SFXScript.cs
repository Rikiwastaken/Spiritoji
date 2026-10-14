using UnityEngine;

public class SFXScript : MonoBehaviour
{

    private float TimeToDestroy;

    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        if (Time.time > TimeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void Initialize(AudioClip Clip)
    {
        TimeToDestroy = Time.time + Clip.length + 0.25f;
        audioSource.clip = Clip;
        audioSource.Play();
    }
}
