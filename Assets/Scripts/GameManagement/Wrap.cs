using UnityEngine;
using System.Collections;

public class Wrap : MonoBehaviour {
	
	GameManagement mgmt;
	private float topBound;
	private float bottomBound;
	private float screenHeight;
	
	// Use this for initialization
	void Start () {
		topBound = Camera.main.ScreenToWorldPoint( new Vector3(0, Camera.main.GetScreenHeight(), 0) ).y;
		bottomBound = -topBound;
		screenHeight = 2.0f * topBound;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y >= topBound) {
			transform.Translate( new Vector3(0, - screenHeight, 0) );
		} else if(gameObject.transform.position.y <= bottomBound) {
			transform.Translate( new Vector3(0, screenHeight, 0) );
		}
	}
}
