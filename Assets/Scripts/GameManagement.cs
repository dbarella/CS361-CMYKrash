using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {
	
	public GameObject shipPrefab;
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
		//Instantiates the camera s.t. the z-axis passes through the center of the screen.
		Vector3 lowerLeft = camera.ScreenToWorldPoint (new Vector3 (0,0,camera.nearClipPlane));
		transform.Translate(new Vector3(-lowerLeft.x,-lowerLeft.y/2.0f,-10));
		
		//Height and Width in world units
		Vector3 hnw = camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,camera.nearClipPlane));
		screenHeight = hnw.y;
		screenWidth = hnw.x;
		
		laneHeight = screenHeight / (float) numLanes;
		
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
			spawners[i+3] = Instantiate(spawnerPrefab, new Vector3(screenWidth, (float)i * laneHeight, 0), Quaternion.identity) as GameObject;
			i++;
		}
		
		//Setup the ships
		i=-1;
		foreach(GameObject ship in ships) {
			ships[i+1] = Instantiate(shipPrefab, new Vector3(shipSpawnOffset, (float)i * laneHeight, 0), Quaternion.identity) as GameObject;
			i++;
		}
	}
	
	public void Reset() {
		Setup();
	}
	
	public float GetLaneHeight(){
		return laneHeight;
	}
	
	public float GetGameSpeed() {
		return gameSpeed;
	}
}
