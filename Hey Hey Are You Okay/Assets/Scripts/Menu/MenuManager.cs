using System.Collections;
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

    [Header("Opening Main Menu")]
    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject bgPanel, bottomPanel;

    public Button examButton, survivalButton;

    //Setting the backstories
    RawImage backdrop;
    string sceneToLoad = "CutsScene";
    [SerializeField] BackstoryScriptableObject backstoryA;
    [SerializeField] BackstoryScriptableObject backstoryB;
    [SerializeField] GameObject bButton;

    [Header("Managers")]
    [SerializeField] GameObject examManager;
    [SerializeField] GameObject survivalManager;

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
    }

    void ChangeMenu(GameObject menuSelected)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menuSelected.SetActive(true);
    }

    public void ExitGame() => Application.Quit();
    public void LoadSymptomScene() => SceneManager.LoadScene(sceneToLoad);

    //Sets the initial backstory when clicking a symptom
    public void OnClickSymptomScene(BackstoryScriptableObject backstory)
    {
        backdrop.texture = backstory.backdrop.texture;
        sceneToLoad = backstory.name + "Scene";

        backstoryA = backstory;
    }

    public void OnClickSymptomSceneB(BackstoryScriptableObject backstory)
    {
        if (backstory.name == "Null")
        {
            bButton.SetActive(false);
        }
        else
        {
            bButton.SetActive(true);
            backstoryB = backstory;
        }
    }

    public void OnClickSymptomAlternator(bool isSymptomAltered)
    {
        if(!isSymptomAltered)
        {
            backdrop.texture = backstoryA.backdrop.texture;
            sceneToLoad = backstoryA.name + "Scene";
        }
        else
        {
            backdrop.texture = backstoryB.backdrop.texture;
            sceneToLoad = backstoryB.name + "Scene";
        }
    }

    public void OnFinishTutorial()
    {
        PlayerPrefs.SetInt("TutorialMenus", 1);
        PlayerPrefs.Save();
    }

    public void OpenMainMenu()
    {
        titlePanel.SetActive(false);
        bgPanel.SetActive(true);
        mainPanel.SetActive(true);
        bottomPanel.SetActive(true);
    }

    public void OnStartExams() => Instantiate(examManager);
    public void OnStartSurvival() => Instantiate(survivalManager);
}
