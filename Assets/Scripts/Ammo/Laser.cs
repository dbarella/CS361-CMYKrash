using UnityEngine;
using System.Collections;

public class Laser : Ammo {

	void Start(){
		//On startup, immediately shift forward so the edge will be on the ship, rather than the laser being half forward and half backwards.
		transform.Translate(new Vector3((transform.position.x+(Screen.width/3)), 0, 0));
		transform.Rotate(90,90,0);
//		Debug.Log("Current position" + transform.position);
	}

	void Update(){
		//OnTriggerEnter is guarenteed to be called before this, so just die.
		Die();
	}
}