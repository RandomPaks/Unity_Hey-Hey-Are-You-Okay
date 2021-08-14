using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] MenuEnum thisMenu = default;
    public void OnClickMenu()
    {
        MenuManager.Instance.OpenMenu(thisMenu);
    }
}
