using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    [SerializeField] string[] playerKeys;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        foreach (string key in playerKeys)
        {
            CheckPlayerKeysExist(key);
        }

        CheckPlayerProgress();
    }

    public bool CheckPlayerProgress()
    {
        bool result;
        foreach (string key in playerKeys)
        {
            result = CheckPlayerKeysDone(key);
            if (!result) return false;
        }
        MenuManager.Instance.examButton.interactable = true;
        MenuManager.Instance.survivalButton.interactable = true;
        return true;
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
