using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class EditorManagement : MonoBehaviour {
	//set to appropriate prefabs
	public GameObject[] list;
	public GameObject levelData;
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
		MaterialSetup();
		Setup ();
	}
	private void MaterialSetup(){
		Texture2D[] textures = Camera.mainCamera.GetComponent<EditorCamera>().GetTextures();
		materialList = new Material[textures.Length];
		for(int i = 0; i < textures.Length; i++){
			materialList[i] = new Material(Shader.Find("Diffuse"));
			materialList[i].mainTexture = textures[i];
		}
	}
	//sets up a blank board
	private void Setup(){
		//initial board
		currentState = 0;
		rows = 7;
		cols = 30;
		board = new Tile[rows,cols];
		tileHeight = screenHeight / rows*0.85f;
		//Debug.Log("tile height: "+tileHeight);
		//init tile
		for(int i = 0; i < rows; i++){
			for(int j = 0; j < cols; j++){
				InitTile (i,j,board);
			}
		}
	}
	private void Setup(int width, int height, int[,] data){
		ResizeBoard (width,height);
		for(int i = 0; i < width; i++){
			for(int j = 0; j<height; j++){
				Debug.Log ("Setup tile: data[i,j]");
				board[i,j].SetObj(data[i,j]);
			}
		}
	}
	public void ResizeBoard(int x, int y){
		tileHeight = screenHeight / x*0.85f;
		Tile[,] newBoard = new Tile[x,y];
		for(int i = 0; i < x; i++){
			for(int j = 0; j < y; j++){				
				
				InitTile(i,j,newBoard);
				if(i < rows && j < cols){ 
					//Debug.Log ("new tile: "+newBoard[i,j]);
					newBoard[i,j].SetObj(board[i,j].GetObj());
				}
			}
		}
		for(int i = 0; i < rows; i ++){
			for(int j = 0; j < cols ; j++){
				board[i,j].Die();

			}
			
		}
		//this is the new board
		rows = x;
		cols = y;
		board = newBoard;
		
	}
	//make a new tile
	private void InitTile(int i, int j, Tile[,] board){
		
		Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(j*tileHeight+tileHeight/2.0f, (rows-i)*tileHeight-tileHeight/2.0f+0.15f*screenHeight, 0));
		position.z = tileOffset;
		Debug.Log (rows+" "+i);
		GameObject go = Instantiate(tilePrefab, position, tilePrefab.transform.rotation) as GameObject;
		board[i,j] = go.GetComponent<Tile>();
		//for some reason all of the Tiles are instantiating at the same pos. I will not attempt to debug as I am exhausted -js
		//Debug.Log ("InitTile ("+i+","+j+"): Posit: "+ position + ". TileHeight: " + tileHeight);
		go.transform.localScale = new Vector3 (2.5f,2.5f,2.5f);
		board[i,j].SetMaterial(materialList);
	}
	
	void FixedUpdate () {
		//check for clicks
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				//Debug.Log ("clicked");
				Tile t = hit.collider.gameObject.GetComponent<Tile>();
				if(t != null)
					t.SetObj(currentState);
				//otherwise it's something else, what do we do then? anything? can this happen?
			}
		}
	}
	//getters and setters
	public void SetState(int newState) {
		currentState = newState;
	}
	public int GetState() {
		return currentState;
	}
	public Material[] GetMaterialList() {
		return materialList;
	}
	public void SetTile(Tile t, int currentState) {
		t.SetObj(currentState);
	}
	public GameObject[] GetGOList(){
		return list;
	}
	public void Play() {
		Debug.Log("Play");
		GameObject go = Instantiate(levelData, levelData.transform.position, levelData.transform.rotation) as GameObject;
		go.GetComponent<PassBetweenScenes>().setScene(MixToPlayable(), list);
		Application.LoadLevel("Game");
	}
	public void Load(string fname) {
		//fname = Application.dataPath+"/Resources/Maps/" + fname + ".txt";
		TextAsset file = Resources.Load("Maps/"+fname) as TextAsset;
		Debug.Log (fname);
		string[] lines = file.text.Split('\n');
		int prefabLength = Convert.ToInt32(lines[0]);
		for(int i = 1; i < prefabLength; i++){
			string temp = lines[i];
			list[i] = Resources.Load("Spawnables/"+temp) as GameObject;
			Debug.Log (list[i]);
		}
		int width = Convert.ToInt32(lines[prefabLength]);
        Debug.Log (width);
		int height = Convert.ToInt32(lines[prefabLength+1]);
		string[] rows = new string[width];
		for(int i = prefabLength+2; i < prefabLength+2+width; i++){
			rows[i-prefabLength-2] = lines[i];
		}
		int[,] board = LoadBoard (rows, width,height);
		Setup (width,height,board);
		/*if (!File.Exists(fname))
        {
			Debug.Log ("File "+fname+" not found");
		}
		else{
			Debug.Log ("Loading...");
			 using (StreamReader sr = File.OpenText(fname))
        	{
				int prefabLength = Convert.ToInt32(sr.ReadLine());
				for(int i = 1; i < prefabLength; i++){
					string temp = sr.ReadLine();
					Debug.Log (temp);
					list[i] = Resources.Load("Spawnables/"+temp) as GameObject;
					Debug.Log (list[i]);
				}
				int width = Convert.ToInt32(sr.ReadLine());
            	int height = Convert.ToInt32(sr.ReadLine ());
				
				int[,] board = LoadBoard (sr, width,height);
				Setup (width,height,board);
        	}
		}*/
	}

	private int[,] LoadBoard(string[] rows, int width, int height){
		int[,] board = new int[width,height];
        for(int i = 0; i < width; i ++){
			string[] line = rows[i].Split(new Char[]{' '});
			for(int j = 0; j < height; j++){
				board[i,j] = Convert.ToInt32(line[j]);
				}
		}
		return board;
	}
	public void Save(string fname) {
		Debug.Log("Saving...");
		using (StreamWriter sw = new StreamWriter(Application.dataPath+"/Resources/Maps/" + fname + ".txt"))
		{
			string[] strings = MixDown();
			//don't write 0--that's always null
			sw.WriteLine (list.Length);
			for(int i = 1; i < list.Length; i++){ 
				sw.WriteLine(list[i].name);
			}
			sw.WriteLine(rows);
			sw.WriteLine(cols);
			foreach (string s in strings){
					sw.WriteLine(s);
			}
		}
		Debug.Log("Save Complete.");
	}
	
	public int GetRows() {
		return rows;
	}
	 public float GetTileHeight() {
		return tileHeight;
	}
	
	public string[] MixDown() {
		string[] s = new string[rows];
		for(int i = 0; i < rows; i++) {
			string temp = "";
			for(int j = 0; j < cols; j++) {
				Debug.Log (i+" "+j+" "+board[i,j]);
				temp = temp + board[i,j].GetObj()+" ";
			}
			s[i] = temp;
		}
		return s;
	}
	public int[][] MixToPlayable(){
		int[][] s = new int[rows][];
		for(int i = 0; i < rows; i++) s[i] = new int[cols];
		for(int i = 0; i < rows; i++) {
			string temp = "";
			for(int j = 0; j < cols; j++) {
				s[i][j] = board[i,j].GetObj();
			}
		}
		return s;
	}
}
