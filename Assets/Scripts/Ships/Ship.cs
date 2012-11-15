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
	
	protected GameManagement mgmt;//GameManagement
	protected Weapon weapon;//attached weapon
	
	/* GAMEOBJECTS */
	
	public GameObject explosion; //for a later implementation, an explosion upon death
	public GameObject shield; //I think I need this?  Or am I crazy?
		
	/* VARIABLES */
	
	//health
	public int health=7;
	
	//for use in Move();
	protected float laneHeight;
	protected float target;
	protected bool laneChanging;
	public int curLane; //this should be set in GameManagement upon instantiation.
	
	//powerup bools (both start as false)
	private bool shielded = false;
	private bool powerShot = false;

	
	
	// Use this for initialization
	void Start () { 
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		//storing laneheight for future use
		laneHeight = mgmt.GetLaneHeight();
		laneChanging = false;
		target = transform.position.y;
		//Source the weapon? or is that part of the prefab?
		
		
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Move();
		
	}
	
	
	public void Move(){
		
		//Input keys for changing of the lanes.  
		if(Input.GetButtonDown("Up") && !laneChanging){//if we want to move up, and we're not currently changing lanes
			curLane--;
			if(curLane < 1) { //if we need to wrap around top:
				curLane = 7;//set to bottom lane
				transform.position.y = transform.position.y - (6.0f * laneHeight);//port ship over there (6 lanes down)
				target = transform.position.y;//and reset the target.
			}else{//otherwise
				target = transform.position.y + laneHeight();//we set our target
				laneChanging = true;//and we lock out the up+down keys
				while( transform.position.y <= target) {//for this while loop
					transform.Translate(mgmt.GetGameSpeed()*Time.fixedDeltaTime*new Vector3(0,1,0));//that does the moving!
				}
				laneChanging = false;//at the end we allow more key presses
			}
		}
		
		
		if(Input.GetButtonDown("Down") && !laneChanging){
			curLane++;
			if(curLane > 7) {//if we need to wrap around bot:
				curLane = 1; //set to top lane
				transform.position.y = transform.position.y + (6.0f * laneHeight);//port ship over there (6 lanes up)
				target = transform.position.y; //and reset the target.
			}else{//otherwise
				target = transform.position.y - laneHeight();//we set our target
				laneChanging = true;//and we lock out the up+down keys
				while( transform.position.y >= target) {//for this while loop
					transform.Translate(mgmt.GetGameSpeed()*(-1.0f)*Time.fixedDeltaTime*new Vector3(0,1,0));//that does the moving!
				}
			laneChanging = false;//at the end we allow more key presses
			}
		}
		if(Input.GetButtonDown ("Right")){
			if(transform.position.x < (Screen.width / 2)){//this doesn't allow the player to go beyond halfway across the screen.
				transform.Translate(new Vector3(1,0,0)*Time.fixedDeltaTime);	
			}
		}
		if(Input.GetButtonDown ("Left")){ 
			if(transform.position.x > (Camera.main.WorldToScreenPoint(new Vector3(0,0)).x)) {//this doesn't allow the player to go off the left side.
				transform.Translate(new Vector3(1,0,0)*(-1.0f)*Time.fixedDeltaTime);	
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
			//Note:  I am a dumbass about these sorts of things - I have no clue how to implement the render :(
		} else {
			
		}	
	}
	

	
	

	//Just in case, GetHealth
	public float GetHealth() {
		return health;
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.gameObject.getTag("Enemy")) {
			TakeDamage(other.gameObject.damage);
		}
		if(other.gameObject.getTag("PowerUp")) {//otherwise, if we have a powerup,
			if(other.gameObject.IsShield) { //if it's a shield
				shielded = true;//turn on shield
			}else{//otherwise, it has to be a powershot
				weapon.GivePowerSHot();
			}
		}
		
		
	}
	//OBSOLETE, KEEPING FOR REFERENCE, CAN DELETE LATER - bn
	//Sets powerups based on what is collected.
	/*public void getItem(string itemTag) {
		if(itemTag == "Shield") {
			shielded = true;
		}
		if(itemTag == "PowerShot") {
			powerShot = true;
		}	
	}
	*/
	
	
	
	
	//death function
	public void Die(GameManagement mgmt) {
	
		Debug.Log("ship is out of healths.  Calling mgmt.PlayerDead()");
		mgmt.ShipDied();//Lets the GM know a ship is dead(do we care anymore?)
		Destroy(gameObject);//and destroys itself.
		
	}
	
}
