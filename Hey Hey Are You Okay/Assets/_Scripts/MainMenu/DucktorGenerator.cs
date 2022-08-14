using UnityEngine;
using UnityEngine.UI;

public class DucktorGenerator : MonoBehaviour
{
    [SerializeField] GameObject templateToggle;
    [SerializeField] GameObject templatePanel;
    [SerializeField] GameObject notesList;
    [SerializeField] DucktorsNotesScriptableObject[] ducktorsNotesScriptableObjects;

    /// <summary>
    /// SCRIPT UNUSED TIL FURTHER NOTICE
    /// </summary>
    void Start()
    {
        //foreach (DucktorsNotesScriptableObject ducktorsNotes in ducktorsNotesScriptableObjects)
        //{
        //    GameObject toggle = Instantiate(templateToggle, notesList.transform);
        //    GameObject panel = Instantiate(templatePanel, notesList.transform);
        //    toggle.GetComponentInChildren<Text>().text = ducktorsNotes.name;
        //    panel.GetComponentInChildren<Text>().text = ducktorsNotes.text;
        //    toggle.GetComponent<Toggle>().onValueChanged.AddListener(panel.SetActive);
        //    panel.SetActive(false);
        //}
    }
}
