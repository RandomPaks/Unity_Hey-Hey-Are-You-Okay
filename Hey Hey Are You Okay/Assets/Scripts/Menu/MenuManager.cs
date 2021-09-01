using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MenuEnum
{
    MAIN,
    OPTIONS,
    TRAINING,
    BACKSTORY
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public GameObject mainPanel, optionsPanel, trainingPanel, backstoryPanel;
    List<GameObject> menus = new List<GameObject>();

    int isTutorialDone;
    [Header("Setting up the Tutorial")]
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject tutorialButton;

    //Setting the backstories
    RawImage backdrop;
    string sceneToLoad;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

#if UNITY_ANDROID
        Screen.SetResolution(1080, 1920, false);
#else
        Screen.SetResolution(540, 960, false);
#endif
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("TutorialMenus"))
        {
            PlayerPrefs.SetInt("TutorialMenus", 0);
        }
        isTutorialDone = PlayerPrefs.GetInt("TutorialMenus");

        if(isTutorialDone == 0)
        {
            tutorialButton.SetActive(true);
            playButton.SetActive(false);
        }
        else
        {
            tutorialButton.SetActive(false);
            playButton.SetActive(true);
        }
        
        menus.Add(mainPanel);
        menus.Add(optionsPanel);
        menus.Add(trainingPanel);
        menus.Add(backstoryPanel);

        backdrop = backstoryPanel.GetComponentInChildren<RawImage>();
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
            case MenuEnum.BACKSTORY:
                ChangeMenu(backstoryPanel);
                
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

    public void LoadSymptomScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnClickSymptomScene(BackstoryScriptableObject backstory)
    {
        backdrop.texture = backstory.backdrop.texture;
        sceneToLoad = backstory.name + "Scene";
    }

    public void OnFinishTutorial()
    {
        PlayerPrefs.SetInt("TutorialMenus", 1);
    }
}
