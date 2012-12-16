using UnityEngine;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour {
	private string selectedLevel;
	private string[] levels;
	private bool levelSelect;
	private int rowHeight;
	public GameObject levelDataPrefab; 
	// Use this for initialization
	void Start () {
	//Get the list of avaliable levels
	//DirectoryInfo di = new DirectoryInfo("Assets/Maps");	//Check this directory
	//FileInfo[] fi = di.GetFiles("*.txt");
		Object[] files = Resources.LoadAll("Maps");
		levels = new string[files.Length];
		for(int i=0;i<levels.Length;i++)
			levels[i] = files[i].name;
		levelSelect = false;
		rowHeight = 14;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnGUI() {
		if(!levelSelect){
			if(GUI.Button(new Rect(10,50,100,40),"Play Game")) {
				Debug.Log("Playing Game");
				Die ();
				Application.LoadLevel("Game");
			}
			if(GUI.Button(new Rect(10,100,100,40),"Select Levels")) {
				Debug.Log("Loading Level Select");
				levelSelect = true;
				//Application.LoadLevel("LevelSelect");
			}
			if(GUI.Button(new Rect(10,150,100,40),"Level Editor")) {
				Debug.Log("Loading Level Editor");
				Application.LoadLevel("Editor");
			}
			if(GUI.Button(new Rect(10,200,100,40),"Quit Game")) {
				Debug.Log("Quitting Game");
				Application.Quit();
			}
		}
		else{
			for(int i = 0; i < levels.Length; i++){
				if(GUI.Button(new Rect(10+100*((int)i/14),(50+25*i)-((int)i/14)*350,100,25),levels[i])) {
					Debug.Log("Selected level: " + levels[i]);
					selectedLevel = (levels[i].Split('.'))[0];
					Die ();
					Application.LoadLevel("Game");
				}
			}
			if(GUI.Button(new Rect(10,5,100,40),"Main Menu")) {
				levelSelect = false;
			}
			//new Rect(50,(50+25i),(75+25i),80)
		}
		

	}
	
	//When dying, 
	void Die(){
	if(selectedLevel != null){
		GameObject go = Instantiate(levelDataPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		PassBetweenScenes pbs = go.GetComponent<PassBetweenScenes>();
		pbs.SetFname(selectedLevel);
		}
	}
}