using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class GameManagement : MonoBehaviour {
	
	//item 0 of this must be null or everything will break
	public GameObject[] spawnablePrefabs;	//The "key" mapping integers to prefabs
	public GameObject[] shipPrefabs;
	public GameObject spawnerPrefab;
	
	public int numLanes = 7;
	public int numShips = 3;
	public float gameSpeed = 0.5f;
	
	private float laneHeight;
	private float screenHeight;
	private float screenWidth;
		
	private float shipSpawnOffset = 5; //Offset from the left side of the screen
	private float spawnerOffset = 10.0f;
	
	private GameObject[] spawners;
	private GameObject[] ships;
	
	private bool won, lost, spawningDone;
	private int shipsLeft;
	
	// Use this for initialization
	void Awake () {
		//Height and Width in world units
		Vector3 hnw = camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,camera.nearClipPlane));
		screenHeight = hnw.y  * 2.0f;
		screenWidth = hnw.x;
		shipsLeft = numShips;
		laneHeight = screenHeight / (float) numLanes;
		won = false;
		lost = false;
		spawningDone = false;
//		Debug.Log("GameManagement calculated laneHeight: " + laneHeight + " world units");
		
		//These have been commented out because they belong in the various Setup methods.
		//spawners = new GameObject[numLanes];
		//ships = new GameObject[numShips];
		GameObject go = GameObject.FindWithTag("LevelData");

		if(go!= null){
			PassBetweenScenes pbs = go.GetComponent<PassBetweenScenes>();
			Setup(pbs.getScene(),pbs.getSpawnables());
		}
		else Setup(); //Sets up a random-spawning game.		
		//Setup("Assets/Maps/level.txt");
		//Setup();
	}
	
	// Update is called once per frame
	void Update () {
		if(spawningDone){
			if(GameObject.FindWithTag("Enemy")==null) won = true;
		}
	}
	
	public void Setup() {
		//All setup methods ought to have this bit in here. (since numLanes may change in Setup)
		spawners = new GameObject[numLanes];
		ships = new GameObject[numShips];
		
		Debug.Log("Setup spawner array length: " + spawners.Length);
		
		//Setup the spawners
		int i = 0;
		foreach(GameObject spawner in spawners) {
			spawners[i] = Instantiate(spawnerPrefab, new Vector3(screenWidth + spawnerOffset, (Camera.main.ViewportToWorldPoint(Vector3.zero).y) + (float)(i+.5) * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			spawners[i].GetComponent<Spawner>().SetRandom(true);
			i++;
		}
		
		//Setup the ships
		//TODO: This is silly guys, stop hardcoding values. See above for my BRILLIANT fix for the spawner hardcoding. - Adam "The Magnificent" Stafford
		i = -1;
		foreach(GameObject ship in ships) {
//			Debug.Log(shipPrefabs[i+1]);
			ships[i+1] = Instantiate(shipPrefabs[i+1], new Vector3(shipSpawnOffset - screenWidth, (float)i * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			i++;
		}
	}
	
	public void Setup(string fname){
		Setup(ParseFile(fname),spawnablePrefabs);
	}

	//TODO:Currently does not include the GameObject array, since the spawners grab that in Start() right now
	public void Setup(int[][] map, GameObject[] prefabs){
//		Debug.Log(map[0][1]);
		numLanes = map.Length;	//This will get the first dimensional length of the array, aka the height.
		laneHeight = screenHeight / (float) numLanes;	//Set this again, since it has changed
		//TODO: Do we ensure that the objects won't overlap if there are lots of lanes?
		spawners = new GameObject[numLanes];
		ships = new GameObject[numShips];
		//This is not quite DRY, but it's technically only 3 lines.
		int i = 0;
		foreach(GameObject spawner in spawners) {
			spawners[i] = Instantiate(spawnerPrefab, new Vector3(screenWidth + spawnerOffset, (Camera.main.ViewportToWorldPoint(Vector3.zero).y) + (float)(i+.5) * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			Spawner script = spawners[i].GetComponent<Spawner>();
			script.SetRandom(false);
			script.SetObjArray(prefabs);
			script.SetTickArray(map[i]);	//We'll pass in the i'th array from map.
			i++;
		}
		//Setup the ships
		//TODO: This is silly guys, stop hardcoding values. See above for my BRILLIANT fix for the spawner hardcoding. - Adam "The Magnificent" Stafford
		i = -1;
		foreach(GameObject ship in ships) {
//			Debug.Log(shipPrefabs[i+1]);
			ships[i+1] = Instantiate(shipPrefabs[i+1], new Vector3(shipSpawnOffset - screenWidth, (float)i * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			i++;
		}	
	}
	
	public void ShipDied() {
		//if(i >=0 && i < ships.Length) ships[i] = null;
		shipsLeft--;
		Debug.Log ("ship dead");
		if(shipsLeft <= 0)
			lost = true;
	}
	
	public void Reset() {
		Setup();
	}
	
	private int[][] ParseFile(string fname){
		int[][] ret;
		try{
		string line;
		StreamReader theReader = new StreamReader(fname, Encoding.Default);
		using (theReader){
			int nPrefabs = Convert.ToInt32(theReader.ReadLine());
			for(int i = 1; i < nPrefabs; i++){
				
				spawnablePrefabs[i] = Resources.Load("Spawnables/"+theReader.ReadLine ()) as GameObject;
			}	
			line = theReader.ReadLine();
			int x = Convert.ToInt32(line);
			line = theReader.ReadLine();
			int y = Convert.ToInt32(line);
			ret = new int[x][];
			//Because this is the way jagged arrays work in C#, we have to declare them on separate lines.
			for(int i=0; i<x;i++){
				ret[i] = new int[y];
			}
			int j = 0;
			do
			{
				line = theReader.ReadLine();
				string[] split = line.Split(' ');
				Debug.Log("Length of split at" + j +" is: " + split.Length);
				for(int i = 0;i<y;i++){
					int temp = Convert.ToInt32(split[i]);
					ret[j][i]=temp;
				}
				j++;
				
			}
			while (j<x);
			
			return ret;
		}
	}
		catch (FileNotFoundException ex){
			Debug.Log("File not found");
			return null;
		}
	} 
	public void LevelEnded(){
		Debug.Log ("spawning done");
		spawningDone = true;
		
	}
	public float GetLaneHeight(){
		return laneHeight;
	}
	public float GetScreenWidth(){
		return screenWidth;
	}
	public float GetScreenHeight(){
		return screenHeight;
	}
	public float GetGameSpeed() {
		return gameSpeed;
	}
	public GameObject[] GetObjectArray() {
		print ("Spawnable Prefabs: "+spawnablePrefabs.Length);
		return spawnablePrefabs;
	}
	public GameObject[] GetShipsArray(){
		return ships;	
	}
	void OnGUI() {
		if(won == true){
			GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));		
			GUI.Box (new Rect (0,0,100,100), "You won!");

			if(GUI.Button (new Rect(10,40,80,30),"Main Menu")){
					Application.LoadLevel("Menu");

			}
			GUI.EndGroup ();

		}
		if(lost == true){
			GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));		
			GUI.Box (new Rect (0,0,100,100), "You lose.");

			if(GUI.Button (new Rect(10,40,80,30),"Main Menu")){
					Application.LoadLevel("Menu");

			}
			GUI.EndGroup ();
		}
	}
}
