using UnityEngine;
using System.Collections;
using System;

//EM needs to activate EditorCamera and deactivate MainCamera in start()

public class EditorCamera : MonoBehaviour {
	
	public float scrollSpeed;
	
	protected EditorManagement em;
	
	public Texture2D[] buttons;
	public Texture2D[] PSL;
	string filename, xText, yText;
	//protected EditorManagement em;
	
	private float leftBound;
	private float rightBound;
	

	// Use this for initialization
	void Start () {
		em = GetComponent<EditorManagement>();
		leftBound=0;
		rightBound = em.GetRows() * em.GetTileHeight();
		filename = "level";
		xText = "7";
		yText = "30";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound,rightBound), 0, 0);
		transform.Translate(scrollSpeed*Vector3.right*Input.GetAxis("Horizontal")*Time.deltaTime);
		transform.Translate(scrollSpeed*Vector3.up*Input.GetAxis("Vertical")*Time.deltaTime);
	}
	
    void OnGUI() {
        
		float dist = ((Screen.width)*0.65f)/buttons.Length;
		
		//Debug.Log(dist);
		//makes resize text boxes
		xText = GUI.TextField (new Rect (0, Screen.height-Screen.height/8.0f,Screen.width/20.0f, Screen.width/20.0f), xText, 40);
		yText = GUI.TextField (new Rect (Screen.width*0.055f, Screen.height-Screen.height/8.0f,Screen.width/20.0f, Screen.width/20.0f), yText, 40);
		if(GUI.Button (new Rect(Screen.width*0.11f,Screen.height-Screen.height/8.0f,Screen.width/19.0f, Screen.width/20.0f),"Resize\nBoard")){
			int x = Convert.ToInt32(xText);
			int y = Convert.ToInt32(yText);
			em.ResizeBoard(x,y);
		}
		
		//makes item buttons
		float offset = Screen.width*0.17f;
		for(int j = 0; j<buttons.Length;j++){
			if(GUI.Button(new Rect(j*dist+offset, Screen.height/1.15f, Screen.width/20.0f, Screen.width/20.0f), buttons[j])){
				em.SetState(j);
			}
		}
		//textbox to change filename
		filename = GUI.TextField (new Rect (Screen.width*0.8f, Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f), filename, 40);
		//makes play, save, and load buttons
		if(GUI.Button (new Rect(Screen.width*0.95f,Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f),PSL[0])){
			em.Play();
		}
		if(GUI.Button (new Rect(Screen.width*0.90f,Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f),PSL[1])){
			em.Load(filename);
		}
		if(GUI.Button (new Rect(Screen.width*0.85f,Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f),PSL[2])){
			em.Save(filename); 
		}
        
    }
	public Texture2D[] GetTextures(){
		return buttons;
	}
	
}
