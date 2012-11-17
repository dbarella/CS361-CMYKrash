using UnityEngine;
using System.Collections;

public class Explosion : Ammo {
	
	public GameObject detonator;
	public float superfluousVisualOffset = 10.0f;
	
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
		
		//Instantiate a detonator at this location
		GameObject exp = Instantiate(detonator, transform.position, transform.rotation) as GameObject;
		Detonator d = exp.GetComponent<Detonator>();
		
		d.size = s.radius + superfluousVisualOffset;
	}
	//after colliders are done: death.
	void FixedUpdate(){
		Die ();
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log ("Explosion hit: "+col.gameObject.tag);
	}
}
