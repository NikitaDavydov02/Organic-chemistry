using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanizmUIManager : MonoBehaviour {
    [SerializeField]
    public MechanizmManager mechanizmManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Pause()
    {
        mechanizmManager.pause = true;
    }
    public void Play()
    {
        mechanizmManager.pause = false;
    }
    public void MainMenu()
    {
        Application.LoadLevel("Main Menu");
    }
}
