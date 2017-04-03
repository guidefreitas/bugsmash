using System.Collections;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	private GvrViewer gvrMain;

	void Awake(){
		this.gvrMain = GameObject.FindGameObjectWithTag("GvrViewerMain").GetComponent<GvrViewer>();
	}

	IEnumerator StartGameAsync() {
        AsyncOperation async = Application.LoadLevelAsync("Scene1");
        yield return async;
        Debug.Log("Loading complete");
    }
	public void StartGame(){
		StartCoroutine("StartGameAsync");
	}

	public void GoToOptions(){

	}

	public void ToggleVRMode(){
		Debug.Log("ToggleVR clicked");
		this.gvrMain.VRModeEnabled = !this.gvrMain.VRModeEnabled;
	}
}
