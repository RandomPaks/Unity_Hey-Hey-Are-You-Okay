using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] MenuEnum menu = default;
    
    public void OnClickMenu()
    {
        MenuManager.Instance.OpenMenu(menu);
    }
}