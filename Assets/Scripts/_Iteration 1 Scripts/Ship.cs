using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class Ship : MonoBehaviour {
	public int shipNo = 0;
	public int health=6;
	public float speed = 1;
	//Reference to the game management
	protected GameManagement mgmt;
	//Reference to the HUD.
	//lane height, derived from Game Managment
	private float laneHeight;
	//awww yeahh
	public GameObject explosion;
	public GameObject bullet;
	//boundaries of the board
	private float sHeight;
	private float sWidth;
	//Power-ups
	public Stack<PowerUp> balloon = new Stack<PowerUp>();
	//can fire?
	public bool canFire=true;
	public int cooldown=3;
	
	//for invincibility powerup
	private float invincibilityCounter;
	// Use this for initialization
	void Start () {
		health = 6;
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		//laneHeight = mgmt.laneHeight;
		//get boundaries of the board
		//sHeight = mgmt.sHeight;
		//sWidth = mgmt.sWidth;
		invincibilityCounter = 0;
	}
	
	
	// Update is called once per frame
	void Update () {
		if(invincibilityCounter > 0){
			invincibilityCounter -= Time.deltaTime;
		}
		if(Input.GetButtonDown("Up")){
			MoveUp();
		}
		if(Input.GetButtonDown("Down")){
			MoveDown();
		}
		if(Input.GetAxis("Horizontal") != 0)
			Move();
		if(Input.GetKeyDown ("z")){
			balloon.Pop().onActivate();
		}
		if(Input.GetButtonDown ("Jump")) {
			if(canFire){
				canFire=false;
				StartCoroutine(Fire());
			}
		}
	}
	
	IEnumerator Fire(){
		GameObject o = Instantiate(bullet, this.transform.position, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(cooldown);
		canFire = true;
	}
	
	public void TakeDamage(int damage){
		if(invincibilityCounter <= 0){
			health-=damage;
			Debug.Log (health);
			if(health<=0){
				Die(mgmt);
			}
		}
	}
	
	
	
	//for moving left and right, bounds are the sides of the screen
	private void Move(){
		Vector3 toMove = Input.GetAxis("Horizontal")*speed*Time.deltaTime*Vector3.right;
		if(transform.position.x + toMove.x <= sWidth && transform.position.x + toMove.x >= 0)
			transform.Translate(toMove);
	}
	
	//moves the player down a lane
	private void MoveUp(){
		//if(transform.position.y <= sHeight-laneHeight){
			transform.Translate(new Vector3(0,laneHeight,0),Space.World);
		//}
	}
	
	//moves the player up a lane
	private void MoveDown(){
		//if(transform.position.y >= laneHeight){
			transform.Translate(new Vector3(0,-laneHeight,0),Space.World);
		//}
	}
	
	public int GetHealth(){
		return health;
	}
	
	public int getNo(){
		return shipNo;	
	}
	
	public void setNo(int i){
		shipNo = i;
	}
	
	IEnumerator Wait(int i){
		yield return new WaitForSeconds(i);
	}
	
	//duh
	public void AddPowerUp(PowerUp p){
		Debug.Log("Ship: Powerup get!");
		balloon.Push(p);
	}
	public void MakeInvincible(float seconds){
		if(invincibilityCounter < 0) invincibilityCounter = 0;
		invincibilityCounter += seconds;
	}
	//death function
	public void Die(GameManagement mgmt){
		//Boom!
//		GameObject d = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		//goodbye
		Debug.Log("ship is out of healths.  Calling mgmt.PlayerDead()");
		//Hello, management? Yes, the player's dead. He won't be a problem anymore.
		mgmt.ShipDied(shipNo);
	}
	
}
