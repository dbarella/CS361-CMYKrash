using UnityEngine;
using System.Collections;

public class Reticule : Enemy {
	public GameObject enemyExplosion;
	public float countDown = 5.0f;
	
	void Start(){
		
			}

	void FixedUpdate(){
		if(countDown <= 0.0f){
			enemyExplosion = Instantiate(enemyExplosion, transform.position, transform.rotation) as GameObject;
			this.Die();	
		}else{
			countDown -= Time.fixedDeltaTime;
		}
		
		
		
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log ("Explosion hit: "+col.gameObject.tag);
	}
}