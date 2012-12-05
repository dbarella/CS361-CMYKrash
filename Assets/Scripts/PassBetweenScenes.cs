using UnityEngine;
using System.Collections;

public class PassBetweenScenes : MonoBehaviour {
	int[][] board;
	GameObject[] spawnablePrefabs;
	string fname;	//The file to open, if we're opening from 
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
	public void SetFname(string s){
		fname = s;
	}
	public string GetFname(){
		return fname;	
	}
}
