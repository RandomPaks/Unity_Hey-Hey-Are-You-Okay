using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuEnum
{
    MAIN,
    OPTIONS,
    TRAINING,
    CUTS,
    LACERATION
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public GameObject mainPanel, optionsPanel, trainingPanel, cutsPanel, lacerationPanel;
    List<GameObject> menus = new List<GameObject>();
    string selectedScene = "CutsScene";

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        menus.Add(mainPanel);
        menus.Add(optionsPanel);
        menus.Add(trainingPanel);
        menus.Add(cutsPanel);
        menus.Add(lacerationPanel);
    }

    public void OpenMenu(MenuEnum menu)
    {
        switch (menu)
        {
            case MenuEnum.MAIN:
                ChangeMenu(mainPanel);
                break;
            case MenuEnum.OPTIONS:
                ChangeMenu(optionsPanel);
                break;
            case MenuEnum.TRAINING:
                ChangeMenu(trainingPanel);
                break;
            case MenuEnum.CUTS:
                ChangeMenu(cutsPanel);
                break;
            case MenuEnum.LACERATION:
                ChangeMenu(lacerationPanel);
                break;
        }
        //Debug.Log($"Opened: {menu}!");
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
        //Debug.Log($"Selected: {name}!");
    }

    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(selectedScene);
    }
}
