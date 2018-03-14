using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    public GameObject PlayUI;
    public GameObject ResumeUI;
    public GameObject RestartUI;
    public GameObject PauseUI;
    public Canvas canvas;
    public static UIController instance;

    public void HidePlayUI(){
        iTween.MoveTo(PlayUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, 0, 0), 1.0f);
    }

    public void ShowPauseUI(){
        iTween.MoveTo(PauseUI, canvas.transform.position + new Vector3(25 - Screen.width / 2, 25 - Screen.height / 2, 0), 1.0f);
    }

	public void HidePauseUI()
	{
		iTween.MoveTo(PauseUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, -Screen.height / 2, 0), 1.0f);
	}

    public void ShowResumeUI(){
        iTween.MoveTo(ResumeUI, canvas.transform.position + Vector3.zero, 1.0f);
    }

    public void HideResumeUI(){
        iTween.MoveTo(ResumeUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, 0, 0), 1.0f);
    }

	public void ShowRestartUI()
	{
        iTween.MoveTo(RestartUI, canvas.transform.position + Vector3.zero, 1.0f);
	}

	public void HideRestartUI()
	{
		iTween.MoveTo(RestartUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, 0, 0), 1.0f);
	}

    public void PlayHandler(){
        HidePlayUI();
        ShowPauseUI();
        GameController.instance.Play();
        AudioManager.instance.PlayButtonAudio();
    }

	public void ResumeHandler()
	{
        HideResumeUI();
        ShowPauseUI();
        GameController.instance.Resume();
        AudioManager.instance.PlayButtonAudio();
	}

	public void RestartHandler()
	{
        HideRestartUI();
        ShowPauseUI();
        GameController.instance.Restart();
        AudioManager.instance.PlayButtonAudio();
	}

	public void PauseHandler()
	{
		HidePauseUI();
		ShowResumeUI();
        GameController.instance.Pause();
        AudioManager.instance.PlayButtonAudio();
	}

    public void ExitHandler(){
        GameController.instance.Exit();
        AudioManager.instance.PlayButtonAudio();
    }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
