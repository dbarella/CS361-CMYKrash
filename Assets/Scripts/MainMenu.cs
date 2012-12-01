using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnGUI() {
		if(GUI.Button(new Rect(10,50,100,40),"Play Game")) {
			Debug.Log("Playing Game");
			Application.LoadLevel("Game");
		}
		if(GUI.Button(new Rect(10,100,100,40),"Level Editor")) {
			Debug.Log("Loading Level Editor");
			Application.LoadLevel("Editor");
		}
		if(GUI.Button(new Rect(10,150,100,40),"Quit Game")) {
			Debug.Log("Quitting Game");
			Application.Quit();
		}

	}
	
}
