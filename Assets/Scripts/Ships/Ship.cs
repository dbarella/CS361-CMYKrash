using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Last modified by: Brendan, Monday 11/11 @ 3 AM

/*
Finished:
GetItem
TakeDamage
Die
Move

TODO:

RenderShield



*/

public class Ship : MonoBehaviour {
	
	
		
	/* SCRIPT REFERENCES */
	
	protected GameManagement mgmt;
	//Note: Do I attach the weapon reference here, or in the subclasses?  I'm leaning towards the subclasses for now,
	//but I can do it here if you want, let me know - bn
	
	/* GAMEOBJECTS */
	
	public GameObject explosion; //for a later implementation, an explosion upon death
	public GameObject shield; //I think I need this?  Or am I crazy?
		
	/* VARIABLES */
	
	//health
	public float health=7.0f;//Question:  Shouldn't this be an int?  -bn
	
	//powerup bools
	private bool shielded = false;
	private bool powerShot = false;

	
	
	// Use this for initialization
	void Start () { 
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		Move();
		
	}
	
	
	public void Move(){
		//Input keys for changing of the lanes.  All should be pretty self-explanatory
		if(Input.GetButtonDown("Up")){
			transform.Translate(new Vector3(0,mgmt.GetLaneWidth(),0)*Time.deltaTime,Space.World);
		}
		if(Input.GetButtonDown("Down")){
			transform.Translate(new Vector3(0,mgmt.GetLaneWidth(),0)*Time.deltaTime,Space.World);
		}
		if(Input.GetButtonDown ("Right")){
			if(transform.position.x < (Screen.width / 2)){//this doesn't allow the player to go beyond halfway across the screen.
				transform.Translate(new Vector3(0,1,0)*speed*Time.deltaTime,Space.World);	
			}
		}
		if(Input.GetButtonDown ("Left")){ 
			if(transform.position.x > (Camera.main.WorldToScreenPoint(new Vector3(0,0)).x)) {//this doesn't allow the player to go off the left side.
				transform.Translate(new Vector3(0,-1,0)*speed*Time.deltaTime,Space.World);	
			}
		}
		
	}
	
	
	
	
	public void TakeDamage(int damage){
		if(shielded){//If we have a shield
			shielded = false;//pop it
		}else{//otherwise
			health-=damage;//take the hit
			Debug.Log (health);//log it
			if(health<=0){//check to see if the ship needs to die,
				Die(mgmt);//and kill it if it does
			}
		}
	}
	
	//Method to render the shield
	public void RenderShield(){
		if(shielded) {
			//Note:  I currently don't really know how to do this, because I have yet to implement Shield.
			//I'm leaving this a skeleton for now, and will return to it later - bn
		} else {
			
		}	
	}
	

	
	

	//Just in case, GetHealth
	public float GetHealth() {
		return health;
	}
	
	//Sets powerups based on what is collected.
	public void getItem(String itemTag) {
		if(itemTag == "Shield") {
			shielded = true;
		}
		if(itemTag == "PowerShot") {
			powerShot = true;
		}	
	}
	
	
	
	
	
	//death function
	public void Die(GameManagement mgmt) {
	
		Debug.Log("ship is out of healths.  Calling mgmt.PlayerDead()");
		//FYI: Whoever's writing the cute comments needs to stop, it was cute for a while but now its annoying - bn
		mgmt.PlayerDead(shipNo);
	}
	
}
