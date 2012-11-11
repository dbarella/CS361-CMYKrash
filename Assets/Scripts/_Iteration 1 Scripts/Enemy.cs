
using UnityEngine;
using System.Collections;
//wraparound not implimented. Something to think about

//Last modified by: Brendan, Thursday 11/8 @ 7 PM

public class Enemy : MonoBehaviour {
	public float speedMult=3;
	public bool  isRandom=false;
	//Reference to the game management
	public int damageDealt=2;
	private float laneHeight;
	private float sWidth;
	private float sHeight;
	protected GameManagement mgmt;
	
	//Timer vars
	private float timer;

	// Use this for initialization
	void Start () {
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		laneHeight = mgmt.laneHeight;
		//get length and width of the board
		sHeight = mgmt.sHeight;
		sWidth = mgmt.sWidth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(-1*speedMult,0,0));
		timer -= Time.deltaTime;
		
		//Random Movement
		if(isRandom){
			if(timer <= 0) {
				int newMove = Random.Range(-1,2); //Pick a random number from -1, 0 or 1
				
				if(newMove < 0) {
					MoveDown();
				} else if(newMove > 0) {
					MoveUp();
				}
				timer = Random.Range(.5f, 2.0f);
			}
		}
	}
	
	//moves the enemy down a lane
	private void MoveUp(){
		if(transform.position.y <= sHeight-laneHeight){
			transform.Translate(new Vector3(0,laneHeight,0),Space.World);
		}
	}
	
	//moves the enemy up a lane
	private void MoveDown(){
		if(transform.position.y >= laneHeight){
			transform.Translate(new Vector3(0,-laneHeight,0),Space.World);
		}
	}
	
	void OnTriggerEnter(Collider col) {
		Debug.Log("Enemy hit something");
		if(col.gameObject.tag == "Player"){
			Debug.Log("That something was a player");
			int shipNo = col.gameObject.GetComponent<Ship>().getNo();
			mgmt.PlayerHit(damageDealt, shipNo);
			Destroy(gameObject);
		}
		if(col.gameObject.tag == "Planet"){
			Destroy(gameObject);
	}
}
	
	void Die(){
		Destroy (gameObject);
	}
}
 