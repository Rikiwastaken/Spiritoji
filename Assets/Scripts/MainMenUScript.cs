using UnityEngine;

public class MainMenUScript : MonoBehaviour
{
    [SerializeField] private GameObject NameSelectMenu;
    public void LoadNaming()
    {
        gameObject.SetActive(false);
        NameSelectMenu.SetActive(true);
    }

    public void Start()
    {
        MusicManager.instance.StartMainMenuMusic();
        Screen.orientation = ScreenOrientation.Portrait;
    }

}
