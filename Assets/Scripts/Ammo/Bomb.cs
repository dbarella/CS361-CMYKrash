using UnityEngine;
using System.Collections;

public class Bomb : Ammo {
	private float halfScreen;
	private float initPosit;
	public GameObject explosionPrefab;
	void Start(){
		GameManagement gm = Camera.main.GetComponent<GameManagement>();
		halfScreen = gm.GetScreenWidth()/2.0f;
		initPosit = transform.position.x;
		
	}
	
	void FixedUpdate () {
		//if not to middle of screen, move to the right
		if(transform.position.x < halfScreen + initPosit) //Check this logic
			transform.Translate(speed*Vector3.right*Time.fixedDeltaTime);
		//otherwise, kaboom
		else{
			//Debug.Log("Bomb" + halfScreen);
			Explode();
		}
	}
	void OnTriggerEnter(Collider other){
		Debug.Log ("Bomb collided with " + other.tag);
		if(other.gameObject.tag == "Enemy"){
			Explode();
		}
	}
	private void Explode(){
		//get all spawnables
		/*Spawnable[] objects = FindObjectsOfType(typeof(Spawnable)) as Spawnable[];
		foreach(Spawnable s in objects){
			//if within range and not a powerup
			if(/*s.GetComponent<PowerUp>() == null && Distance (s.transform.position) < laneHeight/2){
				//then do damage
				s.TakeDamage(damage);
			}
		}*/
		explosionPrefab = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
		StartCoroutine(Wait());
		//time to die
		Die();

	}
	//distance formula!! why is this its own method? no one knows.
	private float Distance(Vector3 other){
		return Mathf.Sqrt(Mathf.Pow(other.x-transform.position.x,2)+Mathf.Pow(other.y - transform.position.y,2));
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds(3);
	}
}

