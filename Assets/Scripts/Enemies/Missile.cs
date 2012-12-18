using UnityEngine;
using System.Collections;

public class Missile : Enemy {
	public bool isRandom;
	public float laneChangeTime;
	
	private float _timer;
	
	public AudioClip clip;
	public GameObject explosion;
	
	void Start () {
		//base.speedMult = 50;
		//deathBlip = GetComponent<AudioSource>();
		deathClip = clip;
		explosionPrefab = explosion;
	}
	
	void FixedUpdate () {
		base.Move();
		if(!laneChanging)
			_timer -= Time.fixedDeltaTime;
		
		if(laneChanging) ChangeLane(direction);
		if(_timer <= 0f && isRandom) {
			_timer = laneChangeTime; //Reset the timer
//			Debug.Log("Reset Timer");
			ChangeLane( Random.Range(-1,2) ); //Generate a random into to change lanes
		}
	}
	
	new void Die(){
		Debug.Log("missile died!");
		GetComponent<Rigidbody>().useGravity = true;
		explosionPrefab = Instantiate(explosionPrefab,transform.position,Quaternion.identity) as GameObject;
	}
	
	/*void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Destroy (gameObject);
		}
		if(col.tag == "Ammo"){
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			Debug.Log ("Received Damage: " + temp);
			TakeDamage(temp);
			if(health<=0){
				Destroy(gameObject);
			}
		}
	}*/
}
/*
if(col.gameObject.tag == "Ammo"){
			Debug.Log ("collided with " + col.gameObject.name);
			Debug.Log ("hit by Ammo");
			TakeDamage(col.gameObject.GetComponent<Ammo>().GetDamage());
			Debug.Log ("Missile Health ="+ health);
			if(health<=0){
				Die ();
			}
		}
		else if(col.gameObject.tag == "Ship"){
			Debug.Log ("hit by Ship");
			Die ();
		}*/
