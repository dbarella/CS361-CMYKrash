using UnityEngine;
using System.Collections;

public class PassBetweenScenes : MonoBehaviour {
	int[][] board;
	GameObject[] spawnablePrefabs;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	public void setScene (int[][] board,GameObject[] spawnables) {
		this.board = board;
		spawnablePrefabs = spawnables;
	}
	public int[][] getScene() {
		return board;
	}
	public GameObject[] getSpawnables(){
		return spawnablePrefabs;
	}
}
