using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {
    [SerializeField]
    public GameObject mechaizmsPanel;
    [SerializeField]
    public GameObject instructionPanel;
    // Use this for initialization
    void Start () {
        mechaizmsPanel.gameObject.SetActive(false);
        instructionPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Molecules()
    {
        Application.LoadLevel("Molecules");
    }
    public void Mechanizms()
    {
        //mechaizmsPanel.gameObject.SetActive(mechaizmsPanel.gameObject.active);
        mechaizmsPanel.gameObject.SetActive(true);
    }
    public void SubstitutionRadical()
    {
        Application.LoadLevel("Radical Substitution");
    }
    public void OpenInstruction()
    {
        instructionPanel.gameObject.SetActive(true);
    }
    public void CloseInstruction()
    {
        instructionPanel.gameObject.SetActive(false);
    }
    public void CloseApplication()
    {
        Application.Quit();
    }
}
