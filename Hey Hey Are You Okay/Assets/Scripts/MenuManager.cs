using System.Collections.Generic;
using UnityEngine;

public enum MenuEnum
{
    TITLE,
    CHAPTER_1
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public GameObject titlePanel, chapter1Panel;
    List<GameObject> menus = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        menus = new List<GameObject>();
        menus.Add(titlePanel);
        menus.Add(chapter1Panel);
    }

    public void OpenMenu(MenuEnum menu)
    {
        switch (menu)
        {
            case MenuEnum.TITLE:
                ChangeMenu(titlePanel);
                break;
            case MenuEnum.CHAPTER_1:
                ChangeMenu(chapter1Panel);
                break;
        }
    }

    void ChangeMenu(GameObject menuSelected)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menuSelected.SetActive(true);
    }
}
