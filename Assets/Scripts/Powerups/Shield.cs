using UnityEngine;
using System.Collections;

//Last edited by Brendan at 9PM, 11/13/12


public class Shield : PowerUp { 

	// Use this for initialization
	void Start () {
		gm = Camera.main.GetComponent<GameManagement>();
		damage = 0.0f;
		health = 1.0f;
		laneChanging = false;
		isShield = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {//all update needs to do is move the shield to the left.
		base.Move();
	}
	
	
	
	void OnTriggerEnter(Collider other) {//collider to give powerup!
		if(other.gameObject.CompareTag("Player")) {//if collides with a ship
			this.Die(); //Destroy self
		} 	
	}
	
	
}
