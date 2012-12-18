using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Last modified by: Brendan, Monday 11/11 @ 3 AM

public class Ship : MonoBehaviour {
	
	/* SCRIPT REFERENCES */
	
	public GameObject explosion;
	private AudioSource audioSource;
	public AudioClip deathBlip;
	public float deathBlipVolume = 0.5f;
	private ParticleSystem shieldAnim; //shield charge up particle system
	public AudioClip shieldBlip;
	public float shieldBlipVolume = 0.5f;
	
	public Texture2D inPain;
	public Texture2D aOk;
	
	protected GameManagement mgmt;//GameManagement
	protected Weapon weapon;//attached weapon
		
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
	
	//powerup bools (both start as false)
	private bool shielded = false;
	private bool powerShot = false; //I guess we never did get around to power shot, did we? Well maybe one day we can come back. This isn't a bad idea -js
	
	//Prototype vars
	private int shipInstance;
	private bool laneSplit;
	
	private bool dead = false;
	
	// Use this for initialization
	void Start () { 
		//sheild animation particle system
		shieldAnim = GetComponent<ParticleSystem>();
		//Source audio
		audioSource = GetComponent<AudioSource>();
		//Source the Game Management
		mgmt = Camera.main.GetComponent<GameManagement>();
		//storing laneheight for future use
		laneHeight = mgmt.GetLaneHeight();
		
		laneChanging = false;
		laneSplit = false;
	}
	
	void FixedUpdate(){
		Move();	
	}
	
	// Update is called once per frame
	void Update () {
		RenderShield ();
		RenderPoweredUp();
	}
	
	
	public void Move(){
		if(laneChanging) {
			ChangeLanes (direction);
		//Input keys for changing of the lanes.  
		} else if(Input.GetKey("w")){//if we want to move up, and we're not currently changing lanes
			direction = 1;
			ChangeLanes(direction);
		} else if(Input.GetKey("s")){
			direction = -1;
			ChangeLanes(direction);
		}
		//Lane Spread Toggle
		else if (Input.GetKeyDown("space") && shipInstance != 0) {
			if(!laneSplit) {
				direction = shipInstance;
				ChangeLanes(shipInstance);
				laneSplit = true;
			} else {
				direction = -shipInstance;
				ChangeLanes(-shipInstance);
				laneSplit = false;
			}
		}
		if(Input.GetKey ("d")){
			if(transform.position.x < (mgmt.GetScreenWidth())){//this doesn't allow the player to go beyond halfway across the screen.
				transform.Translate(new Vector3(1,0,0)*Time.fixedDeltaTime*shipSpeed);	
			}
		}
		if(Input.GetKey ("a")){ 
			if(transform.position.x > (Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x)) {//this doesn't allow the player to go off the left side.
				transform.Translate(new Vector3(1,0,0)*(-1.0f)*Time.fixedDeltaTime*shipSpeed);	
			}
		}
	}
	
	//helper method for changing lanes. direction is -1 or 1 depending on whether ship is going up or down.
	private void ChangeLanes(int direction){
		
//		print(laneChanging+" "+amtMoved);
		if(!laneChanging){
			laneChanging = true;
			amtMoved = 0;
		}
		else if(amtMoved >= laneHeight){
			laneChanging = false;
			this.direction = 0;
		}
		else{
			Vector3 translateVector = new Vector3(0,laneChangeMult*shipSpeed*(float)direction*Time.fixedDeltaTime,0);
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
//			Debug.Log (health);//log it
			StartCoroutine(Ouch(3));
			if(health<=0 && !dead){//check to see if the ship needs to die,
				dead = true;
				Die();//and kill it if it does
			}
		}
	}
	
	//Method to render the shield
	public void RenderShield() {
		if(shielded) {
			transform.Find("Shield").GetComponent<MeshRenderer>().enabled = true;
		} else {
			transform.Find("Shield").GetComponent<MeshRenderer>().enabled = false;
		}	
	}
	
	public void RenderPoweredUp() {
		if(this.GetComponent<Weapon>().isPowerShot) {
			transform.Find("PoweredUp").GetComponent<MeshRenderer>().enabled = true;
		} else {
			transform.Find("PoweredUp").GetComponent<MeshRenderer>().enabled = false;	
		}
	}
	

	
	

	//Just in case, GetHealth
	public float GetHealth() {
		return health;
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Enemy")||other.gameObject.CompareTag("EnemyBullet")) {
			TakeDamage(other.gameObject.GetComponent<Spawnable>().GetDamage());
		}
		if(other.gameObject.CompareTag("PowerUp")) {//otherwise, if we have a powerup,
			Debug.Log (this.name + ": Powerup");
			if(other.gameObject.GetComponent<PowerUp>().IsShield()) { //if it's a shield
				Debug.Log (this.name + ": Shielded");
				StartCoroutine(Shield());
			}else{//otherwise, it has to be a powershot
				Debug.Log(this.name + ": Powershot");
				this.GetComponent<Weapon>().SetPowerShot();
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
	
	public void SetShipInstance(int i) {
		shipInstance = i;
	}
	
	//damage animation
	IEnumerator Ouch(int i){
		/*if(i==0){
			Debug.Log ("retungin");
			return true; 
		}*/
		renderer.material.SetTexture("_MainTex",inPain);
		yield return new WaitForSeconds(0.2f);
		renderer.material.SetTexture("_MainTex" ,aOk);
		Ouch (i--);
		
	}
	
	//shield animation
	IEnumerator Shield(){
		
		if(shieldAnim != null){
			shieldAnim.Play(); //play shield sharge up animation
			audio.PlayOneShot(shieldBlip,shieldBlipVolume);
		}
		yield return new WaitForSeconds(1.0f);
		shielded = true;//turn on shield
	}
	
	//death function
	public void Die() {
		audio.PlayOneShot(deathBlip,deathBlipVolume); //for some reason, not working. It knows the audio clip... -js
		mgmt.ShipDied();//Lets the GM know a ship is dead(do we care anymore?) /*Commented out by Emma for now as this method needs an int argument--which ship it is, I think?-- and if the ship knows this about itself, I can't find it. If this actually needs to be here, that'll need to be fixed for real.*/
		explosion = Instantiate(explosion,transform.position,Quaternion.identity) as GameObject;
		Destroy(gameObject);//and destroys itself.
		
	}
	
}
