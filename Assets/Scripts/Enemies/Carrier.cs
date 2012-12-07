using UnityEngine;
using System.Collections;

public class Carrier : Enemy {
	//a fine piece of craftsmanship

	public GameObject explosion;
	public GameObject buzzard;
	public float launchInterval;
	public float launchDist;
	
	private float timer;

	// Use this for initialization
	void Start () {
		timer = launchInterval;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move();
		
		if(timer<0){
			Launch();
			timer = launchInterval;
		}
		
		if(timer>0){
			timer -= Time.deltaTime;
		}
	
	}
	
	void Launch(){
		Vector3 launchPad = new Vector3(transform.position.x - launchDist, transform.position.y,transform.position.z);
		Instantiate(buzzard,launchPad,Quaternion.identity);
		//buzzard = Instantiate(buzzard,launchPad,Quaternion.identity) as GameObject;
		//buzzard.GetComponent<Missile>().ChangeLane(1);
		//buzzard = Instantiate(buzzard,launchPad,Quaternion.identity) as GameObject;
		//buzzard.GetComponent<Missile>().ChangeLane(-1);
		
	}
	
	void OnTriggerEnter(Collider col){
		float temp;
		if(col.tag == "Player"){
			Destroy (gameObject);
		}
		if(col.tag == "Ammo"){
			temp = col.gameObject.GetComponent<Ammo>().GetDamage();
			TakeDamage(temp);
			if(health<=0){
				Die ();
//				Debug.Log(this.name + ": Died.");
			}
		}
	}
	
	new void Die(){
		//would love to do death animation
		Destroy(gameObject);
	}
}
	
