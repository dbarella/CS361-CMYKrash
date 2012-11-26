using UnityEngine;
using System.Collections;

public class EditorManagement : MonoBehaviour {
	//set to appropriate prefabs
	public GameObject[] list;
	//keeps track of current button pressed
	private int currentState;
	//screen stuff
	private float screenHeight, screenWidth;
	private float tileHeight;
	//board stuff
	public GameObject tilePrefab;
	private int rows, cols;	
	private Tile[,] board;
	public Material[] materialList;
	
	private float tileOffset = 1.0f;
	
	// Use this for initialization
	void Start () {
		//screen
		//Vector3 hnw = camera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,camera.nearClipPlane));
		
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		//initial board
		currentState = 0;
		rows = 7;
		cols = 30;
		board = new Tile[rows,cols];
		tileHeight = screenHeight / rows;
		Debug.Log("tile height: "+tileHeight);
		//init tile
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < cols; j++){
				InitTile (i,j,board);
			}
		}
	}
	
	public void ResizeBoard(int x, int y){
		tileHeight = screenHeight / x;
		Tile[,] newBoard = new Tile[x,y];
		for(int i = 0; i < x; i++){
			for(int j = 0; j < y; j++){				
				//if it exists
				if(i < rows && j < cols){ 
					//copy
					newBoard[i,j] = board[i,j];
					//but move it to the new position
					newBoard[i,j].gameObject.transform.Translate(new Vector3(i*tileHeight, j*tileHeight, 0));
				}
				//otherwise make a new one
				else{
					InitTile(i,j,newBoard);
				}
			}
		}
		//this is the new board
		rows = x;
		cols = y;
		board = newBoard;
		
	}
	//make a new tile
	private void InitTile(int i, int j, Tile[,] board){
		
		Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(j*tileHeight+tileHeight/2.0f, i*tileHeight+tileHeight/2.0f, 0));
		position.z = tileOffset;
		GameObject go = Instantiate(tilePrefab, position, tilePrefab.transform.rotation) as GameObject;
		board[i,j] = go.GetComponent<Tile>();
		//for some reason all of the Tiles are instantiating at the same pos. I will not attempt to debug as I am exhausted -js
		Debug.Log ("InitTile ("+i+","+j+"): Posit: "+ position + ". TileHeight: " + tileHeight);
		//go.transform.localScale = new Vector3 ((tileHeight/2.0f)*0.9f, 1, (tileHeight/2.0f)*0.9f);
	}
	
	void FixedUpdate () {
		//check for clicks
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				Debug.Log ("clicked");
				Tile t = hit.collider.gameObject.GetComponent<Tile>();
				if(t != null)
					t.SetObj(currentState);
				//otherwise it's something else, what do we do then? anything? can this happen?
			}
		}
	}
	//getters and setters
	public void SetState(int newState){
		currentState = newState;
	}
	public int GetState(){
		return currentState;
	}
	public Material[] GetMaterialList(){
		return materialList;
	}
	//need to do these
	public void Play(){
		Debug.Log("Play");
	}
	public void Load(){
		Debug.Log("Load");

	}
	public void Save(){
		Debug.Log("Save");

	}
	
	public int GetRows(){
		return rows;
	}
	 public float GetTileHeight(){
		return tileHeight;
	}
}
