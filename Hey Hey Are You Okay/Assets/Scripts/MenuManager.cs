using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuEnum
{
    TITLE,
    CHAPTER_1
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public GameObject titlePanel, chapter1Panel;
    List<GameObject> menus = new List<GameObject>();
    string selectedScene = "ThermalBurnsScene";

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
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
        Debug.Log($"Opened: {menu}!");
    }

    void ChangeMenu(GameObject menuSelected)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menuSelected.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectScene(string name)
    {
        selectedScene = name;
        Debug.Log($"Selected: {name}!");
    }

    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(selectedScene);
    }
}
