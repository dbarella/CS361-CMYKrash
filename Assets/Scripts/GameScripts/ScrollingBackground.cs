using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {
	 
	protected GameManagement mgmt;

	// Use this for initialization
	void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
		Scale();
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//scales the object assuming it is instantiated at (0,0,20)
	void Scale(){
		float x = mgmt.GetScreenWidth()/2.0f;
		float y = mgmt.GetScreenHeight()/2.0f;
		Vector3 rescale = new Vector3(x,0,y);
		transform.localScale = rescale;
	}
}
