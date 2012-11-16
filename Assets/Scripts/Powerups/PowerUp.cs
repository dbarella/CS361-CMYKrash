using UnityEngine;
using System.Collections;

public class PowerUp : Spawnable {
	
	public bool isShield; //boolean value for shield - true if shield, false if powershot
	// Use this for initialization
	void Start () {
		//initialize values for powerup
		gm = Camera.main.GetComponent<GameManagement>();
		speedMult = 10.0f;
		damage = 0.0f;
		health = 1.0f;
		laneChanging = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move();
	}
	
	public bool IsShield(){
		return isShield;	
	}
	
}
