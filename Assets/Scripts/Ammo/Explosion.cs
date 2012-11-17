using UnityEngine;
using System.Collections;

public class Explosion : Ammo {
	void Start(){
		//make a sphere collider
		SphereCollider s = gameObject.AddComponent("SphereCollider") as SphereCollider;
		//centered at the bomb
		s.center = Vector3.zero;
		//get lane height from GM
		GameManagement gm = Camera.main.GetComponent<GameManagement>();
		float height = gm.GetLaneHeight();
		//and set the radius of the collider to it
		s.radius = height/(this.transform.localScale.x);
		
	}
	//after colliders are done: death.
	void FixedUpdate(){
		Die ();
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log ("Explosion hit: "+col.gameObject.tag);
	}
	
	
}
