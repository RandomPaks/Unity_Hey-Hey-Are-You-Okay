using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] MenuEnum menu = default;
    [SerializeField] bool isBackgroundSlow;
    [SerializeField] private BackgroundMove[] mainBackgroundsMove;

    public void OnClickMenu()
    {
        MenuManager.Instance.OpenMenu(menu);

        if (isBackgroundSlow)
        {
            foreach(BackgroundMove bgMove in mainBackgroundsMove)
            {
                bgMove.speed = 5.0f;
            }
        }
        else
        {
            foreach (BackgroundMove bgMove in mainBackgroundsMove)
            {
                bgMove.speed = 50.0f;
            }
        }
    }
}