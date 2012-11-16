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
	public float health=7.0f;
	
	//for use in Move();
	public float shipSpeed = 10.0f;
	//how much faster lane changing goes than regular movement
	public float laneChangeMult = 2.0f;
	protected float laneHeight;
	protected float amtMoved;
	protected bool laneChanging;
	protected int direction;
	//public int curLane; //this should be set in GameManagement upon instantiation.
	
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
		//Source the weapon? or is that part of the prefab?	
	}
	
	
	// Update is called once per frame
	void Update () {
		Move();
		
	}
	
	
	public void Move(){
		if(laneChanging)
			ChangeLanes (direction);
		//Input keys for changing of the lanes.  
		else if(Input.GetKeyDown("w")){//if we want to move up, and we're not currently changing lanes
			direction = 1;
			ChangeLanes(direction);
		}
		else if(Input.GetKeyDown("s")){
			direction = -1;
			ChangeLanes(direction);
		}
		if(Input.GetKey ("d")){
			if(transform.position.x < (mgmt.GetScreenWidth() / 2)){//this doesn't allow the player to go beyond halfway across the screen.
				transform.Translate(new Vector3(1,0,0)*Time.deltaTime*shipSpeed);	
			}
		}
		if(Input.GetKey ("a")){ 
			Debug.Log(Camera.main.WorldToScreenPoint(new Vector3(0,0,0)).x);
			if(transform.position.x > (Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x)) {//this doesn't allow the player to go off the left side.
				transform.Translate(new Vector3(1,0,0)*(-1.0f)*Time.deltaTime*shipSpeed);	
			}
		}
		
	}
	
	//helper method for changing lanes. direction is -1 or 1 depending on whether ship is going up or down.
	private void ChangeLanes(int direction){
		
		print(laneChanging+" "+amtMoved);
		if(!laneChanging){
			laneChanging = true;
			amtMoved = 0;
		}
		else if(amtMoved >= laneHeight){
			laneChanging = false;
			this.direction = 0;
		}
		else{
			Vector3 translateVector = new Vector3(0,laneChangeMult*shipSpeed*(float)direction*Time.deltaTime,0);
			if(amtMoved + Mathf.Abs(translateVector.y) > laneHeight){
				translateVector = (Vector3.up*(float)direction*(Mathf.Abs(laneHeight-amtMoved)));
				laneChanging = false;
				this.direction = 0;
			}
			transform.Translate(translateVector);
			amtMoved += Mathf.Abs(translateVector.y);
		}
	}


	
	
	public void TakeDamage(float damage){
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
		if(other.gameObject.CompareTag("Enemy")) {
			TakeDamage(other.gameObject.GetComponent<Spawnable>().GetDamage());
		}
		if(other.gameObject.CompareTag("PowerUp")) {//otherwise, if we have a powerup,
			if(other.gameObject.GetComponent<PowerUp>().IsShield()) { //if it's a shield
				shielded = true;//turn on shield
			}else{//otherwise, it has to be a powershot
				weapon.PowerShot();
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
		//mgmt.ShipDied();//Lets the GM know a ship is dead(do we care anymore?) /*Commented out by Emma for now as this method needs an int argument--which ship it is, I think?-- and if the ship knows this about itself, I can't find it. If this actually needs to be here, that'll need to be fixed for real.*/
		Destroy(gameObject);//and destroys itself.
		
	}
	
}
