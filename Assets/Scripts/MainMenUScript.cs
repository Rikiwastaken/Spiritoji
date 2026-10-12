using UnityEngine;

public class MainMenUScript : MonoBehaviour
{
    [SerializeField] private GameObject NameSelectMenu;
    public void LoadNaming()
    {
        gameObject.SetActive(false);
        NameSelectMenu.SetActive(true);
    }
}
