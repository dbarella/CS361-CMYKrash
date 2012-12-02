using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {
	 
	protected GameManagement mgmt;
	
	private float rightBound;
	private float leftBound;
	private float screenWidth;
	
	public float scrollSpeed=0.5f;
	public Texture2D bumpMap;
	
	void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
		Scale();
		rightBound = Camera.main.ScreenToWorldPoint( new Vector3(0, Camera.main.GetScreenWidth(), 0) ).x;
		leftBound = -rightBound;
		screenWidth = 2.0f*rightBound;
		
	}
	
	// In order for this to scroll in accordance with player movements you'll need to go to
	//Edit->project settings -> input
	void FixedUpdate () {
		float moveCompound = Input.GetAxis("Horizontal") *0.1f;
		float offset = Time.time * scrollSpeed*0.1f;
		renderer.material.mainTextureOffset = new Vector2(-offset,0);
	}
	
	//scales the object assuming it is instantiated at (0,0,20)
	void Scale(){
		float x = mgmt.GetScreenWidth()/5.0f;
		float y = mgmt.GetScreenHeight()/7.0f;
		Vector3 rescale = new Vector3(x,0,y);
		transform.localScale = rescale;
	}
}
