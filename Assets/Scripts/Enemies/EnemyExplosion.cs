using UnityEngine;
using System.Collections;

public class EnemyExplosion : Enemy {
	
	public GameObject detonator;
	public float superfluousVisualOffset = 10.0f;
	public GameObject playerExp;
	
	void Start(){
		damage = 3;
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
		
		//Instantiate a player explosion to damage enemies
		exp = Instantiate(playerExp, transform.position, transform.rotation) as GameObject;
	}
	//after colliders are done: death.
	void FixedUpdate(){
		this.Die();	
	}
	
	new void OnTriggerEnter(Collider col){
		//Debug.Log ("Explosion hit: "+col.gameObject.tag);
	}
	
	new void Die(){
		audio.PlayOneShot(deathClip, deathClipVolume);
		Destroy(gameObject);
	}
}
