using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        CheckPlayerKeysExist("TutCutsCompleted");
        CheckPlayerKeysExist("TutLacerationCompleted");
        CheckPlayerKeysExist("TutBurnsCompleted");
        CheckPlayerKeysExist("TutCutsAltCompleted");
        CheckPlayerKeysExist("TutLacerationAltCompleted");
        CheckPlayerKeysExist("TutBurnsAltCompleted");

        CheckPlayerProgress();
    }

    public void CheckPlayerProgress()
    {
        if(CheckPlayerKeysDone("TutCutsCompleted") &&
            CheckPlayerKeysDone("TutLacerationCompleted") &&
            CheckPlayerKeysDone("TutBurnsCompleted") &&
            CheckPlayerKeysDone("TutCutsAltCompleted") &&
            CheckPlayerKeysDone("TutLacerationAltCompleted") &&
            CheckPlayerKeysDone("TutBurnsAltCompleted"))
        {
            MenuManager.Instance.examButton.interactable = true;
        }
    }

    void CheckPlayerKeysExist(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    bool CheckPlayerKeysDone(string key)
    {
        if(PlayerPrefs.GetInt(key) == 1)
        {
            return true;
        }
        return false;
    }

    public void PlayerFinishLevel(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 1);
        }
    }
}
