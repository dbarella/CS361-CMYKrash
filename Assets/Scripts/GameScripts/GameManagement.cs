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
	public float[][] probabilitySections;
	
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
	private bool randomGame;
	public float randomModeSwitchTime;
	private float randomModeTimer;
	
	private bool paused;
	
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
		randomGame = false;
		spawningDone = false;
		paused = false;
//		Debug.Log("GameManagement calculated laneHeight: " + laneHeight + " world units");
		
		//These have been commented out because they belong in the various Setup methods.
		//spawners = new GameObject[numLanes];
		//ships = new GameObject[numShips];
		GameObject go = GameObject.FindWithTag("LevelData");

		if(go!= null){
			PassBetweenScenes pbs = go.GetComponent<PassBetweenScenes>();
			if(pbs.GetFname() != null)
				Setup(pbs.GetFname());
			else
				Setup(pbs.getScene(),pbs.getSpawnables());
        }
        else Setup(); //Sets up a random-spawning game.
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape) && !paused) {
			Time.timeScale = 0;
			paused = true;
		} else if(Input.GetKeyDown(KeyCode.Escape) && paused) {
			Time.timeScale = 1.0f;
			paused = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(spawningDone){
			if(GameObject.FindWithTag("Enemy")==null) won = true;
		}
		if(randomGame){
			randomModeTimer -= Time.deltaTime;
			if(randomModeTimer <= 0){
				SetupRandomSpawners();
				randomModeTimer = randomModeSwitchTime;
			}
		}
	}
	private void SetupRandomSpawners(){
		int index = UnityEngine.Random.Range(0, probabilitySections.Length);
				foreach(GameObject spawn in spawners){
					spawn.GetComponent<Spawner>().SetProbabilityArray(probabilitySections[index]);
				}
	}
	//new random stuff: the way this works is you have a bunch of modes, which have different probabilities of things appearing.
	//this was supposed to be set up in the inspector but for some reason it's not showing up in the inspector on my machine,
	//so instead i hard code values into probabilitySections[] below. These numbers obviously need tuning. The order of the objects
	//will be the way the prefabs are set up in the inspector. sorry about that opacity. Every randomModeSwitchTime seconds, the game
	//will select a new random probability section.
	//the zero index will be the probability that nothing appears at a given time. this should be non-zero to make the game not 
	//completely terrifying.
	public void Setup() {
		//All setup methods ought to have this bit in here. (since numLanes may change in Setup)
		spawners = new GameObject[numLanes];
		ships = new GameObject[numShips];
		probabilitySections = new float[][] {
			new float[] {1/8.0f, 1/8.0f, 1/8.0f, 1/8.0f, 1/8.0f, 1/8.0f, 1/8.0f, 1/8.0f},
    		//new float[] {0f, 0f, 0.5f, 0.5f, 0f, 0f, 0f, 0f},
    		//new float[] {0f, 0f, 0f, 0f, 0.5f, 0.5f, 0f, 0f}
		};
		randomGame = true;
		randomModeTimer = randomModeSwitchTime;
		Debug.Log("Setup spawner array length: " + spawners.Length);
		
		//Setup the spawners
		int i = 0;
		foreach(GameObject spawner in spawners) {
			spawners[i] = Instantiate(spawnerPrefab, new Vector3(screenWidth + spawnerOffset, (Camera.main.ViewportToWorldPoint(Vector3.zero).y) + (float)(i+.5) * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			spawners[i].GetComponent<Spawner>().SetRandom(true);
			i++;
		}
		SetupRandomSpawners();
		SetupShips(ships);
	}
	
	public void Setup(string fname){
		Setup(ParseFile(fname),spawnablePrefabs);
	}

	//TODO:Currently does not include the GameObject array, since the spawners grab that in Start() right now
	public void Setup(int[][] map, GameObject[] prefabs){
		//foreach(GameObject prefab in prefabs) Debug.Log(prefab);
		this.spawnablePrefabs = prefabs;
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
			//script.SetObjArray(prefabs);
			script.SetTickArray(map[i]);	//We'll pass in the i'th array from map.
			i++;
		}
		//Setup the ships
		SetupShips(ships);
	}
	
	public void SetupShips(GameObject[] ships) {
		int i = -1;
		float _tmpOffset = 0.0f;
		foreach(GameObject ship in ships) {
//			Debug.Log(shipPrefabs[i+1]);
			if(i==0) {
				_tmpOffset = 30.0f;
			} else {
				_tmpOffset = 0.0f;
			}
			ships[i+1] = Instantiate(shipPrefabs[i+1], new Vector3(_tmpOffset + shipSpawnOffset - screenWidth, (float)i * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			ships[i+1].GetComponent<Ship>().SetShipInstance(i);
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
		//Debug.Log(fname);
		TextAsset file = Resources.Load("Maps/"+fname) as TextAsset;
		string[] lines = file.text.Split('\n');
		int nPrefabs = Convert.ToInt32(lines[0]);
		spawnablePrefabs = new GameObject[nPrefabs];
		for(int i = 1; i < nPrefabs; i++){				
			spawnablePrefabs[i] = Resources.Load("Spawnables/"+lines[i]) as GameObject;
		}
		int x = Convert.ToInt32(lines[nPrefabs]);
		int y = Convert.ToInt32(lines[nPrefabs+1]);
		ret = new int[x][];
		for(int i=0; i<x;i++){
				ret[i] = new int[y];
		}
		for(int i = 0; i < x; i++){
			string[] line = lines[i+nPrefabs+2].Split(' ');
			for(int j = 0; j< y; j++){
				ret[i][j] = Convert.ToInt32(line[j]); 
			}
		}
		return ret;
	} 
	public void LevelEnded(){
//		Debug.Log ("spawning done");
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
		//print("Spawnable Prefabs: "+spawnablePrefabs.Length);
		return spawnablePrefabs;
	}
	public GameObject[] GetShipsArray(){
		return ships;	
	}
	void OnGUI() {
		/*GUI.BeginGroup (new Rect (3 * Screen.width / 4, 0, 100, 100));		
			//GUI.Box (new Rect (0,0,100,100), "You lose.");

			if(GUI.Button (new Rect(10,40,80,30),"Main Menu")){
				Application.LoadLevel("Menu");
			}
		GUI.EndGroup ();
		
		GUI.BeginGroup (new Rect (Screen.width / 4, 0, 100, 100));		
			//GUI.Box (new Rect (0,0,100,100), "You lose.");

			if(GUI.Button (new Rect(10,40,80,30),"Pause")){
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		GUI.EndGroup (); */
		
		if(paused == true){
			GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));		
			GUI.Box (new Rect (0,0,100,100), "Pause Menu");

			if(GUI.Button (new Rect(10,40,80,30),"Main Menu")){
				Time.timeScale = 1.0f;
				paused = false;
				Application.LoadLevel("Menu");
			}
			GUI.EndGroup ();

		}
		
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
