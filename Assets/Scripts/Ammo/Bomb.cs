using UnityEngine;
using System.Collections;

public class Bomb : Ammo {
	private float halfScreen;
	private float laneHeight;
	
	void Start(){
			GameManagement gm = Camera.main.GetComponent<GameManagement>();
			halfScreen = gm.GetScreenWidth();
			laneHeight = gm.GetLaneHeight();
	}
	
	void Update () {
		//if not to middle of screen, move to the right
		if(transform.position.x < halfScreen)
			transform.Translate(speed*Vector3.right*Time.deltaTime);
		//otherwise, kaboom
		else{
			Explode();
		}
	}
	private void Explode(){
		//get all spawnables
		Spawnable[] objects = FindObjectsOfType(typeof(Spawnable)) as Spawnable[];
		foreach(Spawnable s in objects){
			//if within range and not a powerup
			if(s.GetComponent<PowerUp>() == null &&Distance (s.transform.position) < laneHeight/2){
				//then do damage
				s.TakeDamage(damage);
			}
		}
		//time to die
		Die();

	}
	//distance formula!! why is this its own method? no one knows.
	private float Distance(Vector3 other){
		return Mathf.Sqrt(Mathf.Pow(other.x-transform.position.x,2)+Mathf.Pow(other.y - transform.position.y,2));
	}
}
