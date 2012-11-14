using UnityEngine;
using System.Collections;

public class Wrap : MonoBehaviour {
	float topbound, bottombound;
	public float toplane, bottomlane;
	GameManagement mgmt;
	// Use this for initialization
	void Start () {
		mgmt = Camera.main.GetComponent<GameManagement>();
		bottombound = 0;
		//topbound = mgmt.sHeight;
		//toplane = topbound - mgmt.laneHeight/2;
		//bottomlane = bottombound + mgmt.laneHeight/2;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(renderer.bounds.max.y > topbound){
			Vector3 newpos  =  new Vector3(transform.position.x, bottomlane, transform.position.z); 
			transform.position = newpos;
		}
		else if(renderer.bounds.max.y <bottombound){
			Vector3 newpos  =  new Vector3(transform.position.x, toplane, transform.position.z); 
			transform.position = newpos;

		}
		
	}
}
