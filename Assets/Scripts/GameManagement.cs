using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {
	
	public GameObject[] shipPrefabs;
	public GameObject spawnerPrefab;
	
	public int numLanes = 7;
	public int numShips = 3;
	public float gameSpeed = 0.5f;
	
	private float laneHeight;
	private float screenHeight;
	private float screenWidth;
	
	private float shipSpawnOffset = 5; //Offset from the left side of the screen
	
	private GameObject[] spawners;
	private GameObject[] ships;
	
	// Use this for initialization
	void Start () {
		//Height and Width in world units
		Vector3 hnw = camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,camera.nearClipPlane));
		screenHeight = hnw.y  * 2.0f;
		screenWidth = hnw.x;
		
		laneHeight = screenHeight / (float) numLanes;
		
		Debug.Log("GameManagement: " + screenWidth);
		
		spawners = new GameObject[numLanes];
		ships = new GameObject[numShips];
		
		Setup();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Setup() {
		
		//Setup the spawners
		int i = -3;
		foreach(GameObject spawner in spawners) {
			spawners[i+3] = Instantiate(spawnerPrefab, new Vector3(screenWidth, (float)i * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			i++;
		}
		
		//Setup the ships
		i = -1;
		foreach(GameObject ship in ships) {
			Debug.Log(shipPrefabs[i+1]);
			ships[i+1] = Instantiate(shipPrefabs[i+1], new Vector3(shipSpawnOffset - screenWidth, (float)i * laneHeight, -transform.position.z), Quaternion.identity) as GameObject;
			i++;
		}
	}
	
	public void ShipDied(int i) {
		if(i >=0 && i < ships.Length) ships[i] = null;
	}
	
	public void Reset() {
		Setup();
	}
	
	public float GetLaneHeight(){
		return laneHeight;
	}
	public float GetScreenWidth(){
		return screenWidth;
	}
	public float GetGameSpeed() {
		return gameSpeed;
	}
}
