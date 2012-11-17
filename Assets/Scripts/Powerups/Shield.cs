using UnityEngine;
using System.Collections;

//Last edited by Brendan at 9PM, 11/13/12


public class Shield : PowerUp { 

	// Use this for initialization
	void Start () {
		gm = Camera.main.GetComponent<GameManagement>();
		speedMult = 10.0f;
		damage = 0.0f;
		health = 1.0f;
		laneChanging = false;
		isShield = true;
	}
	
	// Update is called once per frame
	void Update () {//all update needs to do is move the shield to the left.
		Move();
	}
	
	
	
	void OnTriggerEnter(Collider other) {//collider to give powerup!
		if(other.gameObject.CompareTag("Player")) {//if collides with a ship
			this.Die(); //Destroy self
		} 	
	}
	
	
}
