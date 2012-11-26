using UnityEngine;
using System.Collections;

//EM needs to activate EditorCamera and deactivate MainCamera in start()

public class EditorCamera : MonoBehaviour {
	
	public float scrollSpeed;
	
	protected EditorManagement em;
	
	public Texture2D[] buttons;
	public Texture2D[] PSL;
	
	//protected EditorManagement em;
	
	private float leftBound;
	private float rightBound;
	

	// Use this for initialization
	void Start () {
		em = GetComponent<EditorManagement>();
		leftBound=0;
		rightBound = em.GetRows() * em.GetTileHeight();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound,rightBound), 0, 0);
		transform.Translate(scrollSpeed*Vector3.right*Input.GetAxis("Horizontal")*Time.deltaTime);
		transform.Translate(scrollSpeed*Vector3.up*Input.GetAxis("Vertical")*Time.deltaTime);
	}
	
    void OnGUI() {
        
		float dist = ((Screen.width)*0.9f)/buttons.Length;
		
		Debug.Log(dist);
		
		//makes item buttons
		for(int j = 0; j<buttons.Length;j++){
			if(GUI.Button(new Rect(j*dist, Screen.height/1.15f, Screen.width/20.0f, Screen.width/20.0f), buttons[j])){
				em.SetState(j);
			}
		}
        
		//makes play, save, and load buttons
		if(GUI.Button (new Rect(Screen.width*0.95f,Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f),PSL[0])){
			em.Play();
		}
		if(GUI.Button (new Rect(Screen.width*0.90f,Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f),PSL[1])){
			em.Load();
		}
		if(GUI.Button (new Rect(Screen.width*0.85f,Screen.height-Screen.height/8.0f,Screen.height*0.1f,Screen.height*0.1f),PSL[2])){
			em.Save ();
		}
        
    }
	
}
